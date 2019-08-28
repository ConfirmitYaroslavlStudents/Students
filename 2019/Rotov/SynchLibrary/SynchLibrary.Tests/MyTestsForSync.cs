using System.Collections.Generic;
using GeneralizeSynchLibrary;
using Xunit;

namespace GeneralizeSynchLibrary.Tests
{
    public class MyTestsForSync
    {
        [Fact]
        public void IntersectionIsEmpty()
        {
            var parsedArgs = new ArgParser(new string[] { "--no-delete" });
            var synch = SynchronizerFactory.Create(parsedArgs.NoDelete);
            var collection = new FileWrapperCollection(new List<FileWrapper>
            {
                new FileWrapper(0, "master", "file1.txt"),
                new FileWrapper(1, "slave", "file2.txt")
            });
            var output = synch.Synchronize(collection);
            Assert.Empty(output.GetReplaceList());
        }

        [Fact]

        public void IntersectionWork()
        {
            var parsedArgs = new ArgParser(new string[] { "" });
            var synch = SynchronizerFactory.Create(parsedArgs.NoDelete);
            var collection = new FileWrapperCollection(new List<FileWrapper>
            {
                new FileWrapper(0, "master", "file1.txt"),
                new FileWrapper(1, "slave", "file1.txt")
            });
            var output = synch.Synchronize(collection);
            Assert.True(output.GetReplaceList().Count == 1);
        }

        [Fact]

        public void DeleteModWork()
        {
            var parsedArgs = new ArgParser(new string[] { "" });
            var synch = SynchronizerFactory.Create(parsedArgs.NoDelete);
            var collection = new FileWrapperCollection(new List<FileWrapper>
            {
                new FileWrapper(0, "master", "file1.txt"),
                new FileWrapper(1, "slave", "file1.txt"),
                new FileWrapper(1, "slave", "file2.txt"),
                new FileWrapper(1, "slave", "file3.txt")
            });
            var output = synch.Synchronize(collection);
            Assert.True(output.GetRemoveList().Count == 2);
        }

        [Fact]
        public void ReplaceWork()
        {
            var parsedArgs = new ArgParser(new string[] { "--no-delete" });
            var synch = SynchronizerFactory.Create(parsedArgs.NoDelete);
            var collection = new FileWrapperCollection(new List<FileWrapper>
            {
                new FileWrapper(0, "master", "file1.txt"),
                new FileWrapper(1, "slave", "file1.txt"),
                new FileWrapper(1, "slave", "file2.txt"),
                new FileWrapper(1, "slave", "file3.txt"),
                new FileWrapper(2, "slave", "file3.txt")
            });
            var output = synch.Synchronize(collection);
            Assert.True(output.GetReplaceList().Count == 2);
        }
    }
}
