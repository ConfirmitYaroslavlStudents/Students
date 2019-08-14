using MasterSlaveSync;
using System.IO.Abstractions.TestingHelpers;
using Xunit;

namespace MasterSlaveSyncTests
{
    public class ConflictsCollectorTests
    {
        [Fact]
        public void Collector_NoConflicts_ReturnsEmptyList()
        {
            var mockFileSystem = new MockFileSystem();

            mockFileSystem.AddDirectory(@"A:\Master");
            mockFileSystem.AddDirectory(@"A:\Slave");
            mockFileSystem.AddFile(@"A:\Master\b.txt", new MockFileData("b"));
            mockFileSystem.AddFile(@"A:\Slave\b.txt", new MockFileData("b"));

            ConflictsCollector conflictsCollector = new ConflictsCollector(mockFileSystem);
            var actual = conflictsCollector.CollectConflicts(
                mockFileSystem.DirectoryInfo.FromDirectoryName(@"A:\Master"),
                mockFileSystem.DirectoryInfo.FromDirectoryName(@"A:\Slave")
                );

            Assert.Empty(actual);

        }

        [Fact]
        public void Collector_OneConflict_ReturnsOne()
        {
            var mockFileSystem = new MockFileSystem();

            mockFileSystem.AddDirectory(@"A:\Master");
            mockFileSystem.AddDirectory(@"A:\Slave");
            mockFileSystem.AddFile(@"A:\Master\b.txt", new MockFileData("b"));

            ConflictsCollector conflictsCollector = new ConflictsCollector(mockFileSystem);
            var actual = conflictsCollector.CollectConflicts(
                mockFileSystem.DirectoryInfo.FromDirectoryName(@"A:\Master"),
                mockFileSystem.DirectoryInfo.FromDirectoryName(@"A:\Slave")
                );

            Assert.Single(actual);

        }
    }
}
