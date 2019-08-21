using System;
using System.Collections.Generic;
using System.Linq;
using Sync.Comparers;
using Sync.ConflictDetectionPolicies;
using Sync.Wrappers;
using Xunit;

namespace Sync.Tests
{
    public class ConflictCollectorTests
    {
        [Fact]
        public void GetConflicts_NoConflicts_ReturnsEmptyList()
        {
            var master = new DirectoryWrapper("master");
            master.CreateFile("1", new FileAttributes(1, DateTime.MinValue));
            master.CreateDirectory("a").CreateFile("2", new FileAttributes(1, DateTime.MinValue));

            var slave = new DirectoryWrapper("slave");
            slave.CreateFile("1", new FileAttributes(1, DateTime.MinValue));
            slave.CreateDirectory("a").CreateFile("2", new FileAttributes(1, DateTime.MinValue));

            var collector = new ConflictsCollector(master, slave, new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer()));

            var conflicts = collector.GetConflicts();

            Assert.Empty(conflicts);
        }

        [Fact]
        public void GetConflicts_MasterConflictsWithSlave_ReturnsNonEmptyList()
        {
            var master = new DirectoryWrapper("master");
            master.CreateFile("1", new FileAttributes(1, DateTime.MinValue));
            master.CreateDirectory("a").CreateFile("2", new FileAttributes(1, DateTime.MinValue));

            var slave = new DirectoryWrapper("slave");
            slave.CreateFile("1", new FileAttributes(1, DateTime.MinValue));

            var collector = new ConflictsCollector(master, slave, new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer()));

            var conflicts = collector.GetConflicts();

            Assert.NotEmpty(conflicts);
        }

        [Fact]
        public void GetConflicts_OneMasterFileConflictsWithSlave_ReturnsOneConflict()
        {
            var master = new DirectoryWrapper("master");
            master.CreateFile("1", new FileAttributes(1, DateTime.MinValue));
            master.CreateDirectory("a").CreateFile("2", new FileAttributes(1, DateTime.MinValue));

            var slave = new DirectoryWrapper("slave");
            slave.CreateFile("1", new FileAttributes(1, DateTime.MinValue));

            var collector = new ConflictsCollector(master, slave, new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer()));

            var conflicts = collector.GetConflicts();

            Assert.Single(conflicts);
        }

        [Fact]
        public void GetConflicts_OneMasterFileConflictsWithSlave_Returns_NotNull_Null()
        {
            var master = new DirectoryWrapper("master");
            master.CreateFile("1", new FileAttributes(1, DateTime.MinValue));
            master.CreateDirectory("a").CreateFile("2", new FileAttributes(1, DateTime.MinValue));

            var slave = new DirectoryWrapper("slave");
            slave.CreateFile("1", new FileAttributes(1, DateTime.MinValue));
            slave.CreateDirectory("a");

            var collector = new ConflictsCollector(master, slave, new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer()));

            var conflicts = collector.GetConflicts();

            Assert.NotNull(conflicts[0].Source);
            Assert.Null(conflicts[0].Destination);
        }

        [Fact]
        public void GetConflicts_OneSlaveFileConflictsWithMaster_Returns_Null_NotNull()
        {
            var master = new DirectoryWrapper("master");
            master.CreateFile("1", new FileAttributes(1, DateTime.MinValue));
            master.CreateDirectory("a");

            var slave = new DirectoryWrapper("slave");
            slave.CreateFile("1", new FileAttributes(1, DateTime.MinValue));
            slave.CreateDirectory("a").CreateFile("2", new FileAttributes(1, DateTime.MinValue));

            var collector = new ConflictsCollector(master, slave, new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer()));

            var conflicts = collector.GetConflicts();

            Assert.Null(conflicts[0].Source);
            Assert.NotNull(conflicts[0].Destination);
        }

        [Fact]
        public void GetConflicts_MultipleConflicts()
        {
            var master = new DirectoryWrapper("master");
            var slave = new DirectoryWrapper("slave");

            var masterFile1 = master.CreateDirectory("a").CreateFile("1", new FileAttributes(1, DateTime.MinValue));
            var masterFile2 = master.CreateFile("2", new FileAttributes(3, DateTime.MaxValue));

            var slaveDirA = slave.CreateDirectory("a");
            var slaveFile1 = slaveDirA.CreateFile("1", new FileAttributes(2, DateTime.MaxValue));
            var slaveFile2 = slaveDirA.CreateFile("2", new FileAttributes(1, DateTime.MaxValue));


            var expected = new HashSet<Conflict>
            {
                new Conflict(slaveFile1, masterFile1),
                new Conflict(null, slaveFile2),
                new Conflict(masterFile2, null)
            };

            var collector = new ConflictsCollector(master, slave, new DefaultConflictDetectionPolicy(new DefaultFileSystemElementsComparer()));

            var actual = collector.GetConflicts().ToHashSet();

            Assert.Equal(expected, actual);
        }
    }
}