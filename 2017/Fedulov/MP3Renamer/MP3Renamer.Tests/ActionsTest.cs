using Microsoft.VisualStudio.TestTools.UnitTesting;
using RenamerLib;
using RenamerLib.Actions;

namespace MP3Renamer.Tests
{
    [TestClass]
    public class ActionsTest
    {
        [TestMethod]
        public void TagToFileNameActionTest()
        {
            MockFileManager fileManager = new MockFileManager();

            IAction tagToFileNameAction = new TagToFileNameAction();

            IMP3File mp3File = new MockMp3File("test.mp3", fileManager);
            string title = "Songbyauthor";
            string artist = "Author";
            mp3File.Title = title;
            mp3File.Artist = artist;
            fileManager.AddFile("test.mp3", mp3File);

            tagToFileNameAction.Process(mp3File);

            Assert.IsTrue(fileManager.Exist("\\" + artist + " - " + title + ".mp3"));
        }

        [TestMethod]
        public void FileNameToTagActionTest()
        {
            MockFileManager fileManager = new MockFileManager();

            IAction fileNameToTagAction = new FileNameToTagAction();

            string title = "Songbyauthor";
            string artist = "Author";
            IMP3File mp3File = new MockMp3File(artist + " - " + title + ".mp3", fileManager);
            fileManager.AddFile(artist + " - " + title + ".mp3", mp3File);

            fileNameToTagAction.Process(mp3File);

            Assert.AreEqual(title, mp3File.Title);
            Assert.AreEqual(artist, mp3File.Artist);
        }
    }
}
