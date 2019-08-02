using System.IO;
using Sync.Comparers;
using Xunit;

namespace Sync.Tests
{
    public class ConflictCollectorTests
    {
        [Fact]
        public void GetConflicts_NoConflicts_ReturnsEmptyList()
        {
            DeleteDirectories();

            var master = Directory.CreateDirectory("master");
            var slave = Directory.CreateDirectory("slave");

            Directory.CreateDirectory(@"master\a");
            Directory.CreateDirectory(@"slave\a");

            File.Create(@"master\1").Close();
            File.Create(@"master\a\2").Close();

            File.Create(@"slave\1").Close();
            File.Create(@"slave\a\2").Close();

            var collector = new ConflictsCollector(master, slave, new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer()));

            var conflicts = collector.GetConflicts();

            Assert.Empty(conflicts);
        }

        [Fact]
        public void GetConflicts_MasterConflictsWithSlave_ReturnsNonEmptyList()
        {
            DeleteDirectories();

            var master = Directory.CreateDirectory("master");
            var slave = Directory.CreateDirectory("slave");

            Directory.CreateDirectory(@"master\a");
            Directory.CreateDirectory(@"slave\a");

            File.Create(@"master\1").Close();
            File.Create(@"master\a\2").Close();

            File.Create(@"slave\1").Close();

            var collector = new ConflictsCollector(master, slave, new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer()));

            var conflicts = collector.GetConflicts();

            Assert.NotEmpty(conflicts);
        }

        [Fact]
        public void GetConflicts_OneMasterFileConflictsWithSlave_ReturnsOneConflict()
        {
            DeleteDirectories();

            var master = Directory.CreateDirectory("master");
            var slave = Directory.CreateDirectory("slave");

            Directory.CreateDirectory(@"master\a");
            Directory.CreateDirectory(@"slave\a");

            File.Create(@"master\1").Close();
            File.Create(@"master\a\2").Close();

            File.Create(@"slave\1").Close();

            var collector = new ConflictsCollector(master, slave, new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer()));

            var conflicts = collector.GetConflicts();

            Assert.Single(conflicts);
        }

        [Fact]
        public void GetConflicts_OneMasterFileConflictsWithSlave_Returns_NotNull_Null()
        {
            DeleteDirectories();

            var master = Directory.CreateDirectory("master");
            var slave = Directory.CreateDirectory("slave");

            Directory.CreateDirectory(@"master\a");
            Directory.CreateDirectory(@"slave\a");

            File.Create(@"master\1").Close();
            File.Create(@"master\a\2").Close();

            File.Create(@"slave\1").Close();

            var collector = new ConflictsCollector(master, slave, new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer()));

            var conflicts = collector.GetConflicts();

            Assert.NotNull(conflicts[0].Source);
            Assert.Null(conflicts[0].Destination);
        }

        [Fact]
        public void GetConflicts_OneSlaveFileConflictsWithMaster_Returns_Null_NotNull()
        {
            DeleteDirectories();

            var master = Directory.CreateDirectory("master");
            var slave = Directory.CreateDirectory("slave");

            Directory.CreateDirectory(@"master\a");
            Directory.CreateDirectory(@"slave\a");

            File.Create(@"master\1").Close();

            File.Create(@"slave\a\2").Close();
            File.Create(@"slave\1").Close();

            var collector = new ConflictsCollector(master, slave, new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer()));

            var conflicts = collector.GetConflicts();

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