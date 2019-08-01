using Xunit;
using System.IO;
using SyncTool.Wrappers;

namespace SyncTool.Tests
{
    public class ConflictSeekerTests
    {
        [Fact]
        public void GetConflicts_NoConflicts_ReturnsEmptyConflictList()
        {
            var master = Directory.CreateDirectory("master");
            var slave = Directory.CreateDirectory("slave");

            File.Create(@"master\a.txt").Close();
            File.Create(@"slave\a.txt").Close();

            var seeker = new ConflictSeeker(master, slave);

            Assert.Empty(seeker.GetConflicts());
        }

        [Fact]
        public void GetConflicts_SlaveFileConflictsWithMaster_ReturnsNonEmptyList()
        {
            var master = Directory.CreateDirectory("master");
            var slave = Directory.CreateDirectory("slave");

            File.Create(@"master\a.txt").Close();
            var file = File.Create(@"slave\a.txt");
            file.WriteByte(1);
            file.Close();

            var seeker = new ConflictSeeker(master, slave);

            Assert.NotEmpty(seeker.GetConflicts());
        }

        [Fact]
        public void GetConflicts_SlaveFileConflictsWithMaster_ExactlyOne_Slave_Master_Conflict()
        {
            var master = Directory.CreateDirectory("master");
            var slave = Directory.CreateDirectory("slave");

            File.Create(@"master\a.txt").Close();
            var file = File.Create(@"slave\a.txt");
            file.WriteByte(1);
            file.Close();

            var seeker = new ConflictSeeker(master, slave);
            Assert.Single(seeker.GetConflicts());
        }

        [Fact]
        public void GetConflicts_SlaveFileConflictsWithMaster_Returns_Slave_Master_Conflict()
        {
            var master = Directory.CreateDirectory("master");
            var slave = Directory.CreateDirectory("slave");

            File.Create(@"master\a.txt").Close();
            var file = File.Create(@"slave\a.txt");
            file.WriteByte(1);
            file.Close();

            var seeker = new ConflictSeeker(master, slave);
            var conflict = seeker.GetConflicts()[0];

            var source = (FileInfoWrapper) conflict.Source;
            var destination = (FileInfoWrapper) conflict.Destination;

            Assert.Contains("slave", source.File.FullName);
            Assert.Contains("master", destination.File.FullName);
        }

        [Fact]
        public void GetConflicts_MasterFileConflictsWithSlave_ExactlyOne_Master_Slave_Conflict()
        {
            var master = Directory.CreateDirectory("master");
            var slave = Directory.CreateDirectory("slave");

            File.Create(@"slave\a.txt").Close();
            var file = File.Create(@"master\a.txt");
            file.WriteByte(1);
            file.Close();

            var seeker = new ConflictSeeker(master, slave);
            var conflicts = seeker.GetConflicts();
            Assert.Single(conflicts);
        }

        [Fact]
        public void GetConflicts_MasterFileConflictsWithSlave_Returns_Master_Slave_Conflict()
        {
            var master = Directory.CreateDirectory("master");
            var slave = Directory.CreateDirectory("slave");

            File.Create(@"slave\a.txt").Close();
            var file = File.Create(@"master\a.txt");
            file.WriteByte(1);
            file.Close();

            var seeker = new ConflictSeeker(master, slave);
            var conflict = seeker.GetConflicts()[0];

            var source = (FileInfoWrapper)conflict.Source;
            var destination = (FileInfoWrapper)conflict.Destination;

            Assert.Contains("master", source.File.FullName);
            Assert.Contains("slave", destination.File.FullName);
        }

        [Fact]
        public void GetConflicts_FileDoesNotExistsInSlave_NotNull_Null()
        {
            var master = Directory.CreateDirectory("master");
            var slave = Directory.CreateDirectory("slave");

            if (File.Exists(@"slave\a.txt"))
                File.Delete(@"slave\a.txt");

            File.Create(@"master\a.txt").Close();

            var seeker = new ConflictSeeker(master, slave);
            var conflicts = seeker.GetConflicts();
            var conflict = conflicts[0];

            var source = (FileInfoWrapper)conflict.Source;
            var destination = (FileInfoWrapper)conflict.Destination;

            Assert.Null(destination);
            Assert.NotNull(source);
        }

        [Fact]
        public void GetConflicts_FileDoesNotExistsInMaster_Null_NotNull()
        {
            var master = Directory.CreateDirectory("master");
            var slave = Directory.CreateDirectory("slave");

            if (File.Exists(@"master\a.txt"))
                File.Delete(@"master\a.txt");

            File.Create(@"slave\a.txt").Close();

            var seeker = new ConflictSeeker(master, slave);
            var conflicts = seeker.GetConflicts();
            var conflict = conflicts[0];

            var source = (FileInfoWrapper)conflict.Source;
            var destination = (FileInfoWrapper)conflict.Destination;

            Assert.NotNull(destination);
            Assert.Null(source);
        }

        [Fact]
        public void GetConflicts_MasterSubDirectoriesNoConflictsWithSlave_ReturnsEmptyList()
        {
            if (Directory.Exists("master"))
                Directory.Delete("master", true);
            if (Directory.Exists("slave"))
                Directory.Delete("slave", true);

            var master = Directory.CreateDirectory("master");
            var slave = Directory.CreateDirectory("slave");

            Directory.CreateDirectory(@"master/a");
            Directory.CreateDirectory(@"master/b");

            Directory.CreateDirectory(@"slave/a");
            Directory.CreateDirectory(@"slave/b");

            var seeker = new ConflictSeeker(master, slave);

            Assert.Empty(seeker.GetConflicts());
        }

        [Fact]
        public void GetConflicts_MasterSubDirectoriesConflictsWithSlave_ReturnsNonEmptyList()
        {
            if (Directory.Exists("master"))
                Directory.Delete("master", true);
            if (Directory.Exists("slave"))
                Directory.Delete("slave", true);

            var master = Directory.CreateDirectory("master");
            var slave = Directory.CreateDirectory("slave");

            Directory.CreateDirectory(@"master/a");
            Directory.CreateDirectory(@"master/b");

            var seeker = new ConflictSeeker(master, slave);

            Assert.NotEmpty(seeker.GetConflicts());
        }

        [Fact]
        public void GetConflicts_SlaveSubDirectoriesConflictsWithMaster_ReturnsNonEmptyList()
        {
            if (Directory.Exists("master"))
                Directory.Delete("master", true);
            if (Directory.Exists("slave"))
                Directory.Delete("slave", true);

            var master = Directory.CreateDirectory("master");
            var slave = Directory.CreateDirectory("slave");

            Directory.CreateDirectory(@"slave/a");
            Directory.CreateDirectory(@"slave/b");

            var seeker = new ConflictSeeker(master, slave);

            Assert.NotEmpty(seeker.GetConflicts());
        }

        [Fact]
        public void GetConflicts_MasterSubDirectoriesConflictsWithSlave_OneConflict_ReturnsOneConflict()
        {
            if (Directory.Exists("master"))
                Directory.Delete("master", true);
            if (Directory.Exists("slave"))
                Directory.Delete("slave", true);

            var master = Directory.CreateDirectory("master");
            var slave = Directory.CreateDirectory("slave");

            Directory.CreateDirectory(@"master/a");
            Directory.CreateDirectory(@"master/b");

            Directory.CreateDirectory(@"slave/a");

            var seeker = new ConflictSeeker(master, slave);
            var conflicts = seeker.GetConflicts();
            Assert.Single(conflicts);
        }

        [Fact]
        public void GetConflicts_SlaveSubDirectoriesConflictsWithMaster_OneConflict_ReturnsOneConflict()
        {
            if (Directory.Exists("master"))
                Directory.Delete("master", true);
            if (Directory.Exists("slave"))
                Directory.Delete("slave", true);

            var master = Directory.CreateDirectory("master");
            var slave = Directory.CreateDirectory("slave");

            Directory.CreateDirectory(@"slave/a");
            Directory.CreateDirectory(@"slave/b");

            Directory.CreateDirectory(@"master/a");

            var seeker = new ConflictSeeker(master, slave);
            var conflicts = seeker.GetConflicts();
            Assert.Single(conflicts);
        }

        [Fact]
        public void GetConflicts_MasterSubDirectoriesConflictsWithSlave_OneConflict_Returns_NotNull_Null()
        {
            if (Directory.Exists("master"))
                Directory.Delete("master", true);
            if (Directory.Exists("slave"))
                Directory.Delete("slave", true);

            var master = Directory.CreateDirectory("master");
            var slave = Directory.CreateDirectory("slave");

            Directory.CreateDirectory(@"master/a");
            Directory.CreateDirectory(@"master/b");

            Directory.CreateDirectory(@"slave/a");

            var seeker = new ConflictSeeker(master, slave);
            var conflicts = seeker.GetConflicts();
            Assert.NotNull(conflicts[0].Source);
            Assert.Null(conflicts[0].Destination);
        }

        [Fact]
        public void GetConflicts_SlaveSubDirectoriesConflictsWithMaster_OneConflict_Returns_Null_NotNull()
        {
            if (Directory.Exists("master"))
                Directory.Delete("master", true);
            if (Directory.Exists("slave"))
                Directory.Delete("slave", true);

            var master = Directory.CreateDirectory("master");
            var slave = Directory.CreateDirectory("slave");

            Directory.CreateDirectory(@"slave/a");
            Directory.CreateDirectory(@"slave/b");

            Directory.CreateDirectory(@"master/a");

            var seeker = new ConflictSeeker(master, slave);
            var conflicts = seeker.GetConflicts();
            Assert.Null(conflicts[0].Source);
            Assert.NotNull(conflicts[0].Destination);
        }
    }
}
