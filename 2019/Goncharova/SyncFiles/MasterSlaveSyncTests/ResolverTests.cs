using MasterSlaveSync;
using MasterSlaveSync.Conflicts;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using Xunit;

namespace MasterSlaveSyncTests
{
    public class ResolverTests
    {
        [Fact]
        public void ResolveConflicts_FileInSlaveButNotInMaster_NoDeleteOff_SlaveFileDeleted()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddFile(@"c:\slave\a.txt", new MockFileData("1"));
            mockFileSystem.AddDirectory(@"c:\master");

            List<FileConflict> fileConflicts = new List<FileConflict>
            {
                new FileConflict(null, mockFileSystem.FileInfo.FromFileName(@"c:\slave\a.txt"))
            };

            var resolver = new Resolver();
            resolver.ResolveConflicts(new ConflictsCollection(fileConflicts), @"c:\master", @"c:\slave");

            Assert.False(mockFileSystem.FileExists(@"c:\slave\a.txt"));
        }

        [Fact]
        public void ResolveConflicts_FileInMasterButNotInSlave_MasterFileCopied()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddFile(@"c:\master\a.txt", new MockFileData("1"));
            mockFileSystem.AddDirectory(@"c:\slave");

            List<FileConflict> fileConflicts = new List<FileConflict>
            {
                new FileConflict(mockFileSystem.FileInfo.FromFileName(@"c:\master\a.txt"), null)
            };

            var resolver = new Resolver();
            resolver.ResolveConflicts(new ConflictsCollection(fileConflicts), @"c:\master", @"c:\slave");

            Assert.True(mockFileSystem.FileExists(@"c:\slave\a.txt"));
        }

        [Fact]
        public void ResolveConflicts_SameNameFileDifferentContent_SlaveFileUpdated()
        {
            var expected = "1";

            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddFile(@"c:\master\a.txt", new MockFileData(expected));
            mockFileSystem.AddFile(@"c:\slave\a.txt", new MockFileData("12"));

            List<FileConflict> fileConflicts = new List<FileConflict>
            {
                new FileConflict(mockFileSystem.FileInfo.FromFileName(@"c:\master\a.txt"),
                mockFileSystem.FileInfo.FromFileName(@"c:\slave\a.txt"))
            };

            var resolver = new Resolver();
            resolver.ResolveConflicts(new ConflictsCollection(fileConflicts), @"c:\master", @"c:\slave");

            Assert.Equal(expected, mockFileSystem.File.ReadAllText(@"c:\slave\a.txt"));
            Assert.Equal(expected, mockFileSystem.File.ReadAllText(@"c:\master\a.txt"));
        }

        [Fact]
        public void ResolveConflicts_DirectoryInSlaveButNotInMaster_NoDeleteOff_SlaveDirectoryDeleted()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddDirectory(@"c:\slave\sub");
            mockFileSystem.AddDirectory(@"c:\master");

            List<DirectoryConflict> directoryConflicts = new List<DirectoryConflict>
            {
                new DirectoryConflict(null, mockFileSystem.DirectoryInfo.FromDirectoryName(@"c:\slave\sub"))
            };

            var resolver = new Resolver();
            resolver.ResolveConflicts(new ConflictsCollection(directoryConflicts), @"c:\master", @"c:\slave");

            Assert.False(mockFileSystem.Directory.Exists(@"c:\slave\sub"));
        }

        [Fact]
        public void ResolveConflicts_DirectoryInMasterButNotInSlave_MasterDirectoryCopied()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddDirectory(@"c:\slave");
            mockFileSystem.AddFile(@"c:\master\sub\a.txt", new MockFileData("1"));

            List<DirectoryConflict> directoryConflicts = new List<DirectoryConflict>
            {
                new DirectoryConflict(mockFileSystem.DirectoryInfo.FromDirectoryName(@"c:\master\sub"), null)
            };

            var resolver = new Resolver();
            resolver.ResolveConflicts(new ConflictsCollection(directoryConflicts), @"c:\master", @"c:\slave");

            Assert.True(mockFileSystem.Directory.Exists(@"c:\slave\sub"));
            Assert.True(mockFileSystem.FileExists(@"c:\slave\sub\a.txt"));
        }

    }
}
