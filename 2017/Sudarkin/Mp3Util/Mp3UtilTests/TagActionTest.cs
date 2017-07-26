using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mp3UtilLib;
using Mp3UtilLib.Actions;
using Mp3UtilTests.Helpers;

namespace Mp3UtilTests
{
    [TestClass]
    public class TagActionTest
    {
        [TestMethod]
        public void ExtractTagsFromName()
        {
            string[] files =
            {
                @"C:\Music\Bullet For My Valentine - Cries in Vain.mp3",
                @"C:\Music\Bullet For My Valentine - Curses.mp3",
                @"C:\Music\Bullet For My Valentine - No Control.mp3",
                @"C:\Music\Bullet For My Valentine - Just Another Star.mp3"
            };

            IActionStrategy tagAction = new TagAction();

            foreach (string file in files)
            {
                AudioFile mp3File = new TestableMp3File(file);
                string[] chunks = Path.GetFileNameWithoutExtension(file)
                    .Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                string artist = chunks[0].Trim();
                string title = chunks[1].Trim();

                tagAction.Process(mp3File);
                
                Assert.AreEqual(artist, mp3File.Artist);
                Assert.AreEqual(title, mp3File.Title);
            }
        }
    }
}