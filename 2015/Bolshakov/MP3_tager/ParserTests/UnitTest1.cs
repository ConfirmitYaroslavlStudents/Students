using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MP3_tager;
using System.Collections.Generic;

namespace ParserTests
{
    //[TODO] more tests on "CodeBehind.cs"
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void OneTagPattern()
        {
            var except = "artist";

            var parser = new TagParser("<ar>");
            var frames = parser.GetFrames(except);

            Assert.AreEqual(except, frames[FrameType.Artist]);
        }

        [TestMethod]
        public void TwoTagPattern()
        {
            var artist = "artist";
            var title = "title";
            var except = artist + " - " + title;

            var parser = new TagParser("<ar> - <ti>");
            var frames = parser.GetFrames(except);

            Assert.AreEqual(artist, frames[FrameType.Artist]);
            Assert.AreEqual(title, frames[FrameType.Title]);
        }

        [TestMethod]
        public void HardCase1Pattern()
        {
            var artist = "arti -st";
            var title = "title";
            var except = artist + " - " + title;

            var parser = new TagParser("<ar> - <ti>");
            var frames = parser.GetFrames(except);

            Assert.AreEqual(artist, frames[FrameType.Artist]);
            Assert.AreEqual(title, frames[FrameType.Title]);
        }

        [TestMethod]
        public void HardCase2Pattern()
        {
            var artist = "artist";
            var title = "tit -le";
            var except = artist + " - " + title;

            var parser = new TagParser("<ar> - <ti>");
            var frames = parser.GetFrames(except);

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
