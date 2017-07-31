using System;
using MusicFileRenameLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileRenamerTest
{
    [TestClass]
    public class ParserTest
    {
        private Parser parser = new Parser();

        [TestMethod]
        public void ParseNormalFileName()
        {
            string fileName = @"test\artist-title.mp3";
            var expectedFullFilePath = @"test\artist-title.mp3";
            var expectedPath = @"test\";
            var expectedExtension = ".mp3";
            var expectedArtist = "artist";
            var expectedTitle = "title";
            var actual = parser.ParseFile(fileName);

            Assert.AreEqual(expectedFullFilePath, actual[0]);
            Assert.AreEqual(expectedPath, actual[1]);
            Assert.AreEqual(expectedExtension, actual[2]);
            Assert.AreEqual(expectedArtist, actual[3]);
            Assert.AreEqual(expectedTitle, actual[4]);
        }

        [TestMethod]
        public void ParseFileNameWithoutDash()
        {
            string fileName = @"test\artisttitle.mp3";
            var expectedFullFilePath = @"test\artisttitle.mp3";
            var expectedPath = @"test\";
            var expectedExtension = ".mp3";
            var expectedArtist = "artisttitle";
            var expectedTitle = "artisttitle";
            var actual = parser.ParseFile(fileName);

            Assert.AreEqual(expectedFullFilePath, actual[0]);
            Assert.AreEqual(expectedPath, actual[1]);
            Assert.AreEqual(expectedExtension, actual[2]);
            Assert.AreEqual(expectedArtist, actual[3]);
            Assert.AreEqual(expectedTitle, actual[4]);
        }

        [TestMethod]
        public void ParseFileWithoutPath()
        {
            string fileName = @"artist-title.mp3";
            var expectedFullFilePath = @"artist-title.mp3";
            var expectedPath = "";
            var expectedExtension = ".mp3";
            var expectedArtist = "artist";
            var expectedTitle = "title";
            var actual = parser.ParseFile(fileName);

            Assert.AreEqual(expectedFullFilePath, actual[0]);
            Assert.AreEqual(expectedPath, actual[1]);
            Assert.AreEqual(expectedExtension, actual[2]);
            Assert.AreEqual(expectedArtist, actual[3]);
            Assert.AreEqual(expectedTitle, actual[4]);
        }
        
        [TestMethod]
        public void CollectFilePath()
        {
            FileShell fileShell = new FileShell("asasasas", @"\test\dir\", ".mp3", "Cat", "Dog");

            parser.CollectFilePath(fileShell);

            var expected = @"\test\dir\Cat-Dog.mp3";
            Assert.AreEqual(expected, fileShell.FullFilePath);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TryParseWrongFilePath()
        {
            string fileName = @".mp3";

            var actual = parser.ParseFile(fileName);
        }

    }
}
