using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RenamerLib;
using RenamerLib.Arguments;

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
            RenamerArguments renamerArguments = parser.ParseArguments(new string[] { });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseWithLessArguments()
        {
            ArgumentsParser parser = new ArgumentsParser();
            RenamerArguments renamerArguments = parser.ParseArguments(new string[] {"*.mp3"});
        }

        [TestMethod]
        public void ParseArgumentsRecusiveToTag()
        {
            ArgumentsParser parser = new ArgumentsParser();
            RenamerArguments renamerArguments = parser.ParseArguments(new string[] { "*.mp3", "-recursive", "-toTag" });

            Assert.AreEqual("*.mp3", renamerArguments.Mask);
            Assert.IsTrue(renamerArguments.IsRecursive);
            Assert.AreEqual(AllowedActions.ToTag, renamerArguments.Action);
        }

        [TestMethod]
        public void ParseArgumentsRecusiveToFileName()
        {
            ArgumentsParser parser = new ArgumentsParser();
            RenamerArguments renamerArguments = parser.ParseArguments(new string[] { "*.mp3", "-recursive", "-toFileName" });

            Assert.AreEqual("*.mp3", renamerArguments.Mask);
            Assert.IsTrue(renamerArguments.IsRecursive);
            Assert.AreEqual(AllowedActions.ToFileName, renamerArguments.Action);
        }

        [TestMethod]
        public void ParseArgumentsToTag()
        {
            ArgumentsParser parser = new ArgumentsParser();
            RenamerArguments renamerArguments = parser.ParseArguments(new string[] { "*.mp3", "-toTag" });

            Assert.AreEqual("*.mp3", renamerArguments.Mask);
            Assert.IsFalse(renamerArguments.IsRecursive);
            Assert.AreEqual(AllowedActions.ToTag, renamerArguments.Action);
        }

        [TestMethod]
        public void ParseArgumentsToFileName()
        {
            ArgumentsParser parser = new ArgumentsParser();
            RenamerArguments renamerArguments = parser.ParseArguments(new string[] { "*.mp3", "-toFileName" });

            Assert.AreEqual("*.mp3", renamerArguments.Mask);
            Assert.IsFalse(renamerArguments.IsRecursive);
            Assert.AreEqual(AllowedActions.ToFileName, renamerArguments.Action);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseWithMoreArguments()
        {
            ArgumentsParser parser = new ArgumentsParser();
            RenamerArguments renamerArguments = parser.ParseArguments(new string[] { "*.mp3", "-recursive", "-toFileName",  "-toTag" });
        }
    }
}
