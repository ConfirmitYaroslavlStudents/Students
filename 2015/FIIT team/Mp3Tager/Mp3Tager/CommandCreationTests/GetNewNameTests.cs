using CommandCreation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mp3Lib;

namespace CommandCreationTests
{
    [TestClass]
    public class GetNewNameTests
    {
        private IMp3File _testMp3File;
        private RenameCommand _command;

        [TestInitialize]
        public void SetUp()
        {
            _testMp3File = new FakeMp3File(@"D:\TestFile.mp3", @"D:",
                new TagBase(new[] { "TestPerformer" }, new[] { "TestGenre" },
                "TestTitle", "TestAlbum", 1));
            _command = new RenameCommand(new[] { CommandNames.Rename, @"D:\TestFile.mp3",
                TagNames.Track + ". " + TagNames.Artist + " - " + TagNames.Title });
        }

        [TestMethod]
        public void GetNewNameTest()
        {
            var expected = "1. TestPerformer - TestTitle";
            var actual = _command.GetNewName(_testMp3File);
            Assert.AreEqual(expected, actual);
        }
    }
}
