using FolderLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mp3Handler;

namespace Tests
{
    [TestClass]
    public class FolderProcessorTests
    {
        [TestMethod]
        public void NoDiffTests()
        {
            var fileSystem = new FileSystemBuilder().
                File(new FileBuilder().
                    FilePath("artist - song").
                    Tag(FrameType.Artist, "artist").
                    Tag(FrameType.Title, "song").
                    Build()).
                File(new FileBuilder().
                    FilePath("art - so").
                    Tag(FrameType.Artist, "art").
                    Tag(FrameType.Title, "so").
                    Build()).
                Build();
            var folderProcessor = new FolderProcessor(fileSystem,fileSystem);

            var differences = folderProcessor.GetDifferences("", "<ar> - <ti>");

            Assert.AreEqual(0,differences.Count);
        }

        [TestMethod]
        public void OneTagDiffTests()
        {
            var first = "artist - song";
            var sec = "art - so";
            var fileSystem = new FileSystemBuilder().
                File(new FileBuilder().
                    FilePath(first).
                    Tag(FrameType.Artist, "artist").
                    Tag(FrameType.Title, "title").
                    Build()).
                File(new FileBuilder().
                    FilePath(sec).
                    Tag(FrameType.Artist, "art").
                    Tag(FrameType.Title, "title").
                    Build()).
                Build();
            var folderProcessor = new FolderProcessor(fileSystem, fileSystem);

            var differences = folderProcessor.GetDifferences("", "<ar> - <ti>");

            Assert.AreEqual(2, differences.Count);
        }

        //[TestMethod]
        //public void Synch()
        //{
        //    var first = "artist - song";
        //    var sec = "art - so";
        //    var fileSystem = new FileSystemBuilder().
        //        File(new FileBuilder().
        //            FilePath(first).
        //            Tag(FrameType.Artist, "artist").
        //            Tag(FrameType.Title, "title").
        //            Build()).
        //        File(new FileBuilder().
        //            FilePath(sec).
        //            Tag(FrameType.Artist, "art").
        //            Tag(FrameType.Title, "title").
        //            Build()).
        //        Build();
        //    var folderProcessor = new FolderProcessor(fileSystem, fileSystem);

        //    var differences = folderProcessor.GetDifferences("", "<ar> - <ti>");

        //    Assert.AreEqual(2, differences.Count);
        //}
    }
}
