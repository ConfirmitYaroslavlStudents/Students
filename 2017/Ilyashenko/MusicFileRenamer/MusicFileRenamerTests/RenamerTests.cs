using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MusicFileRenamerLib;

namespace MusicFileRenamerTests
{
    [TestClass]
    public class RenamerTests
    {
        [TestMethod]
        public void TryMakeFilename()
        {
            var file = new Mp3File("sample.mp3");
            file.Artist = "Peter Gabriel";
            file.Title = "Red Rain";

            var args = (new ArgumentParser()).Parse(new string[] { "*.mp3", "-recursive", "-toFileName" });

            var renamer = new Renamer(args, "", new FileProcessor(new TestableFileSystem()));
            renamer.Rename(file);

            Assert.AreEqual(@"\Peter Gabriel - Red Rain.mp3", file.path);
        }

        [TestMethod]
        public void TryMakeTags()
        {
            var file = new Mp3File(@"Styx - Boat On The River.mp3");

            var args = (new ArgumentParser()).Parse(new string[] { "*.mp3", "-recursive", "-toTag" });

            var renamer = new Renamer(args, "", new FileProcessor(new TestableFileSystem()));
            renamer.Rename(file);

            Assert.AreEqual("Styx", file.Artist);
            Assert.AreEqual("Boat On The River", file.Title);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TryMakeTagsWithWrongFilename()
        {
            var file = new Mp3File("sample.mp3");
            var args = (new ArgumentParser()).Parse(new string[] { "*.mp3", "-recursive", "-toTag" });

            var renamer = new Renamer(args, "", new FileProcessor(new TestableFileSystem()));
            renamer.Rename(file);
        }
    }
}
