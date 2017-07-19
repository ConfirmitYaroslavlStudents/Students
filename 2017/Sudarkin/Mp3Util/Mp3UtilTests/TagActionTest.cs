using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions.TestingHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mp3UtilLib;
using Mp3UtilLib.Actions;
using Mp3UtilTests.Helpers;

namespace Mp3UtilTests
{
    [TestClass]
    public class TagActionTest
    {
        private readonly byte[] _dummyMp3;

        public TagActionTest()
        {
            _dummyMp3 = TestHelper.GetDummyMp3();
        }

        [TestMethod]
        public void ExtractTagsFromName()
        {
            MockFileSystem fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"C:\Music\Bullet For My Valentine - Cries in Vain.mp3", new MockFileData(_dummyMp3) },
                { @"C:\Music\Bullet For My Valentine - Curses.mp3", new MockFileData(_dummyMp3) },
                { @"C:\Music\Bullet For My Valentine - No Control.mp3", new MockFileData(_dummyMp3) },
                { @"C:\Music\Bullet For My Valentine - Just Another Star.mp3", new MockFileData(_dummyMp3) }
            });

            IActionStrategy tagAction = new TagAction();

            foreach (string file in fileSystem.AllFiles)
            {
                Mp3File mp3File = TestHelper.GetMp3File(file, fileSystem);
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