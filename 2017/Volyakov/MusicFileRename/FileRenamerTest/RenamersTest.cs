using System;
using TagLib;
using MusicFileRenameLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileRenamerTest
{
    [TestClass]
    public class RenamersTest
    {
        private IRenamer renamer;
        private FileShell file = new FileShell() { Artist = "Dog", Title  = "Cat",
                                                    TagArtist = "Bob", TagTitle = "Lord"};
        [TestMethod]
        public void CorrectToFileName()
        {
            renamer = new FileNameRenamer();
            renamer.Rename(file);
            Assert.AreEqual("Bob", file.Artist);
            Assert.AreEqual("Lord", file.Title);
        }

        [TestMethod]
        public void CorrectToTag()
        {
            renamer = new TagRenamer();
            renamer.Rename(file);
            Assert.AreEqual("Dog", file.TagArtist);
            Assert.AreEqual("Cat", file.TagTitle);
        }
    }
}
