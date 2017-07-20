using Microsoft.VisualStudio.TestTools.UnitTesting;
using RenamerLibrary;
using System.IO;

namespace RenamerTests
{
    [TestClass]
    public class RenamerTests
    {
        [TestMethod]
        public void MakeFileNames()
        {
            var renamer = new Renamer("");

            var firstSong = new Mp3File(@"Kashmir.mp3");
            firstSong.Artist = "Led Zeppelin";
            firstSong.Title = "Kashmir";

            var secondSong = new Mp3File(@"The Song Remains The Same.mp3");
            firstSong.Artist = "Led Zeppelin";
            firstSong.Title = "The Song Remains The Same";

            renamer.MakeFileNames(new string[] { @"Kashmir.mp3", @"The Song Remains The Same.mp3" });

            Assert.AreEqual(true, File.Exists(@"Led Zeppelin - Kashmir.mp3"));
            Assert.AreEqual(true, File.Exists(@"Led Zeppelin - The Song Remains The Same.mp3"));
        }

        [TestMethod]
        public void MakeFileTags()
        {
            var renamer = new Renamer("");

            renamer.MakeFileTags(new string[] { @"Led Zeppelin - Kashmir.mp3", @"Led Zeppelin - The Song Remains The Same.mp3" });

            var firstSong = new Mp3File(@"Led Zeppelin - Kashmir.mp3");
            var secondSong = new Mp3File(@"Led Zeppelin - The Song Remains The Same.mp3");

            Assert.AreEqual("Led Zeppelin", firstSong.Artist);
            Assert.AreEqual("Kashmir", firstSong.Title);
            Assert.AreEqual("Led Zeppelin", secondSong.Artist);
            Assert.AreEqual("The Song Remains The Same", secondSong.Title);
        }

        [TestMethod]
        public void Rename()
        {
            var renamer = new Renamer("");
            renamer.Rename(new string[] { "*.mp3", "-toTag" });

            var firstSong = new Mp3File(@"Led Zeppelin - Kashmir.mp3");
            var secondSong = new Mp3File(@"Led Zeppelin - The Song Remains The Same.mp3");

            Assert.AreEqual("Led Zeppelin", firstSong.Artist);
            Assert.AreEqual("Kashmir", firstSong.Title);
            Assert.AreEqual("Led Zeppelin", secondSong.Artist);
            Assert.AreEqual("The Song Remains The Same", secondSong.Title);
        }
    }
}
