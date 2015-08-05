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
        private TestFileLoader _fileLoader;
        private Tager _testTager;
        private Mp3Tags _tesTags;
        [TestInitialize]
        public void Init()
        {
            _fileLoader = new TestFileLoader();
            _testTager = new Tager(_fileLoader);
            _tesTags = new Mp3Tags() { Album = "Album", Artist = "Artist", Comment = "Comment", Genre = "Genre", Title = "Title", Year = 2015, Track = 1 };
        }
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TagerWithoutFileCantSave()
        {
            Assert.IsNull(_testTager.CurrentFile);
            _testTager.Save();
            _testTager.ChangeTags(new Mp3Tags());

        }
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TagerWithoutFileCantChangeTags()
        {
            _testTager.ChangeTags(new Mp3Tags());
        }
        [TestMethod]
        public void LoadTest()
        {
            _testTager.Load("TEST");
            Assert.IsNotNull(_testTager.CurrentFile);
        }
        [TestMethod]
        public void SaveTest()
        {
            _testTager.Load("TEST");
            var file = _testTager.CurrentFile as TestMp3File;
            Assert.AreEqual(false,file.SaveFlag);
            _testTager.Save();
            Assert.AreEqual(true,file.SaveFlag);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ChangeTagsTest()
        {
            _testTager.Load("TEST");
            var file = _testTager.CurrentFile as TestMp3File;
            _testTager.ChangeTags(_tesTags);
            Assert.AreEqual(_tesTags, file.Tags);
            _testTager.ChangeTags(null);
        }
        [TestMethod]
        public void ChangeNameTest()
        {
            var expectedName = "[1]. Artist - Title 2015 live in Russia";
            var mask = new Mask("[{track}]. {artist} - {title} {year} live in Russia");
            _testTager.Load("oldfilename");
            _testTager.ChangeTags(_tesTags);
            _testTager.ChangeName(mask);
            var currentFile = _testTager.CurrentFile as TestMp3File;
            Assert.AreEqual(true, currentFile.ChangeNameFlag);
            Assert.AreEqual(expectedName,currentFile.Name);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NameNotChangeIfTagIsEmpty()
        {
            var expectedName = "[1]. Artist - Title 2015 live in Russia";
            var tags = new Mp3Tags() { Album = "Album", Artist = "", Comment = "Comment", Genre = "Genre", Title = "Title", Year = 2015, Track = 1 };
            var mask = new Mask("[{track}]. {artist} - {title} {year} live in Russia");
            _testTager.Load("oldfilename");
            _testTager.ChangeTags(tags);
            _testTager.ChangeName(mask);
        }
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ExceptionIfFileNotLoaded()
        {
            var mask = new Mask("[{track}]. {artist} - {title} {year} live in Russia");
            _testTager.ChangeName(mask);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ExceptionIfMaskIsNull()
        {
            _testTager.Load("path");
            _testTager.ChangeName(null);
        }

        [TestMethod]
        public void ValidateNameTest()
        {
            var tags = new Mp3Tags() { Album = "Album", Artist = "Artist", Comment = "Artist2 cover", Genre = "Genre", Title = "Title", Year = 2015, Track = 1 };
            var mask = new Mask("[{track}]. {artist} - {title} {comment} {year} live in Russia");
            _testTager.Load("[1]. Artist - Title Artist2 cover 2015 live in Russia");
            _testTager.ChangeTags(tags);
            Assert.AreEqual(true,_testTager.ValidateFileName(mask));
        }
        [TestMethod]
        public void ValidateNameReturnFalseIfNameDoesntContainTag()
        {     
            var mask = new Mask("[{track}]. {artist} - {title} {year} live in Russia");
            _testTager.Load("[1]. Superartist - Title 2015 live in Russia");
            _testTager.ChangeTags(_tesTags);
            Assert.AreEqual(false, _testTager.ValidateFileName(mask));
        }
        [TestMethod]
        public void ValidateNameReturnFalseIfMaskIsBad()
        {
            var mask = new Mask("[{track}]. {artist} - {title} {year} live in Russia AMAZING VASYA REMIX");
            _testTager.Load("[1]. Superartist - Title 2015 live in Russia");
            _testTager.ChangeTags(_tesTags);
            Assert.AreEqual(false, _testTager.ValidateFileName(mask));
        }
        [TestMethod]
        public void WorksWithLeadingZeroInTrack()
        {
            var mask = new Mask("[{track}]. {artist} - {title} {year} live in Russia");
            _testTager.Load("[01]. Artist - Title 2015 live in Russia");
            _testTager.ChangeTags(_tesTags);
            Assert.AreEqual(true, _testTager.ValidateFileName(mask));
        }
    }
}
