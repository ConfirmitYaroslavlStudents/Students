using MusicFileRenameLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FileRenamerTest
{
    [TestClass]
    public class ShellMakerTest
    {
        private ShellMaker _shellMaker = new ShellMaker();

        [TestMethod]
        public void MakeFileShell()
        {
            var parsedFile = new string[]
            {
                "test.mp3","test",".mp3","artist","title","aTag","tTag"
            };

            var actual = _shellMaker.MakeFileShell(parsedFile);

            Assert.AreEqual("test.mp3", actual.FullFilePath);
            Assert.AreEqual("test", actual.Path);
            Assert.AreEqual(".mp3", actual.Extension);
            Assert.AreEqual("artist", actual.Artist);
            Assert.AreEqual("title", actual.Title);
            Assert.AreEqual("aTag", actual.TagArtist);
            Assert.AreEqual("tTag", actual.TagTitle);
        }

        [TestMethod]
        public void MakeArgsShell()
        {
            var args = new string[]
            {
                "*.mp3", "-toFileName"
            };

            var actual = _shellMaker.MakeArgsShell(args);
            
            Assert.AreEqual("*.mp3",actual.Pattern);
            Assert.AreEqual(false, actual.Recursive);
            Assert.AreEqual(new FileNameRenamer().GetType(), actual.Renamer.GetType());
        }

        [TestMethod]
        public void MakeArgsShellWithRecursive()
        {
            var args = new string[]
            {
                "*.mp3", "-recursive", "-toFileName"
            };

            var actual = _shellMaker.MakeArgsShell(args);

            Assert.AreEqual("*.mp3", actual.Pattern);
            Assert.AreEqual(true, actual.Recursive);
            Assert.AreEqual(new FileNameRenamer().GetType(), actual.Renamer.GetType());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MakeArgsWrongSecondArg()
        {
            var args = new string[]
            {
                "*.mp3", "-", "-toFileName"
            };

            var actual = _shellMaker.MakeArgsShell(args);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MakeArgsWrongThirdArg()
        {
            var args = new string[]
            {
                "*.mp3", "-recursive", "-toFileTag"
            };

            var actual = _shellMaker.MakeArgsShell(args);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TryToMakeFileShellFromWrongParsedFile()
        {
            var parsedFile = new string[]
            {
                "test",".mp3","artist","title"
            };

            var actual = _shellMaker.MakeFileShell(parsedFile);

            Assert.AreEqual("test.mp3", actual.FullFilePath);
            Assert.AreEqual("test", actual.Path);
            Assert.AreEqual(".mp3", actual.Extension);
            Assert.AreEqual("artist", actual.Artist);
            Assert.AreEqual("title", actual.Title);
            Assert.AreEqual("aTag", actual.TagArtist);
            Assert.AreEqual("tTag", actual.TagTitle);
        }
    }
}
