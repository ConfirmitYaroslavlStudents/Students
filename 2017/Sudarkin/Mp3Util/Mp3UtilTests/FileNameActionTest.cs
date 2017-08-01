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
                @"Bullet For My Valentine - Cries in Vain.mp3",
                @"Anne-Marie - Ciao Adios.mp3"
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

                Assert.AreEqual(true, fileSystem.Exists($@"{artist} - {title}.mp3"));
            }
        }

        private string GetRandomString()
        {
            return Guid.NewGuid().ToString();
        }
    }
}