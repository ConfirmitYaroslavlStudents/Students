using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RenamerLibrary;

namespace RenamerTests
{
    [TestClass]
    public class ArgumentParserTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseWithEmptyArguments()
        {
            (new ArgumentParser()).Parse(new string[] { });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseWithTooManyArguments()
        {
            (new ArgumentParser()).Parse(new string[] { "*.mp3", "-recursive", "-toFileName", "-toTag" });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseWithWrongArguments()
        {
            (new ArgumentParser()).Parse(new string[] { "*.mp4", "-recursive", "-wrongArgument" });
        }

        [TestMethod]
        public void ParseToTag()
        {
            var args = (new ArgumentParser()).Parse(new string[] { "*.mp3", "-recursive", "-toTag" });

            Assert.AreEqual("MakeTag", args.Action);
            Assert.AreEqual(true, args.IsRecursive);
            Assert.AreEqual("*.mp3", args.SearchPattern);
        }

        [TestMethod]
        public void ParseToFilename()
        {
            var args = (new ArgumentParser()).Parse(new string[] { "*.mp3", "-toFileName" });

            Assert.AreEqual("MakeFilename", args.Action);
            Assert.AreEqual(false, args.IsRecursive);
            Assert.AreEqual("*.mp3", args.SearchPattern);
        }
    }
}
