using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MP3Renamer;
using TagLib;

namespace MP3Renamer.Tests
{
    /*[TestClass]
    public class MP3RenamerTests
    {
        private string directory = "tests";
        private string testTagToFile = @"tests\01_imagine_dragons_radioactive_myzuka.org.mp3";
        private string testTagToFileExpected = @"tests\Imagine Dragons - Radioactive.mp3";
        private string testTagToFileRecursive = @"tests\dir\01_john_newman_love_me_again_myzuka.org.mp3";
        private string testTagToFileRecursiveExpected = @"tests\dir\John Newman - Love Me Again.mp3";
        private string testFileToTag = @"tests\Adele - Rolling in the deep.mp3";
        private string testFileToTagNameExpected = "Rolling in the deep";
        private string testFileToTagArtistExpected = "Adele";
        private string testFileToTagRecursive = @"tests\dir\Lukas Graham - 7 years.mp3";
        private string testFileToTagRecursiveNameExpected = "7 years";
        private string testFileToTagRecursiveArtistExpected = "Lukas Graham";

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RenameWithNoArguments()
        {
            string mask;
            bool isRecursive, isTagToFileName;
            MP3Renamer.Program.ParseArguments(new string[] { }, out mask, out isRecursive, out isTagToFileName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RenameWithLessArguments()
        {
            string mask;
            bool isRecursive, isTagToFileName;
            MP3Renamer.Program.ParseArguments(new string[] { "mask.mp3" }, out mask, out isRecursive, out isTagToFileName);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RenameWithMoreArguments()
        {
            string mask;
            bool isRecursive, isTagToFileName;
            MP3Renamer.Program.ParseArguments(new string[] { "mask.mp3", "-recursive", "-toTag", "-toFileName" }, out mask, out isRecursive, out isTagToFileName);
        }

        [TestMethod]
        public void RenameFileFromTag()
        {
            if (System.IO.File.Exists(testTagToFileExpected))
                System.IO.File.Move(testTagToFileExpected, testTagToFile);

            Renamer renamer = new Renamer(directory);
            renamer.Rename("*dragons*.mp3", false, true);

            

            Assert.IsTrue(System.IO.File.Exists(testTagToFileExpected));
        }

        [TestMethod]
        public void RenameFileFromTagRecursive()
        {
            if (System.IO.File.Exists(testTagToFileRecursiveExpected))
                System.IO.File.Move(testTagToFileRecursiveExpected, testTagToFileRecursive);

            Renamer renamer = new Renamer(directory);
            renamer.Rename("*again*.mp3", true, false);

            Assert.IsTrue(System.IO.File.Exists(testTagToFileRecursiveExpected));
        }

        [TestMethod]
        public void ChangeTagFromFile()
        {
            var file = TagLib.File.Create(testFileToTag);
            file.Tag.Title = "";
            file.Tag.Artists = new string[] {};
            file.Save();

            Renamer renamer = new Renamer(directory);
            renamer.Rename("Adele*.mp3", false, false);

            var resultFile = TagLib.File.Create(testFileToTag);
            var resultArtist = resultFile.Tag.FirstPerformer;
            var resultTitle = resultFile.Tag.Title;

            Assert.AreEqual(testFileToTagArtistExpected, resultArtist);
            Assert.AreEqual(testFileToTagNameExpected, resultTitle);
        }

        [TestMethod]
        public void ChangeTagFromFileRecursive()
        {
            var file = TagLib.File.Create(testFileToTagRecursive);
            file.Tag.Title = "";
            file.Tag.Artists = new string[] { };
            file.Save();

            Renamer renamer = new Renamer(directory);
            renamer.Rename("Lukas*.mp3", true, false);

            var resultFile = TagLib.File.Create(testFileToTagRecursive);
            var resultArtist = resultFile.Tag.FirstPerformer;
            var resultTitle = resultFile.Tag.Title;

            Assert.AreEqual(testFileToTagRecursiveArtistExpected, resultArtist);
            Assert.AreEqual(testFileToTagRecursiveNameExpected, resultTitle);
        }
    }*/
}
