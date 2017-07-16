using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rename;
using TagLib;

namespace RenamerTests
{
    [TestClass]
    public class RenamerTests
    {
        private string filePathForName = @"tests\Kashmir.mp3";
        private string filePathForNameCopy = @"tests\KashimirCopy.mp3";
        private string filePathForTag = @"tests\Led Zeppelin - Stairway to Heaven.mp3";
        private string directory = "tests";
        private File file;

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RenameWithEmptyArguments()
        {
            var renamer = new FileRenamer(directory);
            renamer.Rename(new string[] { });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RenameWithTooManyArguments()
        {
            var renamer = new FileRenamer(directory);
            renamer.Rename(new string[] {"*.mp3", "-recursive", "-toFileName", "-toTag" });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RenameWithWrongArguments()
        {
            var renamer = new FileRenamer(directory);
            renamer.Rename(new string[] { "*.mp3", "-recursive", "-wrongArgument"});
        }

        [TestMethod]
        public void MakeFilenameFromTags()
        {
            if (System.IO.File.Exists(@"tests\Led Zeppelin - Kashmir.mp3"))
                System.IO.File.Delete(@"tests\Led Zeppelin - Kashmir.mp3");
            System.IO.File.Copy(filePathForName, filePathForNameCopy);
            
            var renamer = new FileRenamer(directory);
            renamer.MakeFileNames(new string[] { filePathForNameCopy });

            Assert.AreEqual(true, System.IO.File.Exists(@"tests\Led Zeppelin - Kashmir.mp3"));
        }

        [TestMethod]
        public void MakeTagsFromFilename()
        {
            file = File.Create(filePathForTag);
            file.Tag.Performers = new string[] { };
            file.Tag.Title = String.Empty;
            file.Save();

            var renamer = new FileRenamer(directory);
            renamer.MakeFileTags(new string[] { filePathForTag });

            file = File.Create(filePathForTag);
            Assert.AreEqual("Led Zeppelin", file.Tag.FirstPerformer);
            Assert.AreEqual("Stairway to Heaven", file.Tag.Title);
        }

        [TestMethod]
        public void GetFilePathsWithoutSubfolders()
        {
            var paths = new string[]
            {
                @"tests\Kashmir.mp3",
                @"tests\Led Zeppelin - Kashmir.mp3",
                @"tests\Led Zeppelin - Stairway to Heaven.mp3"
            };

            var renamer = new FileRenamer(directory);
            var recieverPaths = renamer.GetFilePaths(directory, "*.mp3", false);

            var expectedPaths = String.Empty;
            foreach (var path in paths)
                expectedPaths += path;

            var actualPaths = String.Empty;
            foreach (var path in recieverPaths)
                actualPaths += path;

            Assert.AreEqual(expectedPaths, actualPaths);
        }

        [TestMethod]
        public void GetFilePathsWithSubfolders()
        {
            var paths = new string[]
            {
                @"tests\Kashmir.mp3",
                @"tests\Led Zeppelin - Kashmir.mp3",
                @"tests\Led Zeppelin - Stairway to Heaven.mp3",
                @"tests\dir\Led Zeppelin - The Song Remains The Same.mp3"
            };

            var renamer = new FileRenamer(directory);
            var recieverPaths = renamer.GetFilePaths(directory, "*.mp3", true);

            var expectedPaths = String.Empty;
            foreach (var path in paths)
                expectedPaths += path;

            var actualPaths = String.Empty;
            foreach (var path in recieverPaths)
                actualPaths += path;

            Assert.AreEqual(expectedPaths, actualPaths);
        }
    }
}
