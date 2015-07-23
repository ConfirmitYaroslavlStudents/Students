using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mp3TagLib;

namespace Mp3TagTest
{
    [TestClass]
    public class TagerTestClass
    {
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void EmptyTagerTest()
        {
            var fileLoader=new TestFileLoader();
            var testTager=new Tager(fileLoader);
            Assert.IsNull(testTager.CurrentFile);
            Assert.AreEqual(false, testTager.Save());
            testTager.ChangeTags(new Mp3Tags());

        }
        [TestMethod]
        public void LoadTest()
        {
            var fileLoader = new TestFileLoader();
            var testTager = new Tager(fileLoader);
            testTager.Load("TEST");
            Assert.IsNotNull(testTager.CurrentFile);
        }
        [TestMethod]
        public void SaveTest()
        {
            var fileLoader = new TestFileLoader();
            var testTager = new Tager(fileLoader);
            testTager.Load("TEST");
            var file = testTager.CurrentFile as TestMp3File;
            Assert.AreEqual(false,file.SaveFlag);
            Assert.AreEqual(true,testTager.Save());
            Assert.AreEqual(true,file.SaveFlag);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ChangeTest()
        {
            var fileLoader = new TestFileLoader();
            var testTager = new Tager(fileLoader);
            testTager.Load("TEST");
            var file = testTager.CurrentFile as TestMp3File;
            var newTags=new Mp3Tags(){Album = "Album",Artist = "Artist",Comment = "Comment",Genre = "Genre",Title = "Title",Year = 2015};
            testTager.ChangeTags(newTags);
            Assert.AreEqual(newTags, file.Tags);
            testTager.ChangeTags(null);
        }
    }
}
