using System;
using CommandCreation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mp3Lib;

namespace CommandCreationTests
{
    [TestClass]
    public class ChangeTagTests
    {
        private IMp3File _testMp3File;
        private ChangeTagsCommand _command;

        [TestInitialize]
        public void SetUp()
        {
            _testMp3File = new FakeMp3File(@"D:\TestPerformer - TestTitle.mp3", @"D:",
                new TagBase(new[] { "test" }, new[] { "test" },
                "test", "test", 1));
            _command = new ChangeTagsCommand(new[] { CommandNames.ChangeTags,
                @"{artist} - {title}",
                TagNames.Track + ". " + TagNames.Artist + " - " + TagNames.Title });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "There is no such tag.")]
        public void ChangeTagTest_NoSuchTag_ThrowsException()
        {
            _command.ChangeTag(_testMp3File, "someTag", "newValue");
        }

        [TestMethod]
        public void ChangeTagTest_SuccessfulChange()
        {
            var expected = "Alla Pugacheva";
            _command.ChangeTag(_testMp3File, TagNames.Artist, expected);
            Assert.AreEqual(expected, _testMp3File.Tag.Performers[0]);
        }

        [TestMethod]
        public void ChangeTags_SuccessfulChange()
        {
            _command.ChangeTags(_testMp3File, "{artist} - {title}");
            Assert.AreEqual("TestTitle" ,_testMp3File.Tag.Title );
            Assert.AreEqual("TestPerformer", _testMp3File.Tag.Performers[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Wrong mask")]
        public void ChangeTags_ThrowException()
        {
            _command.ChangeTags(_testMp3File, "{artist}.{title}");           
        }
    }
}
