using System.IO;
using Sync.Comparers;
using Sync.Wrappers;
using Xunit;

namespace Sync.Tests
{
    public class ConflictSeekerTests
    {
        [Fact]
        public void GetConflicts_NoConflicts_ReturnsEmptyConflictList()
        {
            DeleteDirectories();

            var master = Directory.CreateDirectory("master");
            var slave = Directory.CreateDirectory("slave");

            File.Create(@"master\a.txt").Close();
            File.Create(@"slave\a.txt").Close();

            var seeker =
                new ConflictSeeker(new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer()));

            Assert.Empty(seeker.GetConflicts(master, slave));
        }

        [Fact]
        public void GetConflicts_SlaveFileConflictsWithMaster_ReturnsNonEmptyList()
        {
            DeleteDirectories();

            var master = Directory.CreateDirectory("master");
            var slave = Directory.CreateDirectory("slave");

            File.Create(@"master\a.txt").Close();
            var file = File.Create(@"slave\a.txt");
            file.WriteByte(1);
            file.Close();

            var seeker =
                new ConflictSeeker(new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer()));


            Assert.NotEmpty(seeker.GetConflicts(master, slave));
        }

        [Fact]
        public void GetConflicts_SlaveFileContainmentConflictsWithMaster_ExactlyOne_Slave_Master_Conflict()
        {
            DeleteDirectories();

            var master = Directory.CreateDirectory("master");
            var slave = Directory.CreateDirectory("slave");

            File.Create(@"master\a.txt").Close();
            var file = File.Create(@"slave\a.txt");
            file.WriteByte(1);
            file.Close();

            var seeker =
                new ConflictSeeker(new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer()));
            Assert.Single(seeker.GetConflicts(master, slave));
        }


        [Fact]
        public void GetConflicts_SlaveFileContainmentConflictsWithMaster_Returns_Slave_Master_Conflict()
        {
            DeleteDirectories();

            var master = Directory.CreateDirectory("master");
            var slave = Directory.CreateDirectory("slave");

            File.Create(@"master\a.txt").Close();
            var file = File.Create(@"slave\a.txt");
            file.WriteByte(1);
            file.Close();

            var seeker =
                new ConflictSeeker(new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer()));
            var conflict = seeker.GetConflicts(master, slave)[0];

            var source = (FileWrapper) conflict.Source;
            var destination = (FileWrapper) conflict.Destination;

            Assert.Contains("slave", source.File.FullName);
            Assert.Contains("master", destination.File.FullName);
        }

        [Fact]
        public void GetConflicts_MasterFileContainmentConflictsWithSlave_ExactlyOne_Master_Slave_Conflict()
        {
            DeleteDirectories();

            var master = Directory.CreateDirectory("master");
            var slave = Directory.CreateDirectory("slave");

            File.Create(@"slave\a.txt").Close();
            var file = File.Create(@"master\a.txt");
            file.WriteByte(1);
            file.Close();

            var seeker =
                new ConflictSeeker(new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer()));
            var conflicts = seeker.GetConflicts(master, slave);
            Assert.Single(conflicts);
        }

        [Fact]
        public void GetConflicts_MasterFileContainmentConflictsWithSlave_Returns_Master_Slave_Conflict()
        {
            DeleteDirectories();

            var master = Directory.CreateDirectory("master");
            var slave = Directory.CreateDirectory("slave");

            File.Create(@"slave\a.txt").Close();
            var file = File.Create(@"master\a.txt");
            file.WriteByte(1);
            file.Close();

            var seeker =
                new ConflictSeeker(new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer()));
            var conflict = seeker.GetConflicts(master, slave)[0];

            var source = (FileWrapper) conflict.Source;
            var destination = (FileWrapper) conflict.Destination;

            Assert.Contains("master", source.File.FullName);
            Assert.Contains("slave", destination.File.FullName);
        }

        [Fact]
        public void GetConflicts_FileDoesNotExistsInSlave_NotNull_Null()
        {
            DeleteDirectories();

            var master = Directory.CreateDirectory("master");
            var slave = Directory.CreateDirectory("slave");

            if (File.Exists(@"slave\a.txt"))
                File.Delete(@"slave\a.txt");

            File.Create(@"master\a.txt").Close();

            var seeker =
                new ConflictSeeker(new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer()));
            var conflicts = seeker.GetConflicts(master, slave);
            var conflict = conflicts[0];

            var source = (FileWrapper) conflict.Source;
            var destination = (FileWrapper) conflict.Destination;

            Assert.Null(destination);
            Assert.NotNull(source);
        }

        [Fact]
        public void GetConflicts_FileDoesNotExistsInMaster_Null_NotNull()
        {
            DeleteDirectories();

            var master = Directory.CreateDirectory("master");
            var slave = Directory.CreateDirectory("slave");

            if (File.Exists(@"master\a.txt"))
                File.Delete(@"master\a.txt");

            File.Create(@"slave\a.txt").Close();

            var seeker =
                new ConflictSeeker(new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer()));
            var conflicts = seeker.GetConflicts(master, slave);
            var conflict = conflicts[0];

            var source = (FileWrapper) conflict.Source;
            var destination = (FileWrapper) conflict.Destination;

            Assert.NotNull(destination);
            Assert.Null(source);
        }

        [Fact]
        public void GetConflicts_MasterSubDirectoriesNoConflictsWithSlave_ReturnsEmptyList()
        {
            DeleteDirectories();

            var master = Directory.CreateDirectory("master");
            var slave = Directory.CreateDirectory("slave");

            Directory.CreateDirectory(@"master/a");
            Directory.CreateDirectory(@"master/b");

            Directory.CreateDirectory(@"slave/a");
            Directory.CreateDirectory(@"slave/b");

            var seeker =
                new ConflictSeeker(new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer()));

            Assert.Empty(seeker.GetConflicts(master, slave));
        }

        [Fact]
        public void GetConflicts_MasterSubDirectoriesConflictsWithSlave_ReturnsNonEmptyList()
        {
            DeleteDirectories();

            var master = Directory.CreateDirectory("master");
            var slave = Directory.CreateDirectory("slave");

            Directory.CreateDirectory(@"master/a");
            Directory.CreateDirectory(@"master/b");

            var seeker =
                new ConflictSeeker(new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer()));

            Assert.NotEmpty(seeker.GetConflicts(master, slave));
        }

        [Fact]
        public void GetConflicts_SlaveSubDirectoriesConflictsWithMaster_ReturnsNonEmptyList()
        {
            DeleteDirectories();

            var master = Directory.CreateDirectory("master");
            var slave = Directory.CreateDirectory("slave");

            Directory.CreateDirectory(@"slave/a");
            Directory.CreateDirectory(@"slave/b");

            var seeker =
                new ConflictSeeker(new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer()));

            Assert.NotEmpty(seeker.GetConflicts(master, slave));
        }

        [Fact]
        public void GetConflicts_MasterSubDirectoriesConflictsWithSlave_OneConflict_ReturnsOneConflict()
        {
            DeleteDirectories();

            var master = Directory.CreateDirectory("master");
            var slave = Directory.CreateDirectory("slave");

            Directory.CreateDirectory(@"master/a");
            Directory.CreateDirectory(@"master/b");

            Directory.CreateDirectory(@"slave/a");

            var seeker =
                new ConflictSeeker(new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer()));
            var conflicts = seeker.GetConflicts(master, slave);
            Assert.Single(conflicts);
        }

        [Fact]
        public void GetConflicts_SlaveSubDirectoriesConflictsWithMaster_OneConflict_ReturnsOneConflict()
        {
            DeleteDirectories();

            var master = Directory.CreateDirectory("master");
            var slave = Directory.CreateDirectory("slave");

            Directory.CreateDirectory(@"slave/a");
            Directory.CreateDirectory(@"slave/b");

            Directory.CreateDirectory(@"master/a");

            var seeker =
                new ConflictSeeker(new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer()));
            var conflicts = seeker.GetConflicts(master, slave);
            Assert.Single(conflicts);
        }

        [Fact]
        public void GetConflicts_MasterSubDirectoriesConflictsWithSlave_OneConflict_Returns_NotNull_Null()
        {
            DeleteDirectories();

            var master = Directory.CreateDirectory("master");
            var slave = Directory.CreateDirectory("slave");

            Directory.CreateDirectory(@"master/a");
            Directory.CreateDirectory(@"master/b");

            Directory.CreateDirectory(@"slave/a");

            var seeker =
                new ConflictSeeker(new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer()));

            var conflicts = seeker.GetConflicts(master, slave);
            Assert.NotNull(conflicts[0].Source);
            Assert.Null(conflicts[0].Destination);
        }

        [Fact]
        public void GetConflicts_SlaveSubDirectoriesConflictsWithMaster_OneConflict_Returns_Null_NotNull()
        {
            DeleteDirectories();

            var master = Directory.CreateDirectory("master");
            var slave = Directory.CreateDirectory("slave");

            Directory.CreateDirectory(@"slave/a");
            Directory.CreateDirectory(@"slave/b");

            Directory.CreateDirectory(@"master/a");

            var seeker =
                new ConflictSeeker(new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer()));
            var conflicts = seeker.GetConflicts(master, slave);
            Assert.Null(conflicts[0].Source);
            Assert.NotNull(conflicts[0].Destination);
        }

        private static void DeleteDirectories()
        {
            if (Directory.Exists("master"))
                Directory.Delete("master", true);
            if (Directory.Exists("slave"))
                Directory.Delete("slave", true);
        }
    }
}