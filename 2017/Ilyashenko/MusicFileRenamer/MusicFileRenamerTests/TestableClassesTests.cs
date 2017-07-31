using Microsoft.VisualStudio.TestTools.UnitTesting;
using MusicFileRenamerLib;

namespace MusicFileRenamerTests
{
    [TestClass]
    public class TestableClassesTests
    {
        [TestMethod]
        public void TestableTagMaker()
        {
            var file = new Mp3File(@"Led Zeppelin - Kashmir.mp3");
            var tagMaker = new TestableTagMaker();
            tagMaker.MakeTags(file);

            Assert.AreEqual("Led Zeppelin", file.Artist);
            Assert.AreEqual("Kashmir", file.Title);
        }

        [TestMethod]
        public void TestableFilenameMaker()
        {
            var file = new Mp3File("sample.mp3");
            file.Artist = "Led Zeppelin";
            file.Title = "The Song Remains The Same";

            var tagMaker = new TestableFilenameMaker();
            tagMaker.MakeFilename(file);

            Assert.AreEqual(@"\Led Zeppelin - The Song Remains The Same.mp3", file.path);
        }
    }
}
