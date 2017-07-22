using System.Collections.Generic;
using System.Linq;
using System.IO.Abstractions.TestingHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mp3UtilLib;

namespace Mp3UtilTests
{
    [TestClass]
    public class FileManagerTest
    {
        private readonly FileManager _fileManager;

        private readonly string[] _musicFolder =
        {
            @"C:\Music\Bullet For My Valentine - Cries in Vain.mp3",
            @"C:\Music\Bullet For My Valentine - Curses.mp3",
            @"C:\Music\Bullet For My Valentine - No Control.mp3"
        };
        private readonly string[] _albumFolder =
        {
            @"C:\Music\Album\Bullet For My Valentine - No Control.mp3",
            @"C:\Music\Album\Bullet For My Valentine - Just Another Star.mp3"
        };

        public FileManagerTest()
        {
            Dictionary<string, MockFileData> files = new Dictionary<string, MockFileData>();
            AddEmptyFilesInFileDictionary(files, _musicFolder);
            AddEmptyFilesInFileDictionary(files, _albumFolder);

            MockFileSystem fileSystem = new MockFileSystem(files);
            fileSystem.Directory.SetCurrentDirectory(@"C:\Music");

            _fileManager = new FileManager(fileSystem);
        }

        [TestMethod]
        public void RecursiveSearch()
        {
            RecursiveTest(true, _musicFolder.Concat(_albumFolder));
        }

        [TestMethod]
        public void NonRecursiveSearch()
        {
            RecursiveTest(false, _musicFolder);
        }

        private void RecursiveTest(bool recursive, IEnumerable<string> expectedCollection)
        {
            string[] files = (string[]) _fileManager.GetFilesFromCurrentDirectory("*", recursive);
            string[] collection = expectedCollection as string[] ?? expectedCollection.ToArray();

            Assert.AreEqual(collection.Length, files.Length);
            Assert.AreEqual(0, files.Except(collection).Count());
        }

        private void AddEmptyFilesInFileDictionary(
            Dictionary<string, MockFileData> dictionary, IEnumerable<string> files)
        {
            foreach (string item in files)
            {
                dictionary.Add(item, new MockFileData(""));
            }
        }
    }
}