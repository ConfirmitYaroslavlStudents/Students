using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mp3TagLib;
using Mp3TagLib.Sync;

namespace Mp3TagTest
{
    [TestClass]
    public class SynchronizerTestClass
    {
        private Synchronizer _synchronizer;
        private TestMp3File _fileWithBadName;
        private TestMp3File _fileWithBadTags;
        private TestMp3File _fileWithBadTagsAndBadName;
        private Mask _testMask;
        private Tager _testTager;

        [TestInitialize]
        public void Init()
        {
            _testMask=new Mask("{artist} {title}");
            _testTager=new Tager(new TestFileLoader());
            _synchronizer=new Synchronizer(_testTager);
            _fileWithBadName=new TestMp3File(){Path = "Artist",Tags = new Mp3Tags(){Artist = "Artist",Title = "Title"}};
            _fileWithBadTags = new TestMp3File() { Path = "Artist Title", Tags = new Mp3Tags() { Artist = "Artist", Title = "" } };
            _fileWithBadTagsAndBadName = new TestMp3File() { Path = "Artist", Tags = new Mp3Tags() { Artist = "Artist", Title = "" } };
        }

        [TestMethod]
        public void FilesNotChangeBeforeSaving()
        {
            _synchronizer.Sync(new List<IMp3File>() { _fileWithBadName, _fileWithBadTags, _fileWithBadTagsAndBadName }, _testMask);
          
            Assert.IsFalse(_fileWithBadName.SaveFlag);
            Assert.IsFalse( _fileWithBadTags.SaveFlag);
            Assert.IsFalse( _fileWithBadTagsAndBadName.SaveFlag);
        }

        [TestMethod]
        public void ErrorFilesContainsFileWithBadTagAndBadName()
        {
            _synchronizer.Sync(new List<IMp3File>() { _fileWithBadName, _fileWithBadTags, _fileWithBadTagsAndBadName }, _testMask);
           
            Assert.IsTrue(_synchronizer.ErrorFiles.Keys.Contains(_fileWithBadTagsAndBadName.Name));
        }

        [TestMethod]
        public void ModifiedFilesContainsNotSyncFiles()
        {
            _synchronizer.Sync(new List<IMp3File>() { _fileWithBadName, _fileWithBadTags, _fileWithBadTagsAndBadName }, _testMask);
           
            Assert.IsTrue(_synchronizer.ModifiedFiles.Contains(_fileWithBadTags));
            Assert.IsTrue( _synchronizer.ModifiedFiles.Contains(_fileWithBadName));
        }

        [TestMethod]
        public void SaveTest()
        {
            _synchronizer.Sync(new List<IMp3File>() { _fileWithBadName, _fileWithBadTags, _fileWithBadTagsAndBadName }, _testMask);
            _synchronizer.Save();
           
            Assert.IsTrue( _fileWithBadName.SaveFlag);
            Assert.IsTrue( _fileWithBadTags.SaveFlag);
            Assert.IsFalse( _fileWithBadTagsAndBadName.SaveFlag);
        }
    }
}
