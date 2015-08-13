using CommandCreation;
using FileLib;
using NUnit.Framework;
using Tests.Fakes;

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
            _file = new FakeMp3File(new Mp3Tags(), @"D:\TestFile.mp3");
            _file.Tags.Album = "TestAlbum";
            _file.Tags.Artist = "TestPerformer";
            _file.Tags.Genre = "TestGenre";
            _file.Tags.Title = "TestTitle";
            _file.Tags.Track = 1;         

            _command = new RenameCommand(_file, new FakeFileExistenceChecker(), 
                TagNames.Track + ". " + TagNames.Artist + " - " + TagNames.Title);
        }

        [Test]
        public void RenameTestExecute_Successful()
        {
            _command.Execute();

            var expected = @"D:\1. TestPerformer - TestTitle.mp3";
            var actual = _file.FullName;
            Assert.AreEqual(expected, actual);
        }


    }
}
