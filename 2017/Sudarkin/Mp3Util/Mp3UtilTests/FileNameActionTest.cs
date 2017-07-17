using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mp3UtilConsole;
using Mp3UtilConsole.Actions;
using Mp3UtilTests.Helpers;

namespace Mp3UtilTests
{
    [TestClass]
    public class FileNameActionTest
    {
        private readonly byte[] _dummyMp3;

        public FileNameActionTest()
        {
            _dummyMp3 = TestHelper.GetDummyMp3();
        }

        [TestMethod]
        public void RenameFiles()
        {
            MockFileSystem fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                {@"C:\Music\Bullet For My Valentine - Cries in Vain.mp3", new MockFileData(_dummyMp3)},
                {@"C:\Music\Bullet For My Valentine - Curses.mp3", new MockFileData(_dummyMp3)},
                {@"C:\Music\Bullet For My Valentine - No Control.mp3", new MockFileData(_dummyMp3)},
                {@"C:\Music\Bullet For My Valentine - Just Another Star.mp3", new MockFileData(_dummyMp3)}
            });

            IActionStrategy fileNameAction = new FileNameAction(fileSystem);

            foreach (string file in fileSystem.AllFiles)
            {
                Mp3File mp3File = TestHelper.GetMp3File(file, fileSystem);
                string artist = GetRandomString();
                string title = GetRandomString();
                mp3File.Artist = artist;
                mp3File.Title = title;

                fileNameAction.Process(mp3File);

                Assert.AreEqual(true, fileSystem.FileExists($@"C:\Music\{artist} - {title}.mp3"));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(IOException))]
        public void RenameFileExistsError()
        {
            MockFileSystem fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                {@"C:\Music\Bullet For My Valentine - Cries in Vain.mp3", new MockFileData(_dummyMp3)},
                {@"C:\Music\Artist - Title.mp3", new MockFileData(_dummyMp3)},
            });

            Mp3File mp3File = TestHelper.GetMp3File(fileSystem.AllFiles.ToArray()[0], fileSystem);
            mp3File.Artist = "Artist";
            mp3File.Title = "Title";

            new FileNameAction(fileSystem).Process(mp3File);
        }

        private string GetRandomString()
        {
            return Guid.NewGuid().ToString();
        }
    }
}