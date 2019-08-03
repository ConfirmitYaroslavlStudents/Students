using System;
using Sync.Comparers;
using Sync.ConflictDetectionPolicies;
using Sync.Wrappers;
using Xunit;

namespace Sync.Tests
{
    public class ConflictSeekerTests
    {
        [Fact]
        public void GetConflicts_NoConflicts_ReturnsEmptyConflictList()
        {
            var master = new DirectoryWrapper("master");
            var slave = new DirectoryWrapper("slave");
            master.CreateFile("1", new FileAttributes(1, DateTime.MinValue));
            slave.CreateFile("1", new FileAttributes(1, DateTime.MinValue));

            var seeker =
                new ConflictSeeker(new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer()));

            Assert.Empty(seeker.GetConflicts(master, slave));
        }

        [Fact]
        public void GetConflicts_SlaveFileConflictsWithMaster_ReturnsNonEmptyList()
        {
            var master = new DirectoryWrapper("master");
            var slave = new DirectoryWrapper("slave");
            master.CreateFile("1", new FileAttributes(1, DateTime.MinValue));
            slave.CreateFile("1", new FileAttributes(1, DateTime.MaxValue));

            var seeker =
                new ConflictSeeker(new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer()));


            Assert.NotEmpty(seeker.GetConflicts(master, slave));
        }

        [Fact]
        public void GetConflicts_SlaveFileContainmentConflictsWithMaster_ExactlyOne_Slave_Master_Conflict()
        {
            var master = new DirectoryWrapper("master");
            var slave = new DirectoryWrapper("slave");
            master.CreateFile("1", new FileAttributes(1, DateTime.MinValue));
            slave.CreateFile("1", new FileAttributes(2, DateTime.MinValue));

            var seeker =
                new ConflictSeeker(new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer()));
            Assert.Single(seeker.GetConflicts(master, slave));
        }


        [Fact]
        public void GetConflicts_SlaveFileContainmentConflictsWithMaster_Returns_Slave_Master_Conflict()
        {
            var master = new DirectoryWrapper("master");
            var slave = new DirectoryWrapper("slave");
            master.CreateFile("1", new FileAttributes(1, DateTime.MinValue));
            slave.CreateFile("1", new FileAttributes(2, DateTime.MinValue));

            var seeker =
                new ConflictSeeker(new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer()));
            var conflict = seeker.GetConflicts(master, slave)[0];

            var source = (FileWrapper) conflict.Source;
            var destination = (FileWrapper) conflict.Destination;

            Assert.Contains("slave", source.FullName);
            Assert.Contains("master", destination.FullName);
        }

        [Fact]
        public void GetConflicts_MasterFileContainmentConflictsWithSlave_ExactlyOne_Master_Slave_Conflict()
        {
            var master = new DirectoryWrapper("master");
            var slave = new DirectoryWrapper("slave");
            master.CreateFile("1", new FileAttributes(2, DateTime.MinValue));
            slave.CreateFile("1", new FileAttributes(1, DateTime.MinValue));

            var seeker =
                new ConflictSeeker(new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer()));
            var conflicts = seeker.GetConflicts(master, slave);
            Assert.Single(conflicts);
        }

        [Fact]
        public void GetConflicts_MasterFileContainmentConflictsWithSlave_Returns_Master_Slave_Conflict()
        {
            var master = new DirectoryWrapper("master");
            var slave = new DirectoryWrapper("slave");
            master.CreateFile("1", new FileAttributes(2, DateTime.MinValue));
            slave.CreateFile("1", new FileAttributes(1, DateTime.MinValue));

            var seeker =
                new ConflictSeeker(new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer()));
            var conflict = seeker.GetConflicts(master, slave)[0];

            var source = (FileWrapper) conflict.Source;
            var destination = (FileWrapper) conflict.Destination;

            Assert.Contains("master", source.FullName);
            Assert.Contains("slave", destination.FullName);
        }

        [Fact]
        public void GetConflicts_FileDoesNotExistsInSlave_NotNull_Null()
        {
            var master = new DirectoryWrapper("master");
            var slave = new DirectoryWrapper("slave");
            master.CreateFile("1", new FileAttributes(2, DateTime.MinValue));

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
            var master = new DirectoryWrapper("master");
            var slave = new DirectoryWrapper("slave");
            slave.CreateFile("1", new FileAttributes(1, DateTime.MinValue));

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
            var master = new DirectoryWrapper("master");
            var slave = new DirectoryWrapper("slave");

            master.CreateDirectory("a");
            master.CreateDirectory("b");

            slave.CreateDirectory("a");
            slave.CreateDirectory("b");

            var seeker =
                new ConflictSeeker(new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer()));

            Assert.Empty(seeker.GetConflicts(master, slave));
        }

        [Fact]
        public void GetConflicts_MasterSubDirectoriesConflictsWithSlave_ReturnsNonEmptyList()
        {
            var master = new DirectoryWrapper("master");
            var slave = new DirectoryWrapper("slave");

            master.CreateDirectory("a");
            master.CreateDirectory("b");

            slave.CreateDirectory("a");

            var seeker =
                new ConflictSeeker(new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer()));

            Assert.NotEmpty(seeker.GetConflicts(master, slave));
        }

        [Fact]
        public void GetConflicts_SlaveSubDirectoriesConflictsWithMaster_ReturnsNonEmptyList()
        {
            var master = new DirectoryWrapper("master");
            var slave = new DirectoryWrapper("slave");

            master.CreateDirectory("a");

            slave.CreateDirectory("a");
            slave.CreateDirectory("b");

            var seeker =
                new ConflictSeeker(new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer()));

            Assert.NotEmpty(seeker.GetConflicts(master, slave));
        }

        [Fact]
        public void GetConflicts_MasterSubDirectoriesConflictsWithSlave_OneConflict_ReturnsOneConflict()
        {
            var master = new DirectoryWrapper("master");
            var slave = new DirectoryWrapper("slave");

            master.CreateDirectory("a");
            master.CreateDirectory("b");

            slave.CreateDirectory("a");

            var seeker =
                new ConflictSeeker(new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer()));
            var conflicts = seeker.GetConflicts(master, slave);
            Assert.Single(conflicts);
        }

        [Fact]
        public void GetConflicts_SlaveSubDirectoriesConflictsWithMaster_OneConflict_ReturnsOneConflict()
        {
            var master = new DirectoryWrapper("master");
            var slave = new DirectoryWrapper("slave");

            master.CreateDirectory("a");

            slave.CreateDirectory("a");
            slave.CreateDirectory("b");


            var seeker =
                new ConflictSeeker(new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer()));
            var conflicts = seeker.GetConflicts(master, slave);
            Assert.Single(conflicts);
        }

        [Fact]
        public void GetConflicts_MasterSubDirectoriesConflictsWithSlave_OneConflict_Returns_NotNull_Null()
        {
            var master = new DirectoryWrapper("master");
            var slave = new DirectoryWrapper("slave");

            master.CreateDirectory("a");
            master.CreateDirectory("b");

            slave.CreateDirectory("a");


            var seeker =
                new ConflictSeeker(new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer()));

            var conflicts = seeker.GetConflicts(master, slave);
            Assert.NotNull(conflicts[0].Source);
            Assert.Null(conflicts[0].Destination);
        }

        [Fact]
        public void GetConflicts_SlaveSubDirectoriesConflictsWithMaster_OneConflict_Returns_Null_NotNull()
        {
            var master = new DirectoryWrapper("master");
            var slave = new DirectoryWrapper("slave");

            master.CreateDirectory("a");

            slave.CreateDirectory("a");
            slave.CreateDirectory("b");


            var seeker =
                new ConflictSeeker(new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer()));
            var conflicts = seeker.GetConflicts(master, slave);
            Assert.Null(conflicts[0].Source);
            Assert.NotNull(conflicts[0].Destination);
        }
    }
}