using System.Collections.Generic;
using Xunit;

namespace SynchLibrary.Tests
{
    public class MyTestsForSync
    {
        [Fact]
        public void IntersectionIsEmpty()
        {
            ILogger logger = LoggerFactory.Create(0);
            var sync = SynchronizerFactory.Create("true");
            var master = new List<FileWrapper> { new FileWrapper("file1.txt") };
            var slave = new List<FileWrapper> { new FileWrapper("file2.txt") };
            sync.SetUpBaseCollections(master , slave);
            Assert.Empty(sync.Intersection);
        }

        [Fact]

        public void IntersectionWork()
        {
            ILogger logger = LoggerFactory.Create(0);
            var sync = SynchronizerFactory.Create("true");
            var master = new List<FileWrapper> { new FileWrapper("file1.txt") };
            var slave = new List<FileWrapper> { new FileWrapper("file1.txt") };
            sync.SetUpBaseCollections(master , slave);
            Assert.Single(sync.Intersection);
        }

        [Fact]

        public void MasterWithoutSlaveWork()
        {
            ILogger logger = LoggerFactory.Create(0);
            var sync = SynchronizerFactory.Create("true");
            var master = new List<FileWrapper> { new FileWrapper("file1.txt") };
            var slave = new List<FileWrapper> { new FileWrapper("file2.txt") };
            sync.SetUpBaseCollections(master , slave);
            Assert.Single(sync.MasterWithoutSlave);
        }

        [Fact]
        public void SlaveWithoutMasterWork()
        {
            ILogger logger = LoggerFactory.Create(0);
            var sync = SynchronizerFactory.Create("true");
            var master = new List<FileWrapper> { new FileWrapper("file1.txt") };
            var slave = new List<FileWrapper> { new FileWrapper("file2.txt") };
            sync.SetUpBaseCollections(master , slave);
            Assert.Single(sync.SlaveWithoutMaster);
        }
    }
}
