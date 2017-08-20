using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MP3Renamer;
using TagLib;
using RenamerLib;

namespace MP3Renamer.Tests
{
    [TestClass]
    public class ArgumentsTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseWithNoArguments()
        {
            ArgumentsParser parser = new ArgumentsParser();
            Arguments arguments = parser.ParseArguments(new string[] { });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseWithLessArguments()
        {
            ArgumentsParser parser = new ArgumentsParser();
            Arguments arguments = parser.ParseArguments(new string[] {"*.mp3"});
        }

        [TestMethod]
        public void ParseArgumentsRecusiveToTag()
        {
            ArgumentsParser parser = new ArgumentsParser();
            Arguments arguments = parser.ParseArguments(new string[] { "*.mp3", "-recursive", "-toTag" });

            Assert.AreEqual("*.mp3", arguments.Mask);
            Assert.IsTrue(arguments.IsRecursive);
            Assert.AreEqual(AllowedActions.toTag, arguments.Action);
        }

        [TestMethod]
        public void ParseArgumentsRecusiveToFileName()
        {
            ArgumentsParser parser = new ArgumentsParser();
            Arguments arguments = parser.ParseArguments(new string[] { "*.mp3", "-recursive", "-toFileName" });

            Assert.AreEqual("*.mp3", arguments.Mask);
            Assert.IsTrue(arguments.IsRecursive);
            Assert.AreEqual(AllowedActions.toFileName, arguments.Action);
        }

        [TestMethod]
        public void ParseArgumentsToTag()
        {
            ArgumentsParser parser = new ArgumentsParser();
            Arguments arguments = parser.ParseArguments(new string[] { "*.mp3", "-toTag" });

            Assert.AreEqual("*.mp3", arguments.Mask);
            Assert.IsFalse(arguments.IsRecursive);
            Assert.AreEqual(AllowedActions.toTag, arguments.Action);
        }

        [TestMethod]
        public void ParseArgumentsToFileName()
        {
            ArgumentsParser parser = new ArgumentsParser();
            Arguments arguments = parser.ParseArguments(new string[] { "*.mp3", "-toFileName" });

            Assert.AreEqual("*.mp3", arguments.Mask);
            Assert.IsFalse(arguments.IsRecursive);
            Assert.AreEqual(AllowedActions.toFileName, arguments.Action);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseWithMoreArguments()
        {
            ArgumentsParser parser = new ArgumentsParser();
            Arguments arguments = parser.ParseArguments(new string[] { "*.mp3", "-recursive", "-toFileName",  "-toTag" });
        }
    }
}
