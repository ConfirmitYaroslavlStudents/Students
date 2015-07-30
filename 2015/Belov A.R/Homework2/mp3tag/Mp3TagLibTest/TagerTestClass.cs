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
        public void TagerWithoutFileCantSave()
        {
            var fileLoader=new TestFileLoader();
            var testTager=new Tager(fileLoader);
            Assert.IsNull(testTager.CurrentFile);
            testTager.Save();
            testTager.ChangeTags(new Mp3Tags());

        }
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TagerWithoutFileCantChangeTags()
        {
            var fileLoader = new TestFileLoader();
            var testTager = new Tager(fileLoader);
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
            testTager.Save();
            Assert.AreEqual(true,file.SaveFlag);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ChangeTagsTest()
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
        [TestMethod]
        public void ChangeNameTest()
        {
            var fileLoader = new TestFileLoader();
            var testTager = new Tager(fileLoader);
            var expectedName = "[1]. Artist - Title 2015 live in Russia";
            var tags = new Mp3Tags() { Album = "Album", Artist = "Artist", Comment = "Comment", Genre = "Genre", Title = "Title", Year = 2015,Track = 1};
            var mask = new Mask("[{track}]. {artist} - {title} {year} live in Russia");
            testTager.Load("oldfilename");
            testTager.ChangeTags(tags);
            testTager.ChangeName(mask);
            var currentFile = testTager.CurrentFile as TestMp3File;
            Assert.AreEqual(true, currentFile.ChangeNameFlag);
            Assert.AreEqual(expectedName,currentFile.Name);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NameNotChangeIfTagIsEmpty()
        {
            var fileLoader = new TestFileLoader();
            var testTager = new Tager(fileLoader);
            var expectedName = "[1]. Artist - Title 2015 live in Russia";
            var tags = new Mp3Tags() { Album = "Album", Artist = "", Comment = "Comment", Genre = "Genre", Title = "Title", Year = 2015, Track = 1 };
            var mask = new Mask("[{track}]. {artist} - {title} {year} live in Russia");
            testTager.Load("oldfilename");
            testTager.ChangeTags(tags);
            testTager.ChangeName(mask);
        }
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ExceptionIfFileNotLoaded()
        {
            var fileLoader = new TestFileLoader();
            var testTager = new Tager(fileLoader);
            var mask = new Mask("[{track}]. {artist} - {title} {year} live in Russia");
            testTager.ChangeName(mask);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ExceptionIfMaskIsNull()
        {
            var fileLoader = new TestFileLoader();
            var testTager = new Tager(fileLoader);
            testTager.Load("path");
            testTager.ChangeName(null);
        }

        [TestMethod]
        public void ValidateNameTest()
        {
            var fileLoader = new TestFileLoader();
            var testTager = new Tager(fileLoader);
            var tags = new Mp3Tags() { Album = "Album", Artist = "Artist", Comment = "Artist2 cover", Genre = "Genre", Title = "Title", Year = 2015, Track = 1 };
            var mask = new Mask("[{track}]. {artist} - {title} {comment} {year} live in Russia");
            testTager.Load("[1]. Artist - Title Artist2 cover 2015 live in Russia");
            testTager.ChangeTags(tags);
            Assert.AreEqual(true,testTager.ValidateFileName(mask));
        }
        [TestMethod]
        public void ValidateNameReturnFalseIfNameDoesntContainTag()
        {
            var fileLoader = new TestFileLoader();
            var testTager = new Tager(fileLoader);
            var tags = new Mp3Tags() { Album = "Album", Artist = "Artist", Comment = "Comment", Genre = "Genre", Title = "Title", Year = 2015, Track = 1 };
            var mask = new Mask("[{track}]. {artist} - {title} {year} live in Russia");
            testTager.Load("[1]. Superartist - Title 2015 live in Russia");
            testTager.ChangeTags(tags);
            Assert.AreEqual(false, testTager.ValidateFileName(mask));
        }
        [TestMethod]
        public void ValidateNameReturnFalseIfMaskIsBad()
        {
            var fileLoader = new TestFileLoader();
            var testTager = new Tager(fileLoader);
            var tags = new Mp3Tags() { Album = "Album", Artist = "Artist", Comment = "Comment", Genre = "Genre", Title = "Title", Year = 2015, Track = 1 };
            var mask = new Mask("[{track}]. {artist} - {title} {year} live in Russia AMAZING VASYA REMIX");
            testTager.Load("[1]. Superartist - Title 2015 live in Russia");
            testTager.ChangeTags(tags);
            Assert.AreEqual(false, testTager.ValidateFileName(mask));
        }
        [TestMethod]
        public void WorksWithLeadingZeroInTrack()
        {
            var fileLoader = new TestFileLoader();
            var testTager = new Tager(fileLoader);
            var tags = new Mp3Tags() { Album = "Album", Artist = "Artist", Comment = "Comment", Genre = "Genre", Title = "Title", Year = 2015, Track = 1 };
            var mask = new Mask("[{track}]. {artist} - {title} {year} live in Russia");
            testTager.Load("[01]. Artist - Title 2015 live in Russia");
            testTager.ChangeTags(tags);
            Assert.AreEqual(true, testTager.ValidateFileName(mask));
        }
    }
}
