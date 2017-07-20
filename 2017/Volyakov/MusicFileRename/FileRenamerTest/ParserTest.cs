using System;
using TagLib;
using MusicFileRenameLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileRenamerTest
{
    [TestClass]
    public class ParserTest
    {
        private Parser parser = new Parser();

        [TestMethod]
        public void ParseFileName()
        {
            string fileName = @"test\artist-title.mp3";
            var expected = new FileShell();
            expected.FullFilePath = @"test\artist-title.mp3";
            expected.Path = @"test\";
            expected.Artist = "artist";
            expected.Title = "title";
            expected.Extension = ".mp3";
            var actual = parser.ParseFile(fileName);
            Assert.AreEqual(expected.FullFilePath, actual.FullFilePath);
            Assert.AreEqual(expected.Path, actual.Path);
            Assert.AreEqual(expected.Artist, actual.Artist);
            Assert.AreEqual(expected.Title, actual.Title);
            Assert.AreEqual(expected.Extension, actual.Extension);
        }

        [TestMethod]
        public void GetExtension()
        {
            var actual = parser.GetFileExtension("*ar.mp3");
            var expected = ".mp3";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CollectFilePath()
        {
            FileShell fileShell = new FileShell();
            fileShell.FullFilePath = "asasasas";
            fileShell.Path = @"\test\dir\";
            fileShell.Artist = "Cat";
            fileShell.Title = "Dog";
            fileShell.Extension = ".mp3";
            parser.CollectFilePath(fileShell);
            var expected = @"\test\dir\Cat-Dog.mp3";
            Assert.AreEqual(expected, fileShell.FullFilePath);
        }

        [TestMethod]
        [ExpectedException (typeof(ArgumentException))]
        public void WrongGetExtension()
        {
            parser.GetFileExtension(".mp");
        }
    }
}
