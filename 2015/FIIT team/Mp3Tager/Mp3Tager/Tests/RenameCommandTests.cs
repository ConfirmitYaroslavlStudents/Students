using CommandCreation;
using Mp3Lib;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class RenameCommandTests
    {
        private Command _command;
        private IMp3File _file;

        [SetUp]
        public void SetUp()
        {
            _file = new FakeMp3File(@"D:\TestFile.mp3", new Mp3Tags());
            _file.Mp3Tags.Album = "TestAlbum";
            _file.Mp3Tags.Artist = "TestPerformer";
            _file.Mp3Tags.Genre = "TestGenre";
            _file.Mp3Tags.Title = "TestTitle";
            _file.Mp3Tags.Track = 1;         

            _command = new RenameCommand(_file, TagNames.Track + ". " + TagNames.Artist + " - " + TagNames.Title);
        }

        [Test]
        public void RenameTestExecute_Successful()
        {
            _command.Execute();

            var expected = @"D:\1. TestPerformer - TestTitle.mp3";
            var actual = _file.Path;
            Assert.AreEqual(expected, actual);
        }


    }
}
