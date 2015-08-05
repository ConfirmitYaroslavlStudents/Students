using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mp3Handler;

namespace Tests
{
    //[TODO] more tests on "CodeBehind.cs"
    [TestClass]
    public class PareserTests
    {
        [TestMethod]
        public void OneTagValueParsing()
        {
            var expect = "artist";

            var parser = new TagParser("<ar>");
            var frames = parser.GetTagsValue(expect);

            Assert.AreEqual(1, frames.Count);
            Assert.AreEqual(expect, frames[FrameType.Artist]);
        }

        [TestMethod]
        public void TwoTagValueParsing()
        {
            var artist = "artist";
            var title = "title";
            var expect = artist + " - " + title;

            var parser = new TagParser("<ar> - <ti>");
            var frames = parser.GetTagsValue(expect);

            Assert.AreEqual(artist, frames[FrameType.Artist]);
            Assert.AreEqual(title, frames[FrameType.Title]);
        }

        [TestMethod]
        public void FakeEndOfTagValueParsing()
        {
            var artist = "arti -st";
            var title = "title";
            var expect = artist + " - " + title;

            var parser = new TagParser("<ar> - <ti>");
            var frames = parser.GetTagsValue(expect);
            Assert.AreEqual(artist, frames[FrameType.Artist]);
            Assert.AreEqual(title, frames[FrameType.Title]);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void NotExistingTagValueParsingException()
        {
            var parser = new TagParser("<a>");
            var frames = parser.GetTagsValue("msg");
        }

        [TestMethod]
        public void NotCorrectPatternValueParsing()
        {
            var parser = new TagParser("<ar><al>");
            Assert.AreEqual(null, parser.GetTagsValue("msg"));
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void NotCompleteTagValueParsing()
        {
            var parser = new TagParser("<ar");
            parser.GetTagsValue("msg");
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void NotCompleteTag2ValueParsing()
        {
            var parser = new TagParser("<ar s <ti>");
            parser.GetTagsValue("m s g");
        }

        [TestMethod]
        public void FrameParsing()
        {
            var parser = new TagParser("<ar><ti>");
            var expect = new List<FrameType>()
            {
                FrameType.Artist,FrameType.Title
            };

            CollectionAssert.AreEquivalent(expect,parser.GetTags());
        }

        [TestMethod]
        public void DublicateTagFrameParsing()
        {
            var parser = new TagParser("<ar><ar>");
            parser.GetTags();

            var expect = new List<FrameType>()
            {
                FrameType.Artist
            };

            CollectionAssert.AreEquivalent(expect, parser.GetTags());
        }
    }
}
