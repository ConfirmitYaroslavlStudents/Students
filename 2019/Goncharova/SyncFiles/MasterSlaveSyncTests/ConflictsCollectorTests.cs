using MasterSlaveSync;
using MasterSlaveSync.Conflicts;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using Xunit;

namespace MasterSlaveSyncTests
{
    public class ConflictsCollectorTests
    {
        [Fact]
        public void Collector_NoConflicts_ReturnsEmptyList()
        {
            var mockFileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"c:\master\a.txt", new MockFileData("1") },
                { @"c:\slave\a.txt", new MockFileData("1") }
            });

            ConflictsCollector conflictsCollector = new ConflictsCollector(mockFileSystem);
            var actual = conflictsCollector.CollectConflicts(
                mockFileSystem.DirectoryInfo.FromDirectoryName(@"c:\master"),
                mockFileSystem.DirectoryInfo.FromDirectoryName(@"c:\slave")
                );

            Assert.Empty(actual.fileConflicts);

        }

        [Fact]
        public void Collector_MasterHasFileSlaveDoesNot_ReturnsCorrectConflict()
        {
            var mockFileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"c:\master\a.txt", new MockFileData("1") },
            });
            mockFileSystem.AddDirectory(@"c:\slave");

            ConflictsCollector conflictsCollector = new ConflictsCollector(mockFileSystem);
            var actual = conflictsCollector.CollectConflicts(
                mockFileSystem.DirectoryInfo.FromDirectoryName(@"c:\master"),
                mockFileSystem.DirectoryInfo.FromDirectoryName(@"c:\slave")
                );

            var expected = new FileConflict(mockFileSystem.FileInfo.FromFileName(@"c:\master\a.txt"), null);

            Assert.Single(actual.fileConflicts);
            Assert.Equal(expected, actual.fileConflicts[0]);

        }
    }
}
