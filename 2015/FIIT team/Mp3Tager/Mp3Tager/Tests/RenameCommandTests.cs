using CommandCreation;
using FileLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Tests.Fakes;

namespace Tests
{
    [TestClass]
    public class RenameCommandTests
    {
        private Command _command;
        private IMp3File _file;

        [TestInitialize]
        public void SetUp()
        {
            _file = new FakeMp3File(new Mp3Tags(), @"D:\TestFile.mp3", new FakeUniquePathCreator());
            _file.Tags.Album = "TestAlbum";
            _file.Tags.Artist = "TestPerformer";
            _file.Tags.Genre = "TestGenre";
            _file.Tags.Title = "TestTitle";
            _file.Tags.Track = 1;
        }

        [TestMethod]
        public void RenameTestExecute_Successful()
        {
            _command = new RenameCommand(_file, new FakeUniquePathCreator(),
                TagNames.Track + ". " + TagNames.Artist + " - " + TagNames.Title);
            _command.Execute();

            Assert.AreEqual(@"D:\1. TestPerformer - TestTitle.mp3", _file.FullName);

        }

        [TestMethod]
        public void Rename_ComplexPattern_SuccessfulRename()
        {
            _command = new RenameCommand(_file, new FakeUniquePathCreator(),
                @"{asd" + TagNames.Artist + "}-.." + TagNames.Title);
            _command.Execute();

            Assert.AreEqual(@"D:\{asdTestPerformer}-..TestTitle.mp3", _file.FullName);
        }

        [TestMethod]
        public void Rename_FileWithSuchNameAlreadyExists_UniquePathCreated()
        {
            _command = new RenameCommand(_file, new FakeUniquePathCreator(),
                TagNames.Artist);
            _command.Execute();

            Assert.AreEqual(@"D:\TestPerformer (2).mp3", _file.FullName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Rename_EmptyPattern_ArgumentException()
        {
            _command = new RenameCommand(_file, new FakeUniquePathCreator(), @"");
            _command.Execute();
        }
    }
}
