using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mp3TagLib;

namespace Mp3TagTest
{
    [TestClass]
    public class Mp3TagsTestClass
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetTest()
        {
            var tags = new Mp3Tags() { Album = "Album", Artist = "Artist", Comment = "Comment", Genre = "Genre", Title = "Title", Year = 2015 };
            Assert.AreEqual(tags.Artist, tags.GetTag("artist"));
            Assert.AreEqual(tags.Album, tags.GetTag("album"));
            Assert.AreEqual(tags.Comment, tags.GetTag("comment"));
            Assert.AreEqual(tags.Genre, tags.GetTag("genRe"));
            Assert.AreEqual(tags.Title, tags.GetTag("title"));
            Assert.AreEqual(tags.Year, uint.Parse(tags.GetTag("year")));
            tags.GetTag("asrtist");
        }
         
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetTest()
        {
            Mp3Tags tags=new Mp3Tags();
            ArgumentException expectedException=null;
            try
            {
                tags.SetTag(null, "sd");
            }
            catch (ArgumentException e)
            {
                expectedException = e;
            }
            Assert.IsNotNull(expectedException);
            tags.SetTag("artist","test");
            Assert.AreEqual("test",tags.Artist);
            tags.SetTag("album", "test");
            Assert.AreEqual("test", tags.Album);
            tags.SetTag("coMment", "test");
            Assert.AreEqual("test", tags.Comment);
            tags.SetTag("genre", "test");
            Assert.AreEqual("test", tags.Genre);
            tags.SetTag("title", "test");
            Assert.AreEqual("test", tags.Title);
            tags.SetTag("not existing tag","value");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void YearSetTest()
        {
            Mp3Tags tags = new Mp3Tags();
            tags.SetTag("year", "2015");
            Assert.AreEqual(2015u, tags.Year);
            tags.SetTag("year", "two thousand fifteen");
        }
    }
}
