using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mp3UtilLib;
using Mp3UtilLib.Actions;
using Mp3UtilTests.Helpers;

namespace Mp3UtilTests
{
    [TestClass]
    public class FileNameActionTest
    {
        [TestMethod]
        public void RenameFiles()
        {
            MockFileSystem fileSystem = new MockFileSystem(new[]
            {
                @"C:\Music\Bullet For My Valentine - Cries in Vain.mp3",
                @"C:\Music\Bullet For My Valentine - Curses.mp3",
                @"C:\Music\Bullet For My Valentine - No Control.mp3",
                @"C:\Music\Bullet For My Valentine - Just Another Star.mp3"
            });

            IActionStrategy fileNameAction = new FileNameAction();

            foreach (string file in fileSystem.AllFiles)
            {
                AudioFile mp3File = new TestableMp3File(file, fileSystem);
                string artist = GetRandomString();
                string title = GetRandomString();
                mp3File.Artist = artist;
                mp3File.Title = title;

                fileNameAction.Process(mp3File);

                Assert.AreEqual(true, fileSystem.Exists($@"C:\Music\{artist} - {title}.mp3"));
            }
        }

        private string GetRandomString()
        {
            return Guid.NewGuid().ToString();
        }
    }
}