
using FileLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using CommandCreation;

namespace Tests
{
    [TestClass]
    public class ChangeTagsCommandTest
    {
        private ChangeTagsCommand _command;
        private FakeMp3File _file;

        [TestInitialize]
        public void SetMp3File()
        {
            _file = new FakeMp3File(new Mp3Tags(), @"D:\TestFile.mp3");
            _file.Tags.Album = "TestAlbum";
            _file.Tags.Artist = "TestPerformer";
            _file.Tags.Genre = "TestGenre";
            _file.Tags.Title = "TestTitle";
            _file.Tags.Track = 1;
        }

        [TestMethod]
        public void Change2Tags_Successful()
        {
            _command = new ChangeTagsCommand(_file, "{artist} - {title}");
            _file.FullName = @"D:\Art - ist.mp3";

            _command.Execute();

            Assert.AreEqual("Art", _file.Tags.Artist);
            Assert.AreEqual("ist", _file.Tags.Title);
        }

        [TestMethod]
        public void Change3Tags_Successful()
        {
            _command = new ChangeTagsCommand(_file, "{track}.{artist} - {title}");
            _file.FullName = @"D:\1.Art - ist.mp3";
            uint expectedTrack = 1;

            _command.Execute();

            Assert.AreEqual("Art", _file.Tags.Artist);
            Assert.AreEqual("ist", _file.Tags.Title);
            Assert.AreEqual(expectedTrack, _file.Tags.Track);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void ChangeTags_WithStrangeSeparator_Successful()
        {
            _command = new ChangeTagsCommand(_file, "{track}.{artist} - {title}");
            _file.FullName = @"D:\1.Artabcist.mp3";

            _command.Execute();
        }

        [TestMethod]
        public void ChangeTags_SeparatorInName_Successful()
        {
            _command = new ChangeTagsCommand(_file, "{artist} - {title}");
            _file.FullName = @"D:\Hi-fi - song one.mp3";

            _command.Execute();

            Assert.AreEqual("Hi-fi", _file.Tags.Artist);
            Assert.AreEqual("song one", _file.Tags.Title);
        }
    }
}
