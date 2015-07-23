using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using RetagerLib;

namespace Tests
{
    //[TODO] more tests on "CodeBehind.cs"
    [TestClass]
    public class PareserTests
    {
        [TestMethod]
        public void OneTagPattern()
        {
            var expect = "artist";

            var parser = new TagParser("<ar>");
            var frames = parser.GetFrames(expect);

            Assert.AreEqual(1, frames.Count);
            Assert.AreEqual(expect, frames[FrameType.Artist]);
        }

        [TestMethod]
        public void TwoTagPattern()
        {
            var artist = "artist";
            var title = "title";
            var expect = artist + " - " + title;

            var parser = new TagParser("<ar> - <ti>");
            var frames = parser.GetFrames(expect);

            Assert.AreEqual(artist, frames[FrameType.Artist]);
            Assert.AreEqual(title, frames[FrameType.Title]);
        }

        [TestMethod]
        public void FakeEndOfTagPattern()
        {
            var artist = "arti -st";
            var title = "title";
            var expect = artist + " - " + title;

            var parser = new TagParser("<ar> - <ti>");
            var frames = parser.GetFrames(expect);

            Assert.AreEqual(artist, frames[FrameType.Artist]);
            Assert.AreEqual(title, frames[FrameType.Title]);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void NotExistingTagException()
        {
            var parser = new TagParser("<a>");
            var frames = parser.GetFrames("msg");
        }

        [TestMethod]
        public void NotCorrectPattern()
        {
            var parser = new TagParser("<ar><al>");
            Assert.AreEqual(null, parser.GetFrames("msg"));
        }
    }
}
