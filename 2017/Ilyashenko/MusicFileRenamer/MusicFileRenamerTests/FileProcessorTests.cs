using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MusicFileRenamerLib;

namespace MusicFileRenamerTests
{
    [TestClass]
    public class FileProcessorTests
    {
        FileProcessor fileProcessor;

        [TestInitialize]
        public void TestInitialize()
        {
            fileProcessor = new FileProcessor(new TestableFileSystem());
        }

        [TestMethod]
        public void TryMakeFileName()
        {
            var file = new Mp3File("sample.mp3");
            file.Artist = "Peter Gabriel";
            file.Title = "Red Rain";

            fileProcessor.MakeFilename(file);

            Assert.AreEqual(@"\Peter Gabriel - Red Rain.mp3", file.path);
        }

        [TestMethod]
        public void TryMakeTags()
        {
            var file = new Mp3File(@"Styx - Boat On The River.mp3");

            fileProcessor.MakeTags(file);

            Assert.AreEqual("Styx", file.Artist);
            Assert.AreEqual("Boat On The River", file.Title);
        }
    }
}
