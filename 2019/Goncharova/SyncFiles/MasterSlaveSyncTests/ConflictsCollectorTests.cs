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

            ConflictsCollector conflictsCollector = new ConflictsCollector();
            var actual = conflictsCollector.CollectConflicts(
                mockFileSystem.DirectoryInfo.FromDirectoryName(@"c:\master"),
                mockFileSystem.DirectoryInfo.FromDirectoryName(@"c:\slave")
                );

            Assert.Empty(actual.FileConflicts);
            Assert.Empty(actual.DirectoryConflicts);
        }

        [Fact]
        public void Collector_MasterHasFileSlaveDoesNot_ReturnsCorrectConflict()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddFile(@"c:\master\a.txt", new MockFileData("1"));
            mockFileSystem.AddDirectory(@"c:\slave");

            ConflictsCollector conflictsCollector = new ConflictsCollector();
            var actual = conflictsCollector.CollectConflicts(
                mockFileSystem.DirectoryInfo.FromDirectoryName(@"c:\master"),
                mockFileSystem.DirectoryInfo.FromDirectoryName(@"c:\slave")
                );

            var expected = new FileConflict(mockFileSystem.FileInfo.FromFileName(@"c:\master\a.txt"), null);

            Assert.Single(actual.FileConflicts);
            Assert.Empty(actual.DirectoryConflicts);
            Assert.Equal(expected, actual.FileConflicts[0]);
        }

        [Fact]
        public void Collector_SlaveHasFileMasterDoesNot_ReturnsCorrectConflict()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddFile(@"c:\slave\a.txt", new MockFileData("1"));
            mockFileSystem.AddDirectory(@"c:\master");

            ConflictsCollector conflictsCollector = new ConflictsCollector();
            var actual = conflictsCollector.CollectConflicts(
                mockFileSystem.DirectoryInfo.FromDirectoryName(@"c:\master"),
                mockFileSystem.DirectoryInfo.FromDirectoryName(@"c:\slave")
                );

            var expected = new FileConflict(null, mockFileSystem.FileInfo.FromFileName(@"c:\slave\a.txt"));

            Assert.Single(actual.FileConflicts);
            Assert.Empty(actual.DirectoryConflicts);
            Assert.Equal(expected, actual.FileConflicts[0]);
        }

        [Fact]
        public void Collector_SameNameFileDifferentContent_ReturnsCorrectConflict()
        {
            var mockFileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"c:\slave\a.txt", new MockFileData("1") },
                { @"c:\master\a.txt", new MockFileData("12") }
            });

            ConflictsCollector conflictsCollector = new ConflictsCollector();
            var actual = conflictsCollector.CollectConflicts(
                mockFileSystem.DirectoryInfo.FromDirectoryName(@"c:\master"),
                mockFileSystem.DirectoryInfo.FromDirectoryName(@"c:\slave")
                );

            var expected = new FileConflict(mockFileSystem.FileInfo.FromFileName(@"c:\master\a.txt"),
                mockFileSystem.FileInfo.FromFileName(@"c:\slave\a.txt"));

            Assert.Single(actual.FileConflicts);
            Assert.Empty(actual.DirectoryConflicts);
            Assert.Equal(expected, actual.FileConflicts[0]);
        }

        [Fact]
        public void Collector_MasterHasSubDirectorySlaveDoesNot_ReturnsCorrectConflict()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddDirectory(@"c:\slave");
            mockFileSystem.AddDirectory(@"c:\master\sub");

            ConflictsCollector conflictsCollector = new ConflictsCollector();
            var actual = conflictsCollector.CollectConflicts(
                mockFileSystem.DirectoryInfo.FromDirectoryName(@"c:\master"),
                mockFileSystem.DirectoryInfo.FromDirectoryName(@"c:\slave")
                );

            var expected = new DirectoryConflict(
                mockFileSystem.DirectoryInfo.FromDirectoryName(@"c:\master\sub"),
                null);

            Assert.Empty(actual.FileConflicts);
            Assert.Single(actual.DirectoryConflicts);
            Assert.Equal(expected, actual.DirectoryConflicts[0]);
        }

        [Fact]
        public void Collector_SlaveHasSubDirectoryMasterDoesNot_ReturnsCorrectConflict()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddDirectory(@"c:\master");
            mockFileSystem.AddDirectory(@"c:\slave\sub");

            ConflictsCollector conflictsCollector = new ConflictsCollector();
            var actual = conflictsCollector.CollectConflicts(
                mockFileSystem.DirectoryInfo.FromDirectoryName(@"c:\master"),
                mockFileSystem.DirectoryInfo.FromDirectoryName(@"c:\slave")
                );

            var expected = new DirectoryConflict(
                null,
                mockFileSystem.DirectoryInfo.FromDirectoryName(@"c:\slave\sub"));

            Assert.Empty(actual.FileConflicts);
            Assert.Single(actual.DirectoryConflicts);
            Assert.Equal(expected, actual.DirectoryConflicts[0]);
        }

        [Fact]
        public void Collector_MasterHasFileSlaveDoesNotInSubDir_ReturnsCorrectConflict()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddFile(@"c:\master\sub\a.txt", new MockFileData("1"));
            mockFileSystem.AddDirectory(@"c:\slave\sub");

            ConflictsCollector conflictsCollector = new ConflictsCollector();
            var actual = conflictsCollector.CollectConflicts(
                mockFileSystem.DirectoryInfo.FromDirectoryName(@"c:\master"),
                mockFileSystem.DirectoryInfo.FromDirectoryName(@"c:\slave")
                );

            var expected = new FileConflict(
                mockFileSystem.FileInfo.FromFileName(@"c:\master\sub\a.txt"),
                null);

            Assert.Empty(actual.DirectoryConflicts);
            Assert.Single(actual.FileConflicts);
            Assert.Equal(expected, actual.FileConflicts[0]);
        }

        [Fact]
        public void Collector_SlaveHasFileMasterDoesNotInSubDir_ReturnsCorrectConflict()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddFile(@"c:\slave\sub\a.txt", new MockFileData("1"));
            mockFileSystem.AddDirectory(@"c:\master\sub");

            ConflictsCollector conflictsCollector = new ConflictsCollector();
            var actual = conflictsCollector.CollectConflicts(
                mockFileSystem.DirectoryInfo.FromDirectoryName(@"c:\master"),
                mockFileSystem.DirectoryInfo.FromDirectoryName(@"c:\slave")
                );

            var expected = new FileConflict(
                null,
                mockFileSystem.FileInfo.FromFileName(@"c:\slave\sub\a.txt"));

            Assert.Empty(actual.DirectoryConflicts);
            Assert.Single(actual.FileConflicts);
            Assert.Equal(expected, actual.FileConflicts[0]);
        }

        [Fact]
        public void Collector_SameNameFileDifferentContentInSubDir_ReturnsCorrectConflict()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddFile(@"c:\slave\sub\a.txt", new MockFileData("1"));
            mockFileSystem.AddFile(@"c:\master\sub\a.txt", new MockFileData("12"));

            ConflictsCollector conflictsCollector = new ConflictsCollector();
            var actual = conflictsCollector.CollectConflicts(
                mockFileSystem.DirectoryInfo.FromDirectoryName(@"c:\master"),
                mockFileSystem.DirectoryInfo.FromDirectoryName(@"c:\slave")
                );

            var expected = new FileConflict(
                mockFileSystem.FileInfo.FromFileName(@"c:\master\sub\a.txt"),
                mockFileSystem.FileInfo.FromFileName(@"c:\slave\sub\a.txt"));

            Assert.Empty(actual.DirectoryConflicts);
            Assert.Single(actual.FileConflicts);
            Assert.Equal(expected, actual.FileConflicts[0]);
        }
    }
}
