using MasterSlaveSync;
using System.IO.Abstractions.TestingHelpers;
using Xunit;
using System;
using System.Collections.Generic;

namespace MasterSlaveSyncTests
{
    public class SynchronizatorTests
    {
        [Fact]
        public void Constructor_MasterDirectoryDoesNotExist_Throws()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddDirectory(@"c:\slave");

            Assert.Throws<ArgumentException>(() => 
            _ = new Synchronizator(@"c:\master", @"c:\slave", new SyncOptions(), mockFileSystem));
            Assert.False(mockFileSystem.Directory.Exists(@"c:\master"));
        }

        [Fact]
        public void AddSlave_SlaveDirectoryDoesNotExist_CreatesSlave()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddDirectory(@"c:\master");

            _ = new Synchronizator(@"c:\master", @"c:\slave", new SyncOptions(), mockFileSystem);

            Assert.True(mockFileSystem.Directory.Exists(@"c:\slave"));
        }

        [Fact]
        public void AddSlave_MasterDirectoryContainsSlave_Throws()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddDirectory(@"c:\master\slave");

            Assert.Throws<ArgumentException>(() =>
            _ = new Synchronizator(@"c:\master", @"c:\master\slave", new SyncOptions(), mockFileSystem));
        }

        [Fact]
        public void AddSlave_SlaveDirectoryContainsMaster_Throws()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddDirectory(@"c:\slave\master");

            Assert.Throws<ArgumentException>(() =>
            _ = new Synchronizator(@"c:\slave\master", @"c:\slave", new SyncOptions(), mockFileSystem));
        }

        [Fact]
        public void Run_FileInSlaveButNotInMaster_NoDeleteOn_FileNotDeleted()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddFile(@"c:\slave\a.txt", new MockFileData("1"));
            mockFileSystem.AddDirectory(@"c:\master");

            var options = new SyncOptions().NoDeleteOn();
            var synchronizator = new Synchronizator(@"c:\master", @"c:\slave", options, mockFileSystem);

            synchronizator.Run();

            Assert.True(mockFileSystem.FileExists(@"c:\slave\a.txt"));
        }

        [Fact]
        public void Run_FileInSlaveButNotInMaster_NoDeleteOff_FileDeleted()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddFile(@"c:\slave\a.txt", new MockFileData("1"));
            mockFileSystem.AddDirectory(@"c:\master");

            var synchronizator = new Synchronizator(@"c:\master", @"c:\slave", new SyncOptions(), mockFileSystem);

            synchronizator.Run();

            Assert.False(mockFileSystem.FileExists(@"c:\slave\a.txt"));
        }

        [Fact]
        public void Run_DirectoryInSlaveButNotInMaster_NoDeleteOn_DirectoryNotDeleted()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddDirectory(@"c:\slave\sub");
            mockFileSystem.AddDirectory(@"c:\master");

            var options = new SyncOptions().NoDeleteOn();
            var synchronizator = new Synchronizator(@"c:\master", @"c:\slave", options, mockFileSystem);

            synchronizator.Run();

            Assert.True(mockFileSystem.Directory.Exists(@"c:\slave\sub"));
        }

        [Fact]
        public void Run_DirectoryInSlaveButNotInMaster_NoDeleteOff_DirectoryDeleted()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddDirectory(@"c:\slave\sub");
            mockFileSystem.AddDirectory(@"c:\master");

            var synchronizator = new Synchronizator(@"c:\master", @"c:\slave", new SyncOptions(), mockFileSystem);

            synchronizator.Run();

            Assert.False(mockFileSystem.Directory.Exists(@"c:\slave\sub"));
        }

        [Fact]
        public void Run_FileInMasterButNotInSlave_MasterFileCopiedToSlave()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddFile(@"c:\master\a.txt", new MockFileData("1"));
            mockFileSystem.AddDirectory(@"c:\slave");

            var synchronizator = new Synchronizator(@"c:\master", @"c:\slave", new SyncOptions(), mockFileSystem);

            synchronizator.Run();

            Assert.True(mockFileSystem.FileExists(@"c:\slave\a.txt"));
        }

        [Fact]
        public void Run_DirectoryInMasterButNotInSlave_MasterDirectoryCopiedToSlave()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddDirectory(@"c:\master\sub");
            mockFileSystem.AddDirectory(@"c:\slave");

            var synchronizator = new Synchronizator(@"c:\master", @"c:\slave", new SyncOptions(), mockFileSystem);

            synchronizator.Run();

            Assert.True(mockFileSystem.Directory.Exists(@"c:\slave\sub"));
        }

        [Fact]
        public void Run_SameNameFile_DifferentLength_SlaveFileUpdated()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddFile(@"c:\master\a.txt", new MockFileData("1"));
            mockFileSystem.AddFile(@"c:\slave\a.txt", new MockFileData("12"));

            var synchronizator = new Synchronizator(@"c:\master", @"c:\slave", new SyncOptions(), mockFileSystem);

            synchronizator.Run();

            Assert.Equal("1", mockFileSystem.File.ReadAllText(@"c:\slave\a.txt"));
            Assert.Equal("1", mockFileSystem.File.ReadAllText(@"c:\master\a.txt"));
        }

        [Fact]
        public void Run_SameNameFile_SameLength_SlaveFileNotUpdated()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddFile(@"c:\master\a.txt", new MockFileData("1"));
            mockFileSystem.AddFile(@"c:\slave\a.txt", new MockFileData("2"));

            var synchronizator = new Synchronizator(@"c:\master", @"c:\slave", new SyncOptions(), mockFileSystem);

            synchronizator.Run();

            Assert.Equal("2", mockFileSystem.File.ReadAllText(@"c:\slave\a.txt"));
            Assert.Equal("1", mockFileSystem.File.ReadAllText(@"c:\master\a.txt"));
        }

        [Fact]
        public void SummaryLogger_SameNameFile_WritesCorrectMessage()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddFile(@"c:\master\a.txt", new MockFileData("1"));
            mockFileSystem.AddFile(@"c:\slave\a.txt", new MockFileData("12"));

            using (var logFile = mockFileSystem.File.CreateText(@"c:\log.txt"))
            {
                var options = new SyncOptions().LogSummary(logFile.Write);
                var synchronizator = new Synchronizator(@"c:\master", @"c:\slave", options, mockFileSystem);

                synchronizator.Run();
            }

            var expected = "Updated \"a.txt\" file";

            Assert.Equal(expected, mockFileSystem.File.ReadAllText(@"c:\log.txt"));
        }

        [Fact]
        public void VerboseLogger_SameNameFile_WritesCorrectMessage()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddFile(@"c:\master\a.txt", new MockFileData("1"));
            mockFileSystem.AddFile(@"c:\slave\a.txt", new MockFileData("12"));

            using (var logFile = mockFileSystem.File.CreateText(@"c:\log.txt"))
            {
                var options = new SyncOptions().LogVerbose(logFile.Write);
                var synchronizator = new Synchronizator(@"c:\master", @"c:\slave", options, mockFileSystem);

                synchronizator.Run();
            }

            var expected = "Updated \"a.txt\" file";

            Assert.Equal(expected, mockFileSystem.File.ReadAllText(@"c:\log.txt"));
        }

        [Fact]
        public void SummaryLogger_DirectoryInMasterButNotInSlave_WritesCorrectMessage()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddDirectory(@"c:\master\sub");
            mockFileSystem.AddDirectory(@"c:\slave");

            using (var logFile = mockFileSystem.File.CreateText(@"c:\log.txt"))
            {
                var options = new SyncOptions().LogSummary(logFile.Write);
                var synchronizator = new Synchronizator(@"c:\master", @"c:\slave", options, mockFileSystem);

                synchronizator.Run();
            }

            var expected = "Copied \"sub\" directory";

            Assert.Equal(expected, mockFileSystem.File.ReadAllText(@"c:\log.txt"));
        }

        [Fact]
        public void VerboseLogger_DirectoryInMasterButNotInSlave_WritesCorrectMessage()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddDirectory(@"c:\master\sub");
            mockFileSystem.AddDirectory(@"c:\slave");

            using (var logFile = mockFileSystem.File.CreateText(@"c:\log.txt"))
            {
                var options = new SyncOptions().LogVerbose(logFile.Write);
                var synchronizator = new Synchronizator(@"c:\master", @"c:\slave", options, mockFileSystem);

                synchronizator.Run();
            }

            var expected = "Copied \"sub\" directory to c:\\slave";

            Assert.Equal(expected, mockFileSystem.File.ReadAllText(@"c:\log.txt"));
        }

        [Fact]
        public void SummaryLogger_DirectoryInSlaveButNotInMaster_NoDeleteOff_WritesCorrectMessage()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddDirectory(@"c:\slave\sub");
            mockFileSystem.AddDirectory(@"c:\master");

            using (var logFile = mockFileSystem.File.CreateText(@"c:\log.txt"))
            {
                var options = new SyncOptions().LogSummary(logFile.Write);
                var synchronizator = new Synchronizator(@"c:\master", @"c:\slave", options, mockFileSystem);

                synchronizator.Run();
            }

            var expected = "Deleted \"sub\" directory";

            Assert.Equal(expected, mockFileSystem.File.ReadAllText(@"c:\log.txt"));
        }

        [Fact]
        public void VerboseLogger_DirectoryInSlaveButNotInMaster_NoDeleteOff_WritesCorrectMessage()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddDirectory(@"c:\slave\sub");
            mockFileSystem.AddDirectory(@"c:\master");

            using (var logFile = mockFileSystem.File.CreateText(@"c:\log.txt"))
            {
                var options = new SyncOptions().LogVerbose(logFile.Write);
                var synchronizator = new Synchronizator(@"c:\master", @"c:\slave", options, mockFileSystem);

                synchronizator.Run();
            }

            var expected = "Deleted \"sub\" directory from c:\\slave";

            Assert.Equal(expected, mockFileSystem.File.ReadAllText(@"c:\log.txt"));
        }

        [Fact]
        public void SummaryLogger_DirectoryInSlaveButNotInMaster_NoDeleteOn_WritesCorrectMessage()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddDirectory(@"c:\slave\sub");
            mockFileSystem.AddDirectory(@"c:\master");

            using (var logFile = mockFileSystem.File.CreateText(@"c:\log.txt"))
            {
                var options = new SyncOptions().LogSummary(logFile.Write).NoDeleteOn();
                var synchronizator = new Synchronizator(@"c:\master", @"c:\slave", options, mockFileSystem);

                synchronizator.Run();
            }

            var expected = "";

            Assert.Equal(expected, mockFileSystem.File.ReadAllText(@"c:\log.txt"));
        }

        [Fact]
        public void VerboseLogger_DirectoryInSlaveButNotInMaster_NoDeleteOn_WritesCorrectMessage()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddDirectory(@"c:\slave\sub");
            mockFileSystem.AddDirectory(@"c:\master");

            using (var logFile = mockFileSystem.File.CreateText(@"c:\log.txt"))
            {
                var options = new SyncOptions().LogVerbose(logFile.Write).NoDeleteOn();
                var synchronizator = new Synchronizator(@"c:\master", @"c:\slave", options, mockFileSystem);

                synchronizator.Run();
            }

            var expected = "";

            Assert.Equal(expected, mockFileSystem.File.ReadAllText(@"c:\log.txt"));
        }

        [Fact]
        public void SummaryLogger_FileInMasterButNotInSlave_WritesCorrectMessage()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddFile(@"c:\master\a.txt", new MockFileData("1"));
            mockFileSystem.AddDirectory(@"c:\slave");

            using (var logFile = mockFileSystem.File.CreateText(@"c:\log.txt"))
            {
                var options = new SyncOptions().LogSummary(logFile.Write);
                var synchronizator = new Synchronizator(@"c:\master", @"c:\slave", options, mockFileSystem);

                synchronizator.Run();
            }

            var expected = "Copied \"a.txt\" file";

            Assert.Equal(expected, mockFileSystem.File.ReadAllText(@"c:\log.txt"));
        }

        [Fact]
        public void VerboseLogger_FileInMasterButNotInSlave_WritesCorrectMessage()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddFile(@"c:\master\a.txt", new MockFileData("1"));
            mockFileSystem.AddDirectory(@"c:\slave");

            using (var logFile = mockFileSystem.File.CreateText(@"c:\log.txt"))
            {
                var options = new SyncOptions().LogVerbose(logFile.Write);
                var synchronizator = new Synchronizator(@"c:\master", @"c:\slave", options, mockFileSystem);

                synchronizator.Run();
            }

            var expected = "Copied \"a.txt\" file to c:\\slave";

            Assert.Equal(expected, mockFileSystem.File.ReadAllText(@"c:\log.txt"));
        }

        [Fact]
        public void SummaryLogger_FileInSlaveButNotInMaster_NoDeleteOff_WritesCorrectMessage()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddFile(@"c:\slave\a.txt", new MockFileData("1"));
            mockFileSystem.AddDirectory(@"c:\master");

            using (var logFile = mockFileSystem.File.CreateText(@"c:\log.txt"))
            {
                var options = new SyncOptions().LogSummary(logFile.Write);
                var synchronizator = new Synchronizator(@"c:\master", @"c:\slave", options, mockFileSystem);

                synchronizator.Run();
            }

            var expected = "Deleted \"a.txt\" file";

            Assert.Equal(expected, mockFileSystem.File.ReadAllText(@"c:\log.txt"));
        }

        [Fact]
        public void VerboseLogger_FileInSlaveButNotInMaster_NoDeleteOff_WritesCorrectMessage()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddFile(@"c:\slave\a.txt", new MockFileData("1"));
            mockFileSystem.AddDirectory(@"c:\master");

            using (var logFile = mockFileSystem.File.CreateText(@"c:\log.txt"))
            {
                var options = new SyncOptions().LogVerbose(logFile.Write);
                var synchronizator = new Synchronizator(@"c:\master", @"c:\slave", options, mockFileSystem);

                synchronizator.Run();
            }

            var expected = "Deleted \"a.txt\" file from c:\\slave";

            Assert.Equal(expected, mockFileSystem.File.ReadAllText(@"c:\log.txt"));
        }

        [Fact]
        public void SummaryLogger_FileInSlaveButNotInMaster_NoDeleteOn_WritesCorrectMessage()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddFile(@"c:\slave\a.txt", new MockFileData("1"));
            mockFileSystem.AddDirectory(@"c:\master");

            using (var logFile = mockFileSystem.File.CreateText(@"c:\log.txt"))
            {
                var options = new SyncOptions().LogSummary(logFile.Write).NoDeleteOn();
                var synchronizator = new Synchronizator(@"c:\master", @"c:\slave", options, mockFileSystem);

                synchronizator.Run();
            }

            var expected = "";

            Assert.Equal(expected, mockFileSystem.File.ReadAllText(@"c:\log.txt"));
        }

        [Fact]
        public void VerboseLogger_FileInSlaveButNotInMaster_NoDeleteOn_WritesCorrectMessage()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddFile(@"c:\slave\a.txt", new MockFileData("1"));
            mockFileSystem.AddDirectory(@"c:\master");

            using (var logFile = mockFileSystem.File.CreateText(@"c:\log.txt"))
            {
                var options = new SyncOptions().LogVerbose(logFile.Write).NoDeleteOn();
                var synchronizator = new Synchronizator(@"c:\master", @"c:\slave", options, mockFileSystem);

                synchronizator.Run();
            }

            var expected = "";

            Assert.Equal(expected, mockFileSystem.File.ReadAllText(@"c:\log.txt"));
        }

        [Fact]
        public void AddSlave_MultipleSlaves_Success()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddDirectory(@"c:\master");
            mockFileSystem.AddDirectory(@"c:\slave");
            mockFileSystem.AddDirectory(@"c:\slave2");
            mockFileSystem.AddDirectory(@"c:\slave3");

            var synchronizator = new Synchronizator(@"c:\master", @"c:\slave",
                new SyncOptions(), mockFileSystem);
            synchronizator.AddSlave(@"c:\slave2");
            synchronizator.AddSlave(@"c:\slave3");

            var expected = new List<string>() { @"c:\slave", @"c:\slave2", @"c:\slave3" };

            Assert.Equal(expected, synchronizator.SlavePaths);
        }

        [Fact]
        public void TwoSlaves_FileInMasterButNotInSlaves_CopiesFileToBothSlaves()
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddFile(@"c:\master\a.txt", new MockFileData("1"));
            mockFileSystem.AddDirectory(@"c:\slave");
            mockFileSystem.AddDirectory(@"c:\slave2");

            var synchronizator = new Synchronizator(@"c:\master", @"c:\slave", 
                new SyncOptions(), mockFileSystem);
            synchronizator.AddSlave(@"c:\slave2");

            synchronizator.Run();

            Assert.True(mockFileSystem.FileExists(@"c:\slave\a.txt"));
            Assert.True(mockFileSystem.FileExists(@"c:\slave2\a.txt"));
        }
    }
}
