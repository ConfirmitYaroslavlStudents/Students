using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mp3Tager;

namespace Mp3LibTests
{
    [TestClass]
    public class ArgumentParserTests
    {
        private ArgumentParser _parser;

        [TestInitialize]
        public void InitializeParser()
        {
            _parser = new ArgumentParser();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "You haven't passed any argument!")]
        public void ParserTest_NoArgumentsPassed_ThrowException()
        {
            // arrange
            string[] args = { };

            // act
            _parser.CreateCommand(args);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Invalid operation: there is no such command!")]
        public void ParserTest_WrongCommand_ThrowException()
        {
            // arrange
            string[] args = { "renam", @"D:\file.mp3", "{title}" };

            // act
            _parser.CreateCommand(args);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Not enough arguments for this command!")]
        public void ParserTest_NotEnoughArgumentsPassed_ThrowException()
        {
            // arrange
            string[] args = { "rename", @"D:\file.mp3" };

            // act
            _parser.CreateCommand(args);
        }

        [TestMethod]
        public void ParserTest_HelpCompleteSuccessfullParsing()
        {
            // arrange
            string[] args = { "help" };
            string expectedCommandName = args[0];

            // act
            var actual = _parser.CreateCommand(args);

            // assert
            Assert.AreEqual(expectedCommandName, actual.CommandName);
            Assert.IsFalse(actual.CommandForHelp != null);
        }

        [TestMethod]
        public void ParserTest_HelpOneCommandSuccessfullParsing()
        {
            // arrange
            string[] args = { "help", "rename" };
            string expectedCommandName = args[0];
            string expectedCommandForHelp = args[1];

            // act
            var actual = _parser.CreateCommand(args);

            // assert
            Assert.AreEqual(expectedCommandName, actual.CommandName);
            Assert.AreEqual(expectedCommandForHelp, actual.CommandForHelp);
        }

        [TestMethod]
        public void ParserTest_RenameSuccessfullParsing()
        {
            // arrange
            string[] args = { "rename", @"D:\file.mp3", "{title}" };
            string expectedCommandName = args[0];
            string expectedPath = args[1];
            string expectedPattern = args[2];

            // act
            var actual = _parser.CreateCommand(args);

            // assert
            Assert.AreEqual(expectedCommandName, actual.CommandName);
            Assert.AreEqual(expectedPath, actual.Path);
            Assert.AreEqual(expectedPattern, actual.Pattern);
        }

        [TestMethod]
        public void ParserTest_ChangeTagSuccessfullParsing()
        {
            // arrange
            string[] args = { "changeTag", @"D:\file.mp3", "{title}", "My Title" };
            string expectedCommandName = args[0];
            string expectedPath = args[1];
            string expectedTag = args[2];
            string expectedNewTagValue = args[3];

            // act
            var actual = _parser.CreateCommand(args);

            // assert
            Assert.AreEqual(expectedCommandName, actual.CommandName);
            Assert.AreEqual(expectedPath, actual.Path);
            Assert.AreEqual(expectedTag, actual.Tag);
            Assert.AreEqual(expectedNewTagValue, actual.NewTagValue);
        }
    }
}
