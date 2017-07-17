using System;
using TagLib;
using MusicFileRename;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileRenamerTest
{
    [TestClass]
    public class MusicFileRenamerTest
    {
        private string pathForTag = @"test\Ed Sheeran-Shape of You.mp3";
        private string pathForRecursiveTest = @"test\dir\MiyaGi & Эндшпиль-Бада-бум.mp3";
        private string pathForName = @"test\1.mp3";
        private string pathForNameCopy = @"test\12.mp3";

        [TestMethod]
        public void ToFileName()
        {
            var expectedFileName = @"test\Грибы-Тает Лёд.mp3";
            if (System.IO.File.Exists(expectedFileName))
                System.IO.File.Delete(expectedFileName);

            System.IO.File.Copy(pathForName, pathForNameCopy);

            MusicFileRenamer.RenameFileNameByTag("12.mp3", false, @"test\");
            Assert.AreEqual(true, System.IO.File.Exists(expectedFileName));
        }

        [TestMethod]
        public void ToTag()
        {
            var fileTag = File.Create(pathForTag);
            fileTag.Tag.Performers = new string[] { };
            fileTag.Tag.Title = "";
            fileTag.Save();

            var expectedPerformerTag = "Ed Sheeran";
            var expectedTitleTag = "Shape of You";

            MusicFileRenamer.RenameTagByFileName("*Shape*.mp3",false,@"test\");
            fileTag = File.Create(pathForTag);
            Assert.AreEqual(fileTag.Tag.Performers[0], expectedPerformerTag);
            Assert.AreEqual(fileTag.Tag.Title, expectedTitleTag);
        }

        [TestMethod]
        public void RecursiveToFileName()
        {
            var expectedString = pathForTag+pathForRecursiveTest;
            if (System.IO.File.Exists(pathForRecursiveTest))
                System.IO.File.Delete(pathForRecursiveTest);

            System.IO.File.Copy(@"test\dir\1.mp3", @"test\dir\aaa.mp3");

            MusicFileRenamer.RenameFileNameByTag("*a*.mp3", true);
            var files = System.IO.Directory.GetFiles(@"test\", "*a*.mp3", System.IO.SearchOption.AllDirectories);

            string actualString = "";
            foreach (var path in files)
                actualString += path;
            Assert.AreEqual(expectedString, actualString);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void WrongDirectory()
        {
            MusicFileRenamer.RenameFileNameByTag("*a*.mp3", false, @"wrongDirectory\");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void WrongPattern()
        {
            MusicFileRenamer.RenameFileNameByTag(".mp3");
        }
    }
}
