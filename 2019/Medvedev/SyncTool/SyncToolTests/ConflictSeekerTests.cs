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

            Assert.True(seeker.GetConflicts().Count == 0);
        }

        [Fact]
        public void GetConflicts_ConflictWithSlave_ReturnsNonEmptyList()
        {
            var master = Directory.CreateDirectory("master");
            var slave = Directory.CreateDirectory("slave");

            File.Create(@"master\a.txt").Close();
            var file = File.Create(@"slave\a.txt");
            file.WriteByte(1);
            file.Close();

            var seeker = new ConflictSeeker(master, slave);

            Assert.True(seeker.GetConflicts().Count > 0);
        }

        [Fact]
        public void GetConflicts_ConflictSlaveWithMaster_ExactlyOne_Slave_Master_Conflict()
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
        public void GetConflicts_ConflictSlaveWithMaster_Returns_Slave_Master_Conflict()
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
        public void GetConflicts_ConflictMasterWithSlave_ExactlyOne_Master_Slave_Conflict()
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
        public void GetConflicts_ConflictMasterWithSlave_Returns_Master_Slave_Conflict()
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
    }
}
