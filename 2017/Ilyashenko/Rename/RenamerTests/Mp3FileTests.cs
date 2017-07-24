using Microsoft.VisualStudio.TestTools.UnitTesting;
using RenamerLibrary;
using System.IO;

namespace RenamerTests
{
    [TestClass]
    public class Mp3FileTests
    {
        [TestMethod]
        public void MakeTags()
        {
            var file = new TestableMp3File(@"Led Zeppelin - Kashmir.mp3");
            file.MakeTags();

            Assert.AreEqual("Led Zeppelin", file.Artist);
            Assert.AreEqual("Kashmir", file.Title);
        }

        [TestMethod]
        public void MakeFilename()
        {
            var file = new TestableMp3File("Kashmir.mp3");
            file.Artist = "Led Zeppelin";
            file.Title = "Kashmir";
            file.MakeFilename();

            Assert.AreEqual("Led Zeppelin - Kashmir", Path.GetFileNameWithoutExtension(file.path));
        }
    }
}
