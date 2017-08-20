using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RenamerLib;

namespace MP3Renamer.Tests
{
    [TestClass]
    public class ActionsTest
    {
        private Random rand = new Random();
        public const string Alphabet =
            "abcdefghijklmnopqrstuvwyxzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public string GenerateString(int size)
        {
            char[] chars = new char[size];
            for (int i = 0; i < size; i++)
            {
                chars[i] = Alphabet[rand.Next(Alphabet.Length)];
            }
            return new string(chars);
        }

        [TestMethod]
        public void TagToFileNameActionTest()
        {
            MockFileManager fileManager = new MockFileManager();

            IAction tagToFileNameAction = new TagToFileNameAction();

            IMP3File mp3File = new MockMP3File("test.mp3", fileManager);
            string title = GenerateString(15);
            string artist = GenerateString(10);
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

            string title = GenerateString(15);
            string artist = GenerateString(10);
            IMP3File mp3File = new MockMP3File(artist + " - " + title + ".mp3", fileManager);
            fileManager.AddFile(artist + " - " + title + ".mp3", mp3File);

            fileNameToTagAction.Process(mp3File);

            Assert.AreEqual(title, mp3File.Title);
            Assert.AreEqual(artist, mp3File.Artist);
        }
    }
}
