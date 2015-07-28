using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mp3Lib;

namespace Mp3LibTests
{
    [TestClass]
    public class Mp3LibActionsTests
    {
        readonly string[] _performers = { "Imagine Dragons" };
        readonly string[] _genres = { "Alternative rock" };
        readonly string _title = "Radioactive";
        readonly string _album = "Night Visions";
        readonly uint _track = 1;
        readonly string _path = @"D:\audio.mp3";
        readonly string _directoryName = @"D:";
        readonly string _pattern = "{track}. {artist} - {title}";
        Actions _actions = new Actions();
        
        /*[TestMethod]
        public void RenameTest()
        {
            // arrange
            TagBase tagsOfFile = new TagBase(_performers, _genres, _title, _album, _track);
            FakeMp3File fileToTest = new FakeMp3File(_path, _directoryName, tagsOfFile);
            string expected = @"D:\1. Imagine Dragons - Radioactive.mp3";
            
            // act
            _actions.Rename(fileToTest, _pattern);
            
            // assert
            Assert.AreEqual(expected, fileToTest.Path);
        }*/

        [TestMethod]
        public void ChangeTagTest_ChangeSuccsesful()
        {
            // arrange
            TagBase tagsOfFile = new TagBase(_performers, _genres, _title, _album, _track);
            FakeMp3File fileToTest = new FakeMp3File(_path, _directoryName, tagsOfFile);
            string TagTitle = "{genre}";
            string newTagValue = "Pop";
            string expected = newTagValue;

            // act
            _actions.ChangeTag(fileToTest, TagTitle, newTagValue);

            // assert
            Assert.AreEqual(expected, fileToTest.Tag.Genres[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "There is no such tag.")]
        public void ChangeTagTest_CannotChangeTag_ThrowException()
        {
            // arrange
            TagBase tagsOfFile = new TagBase(_performers, _genres, _title, _album, _track);
            FakeMp3File fileToTest = new FakeMp3File(_path, _directoryName, tagsOfFile);
            string TagTitle = "{genr}";
            string newTagValue = "Some genre";
            
            // act
            _actions.ChangeTag(fileToTest, TagTitle, newTagValue);
        }
    }
}
