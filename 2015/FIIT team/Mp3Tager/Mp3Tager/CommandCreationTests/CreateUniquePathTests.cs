using CommandCreation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mp3Lib;

namespace CommandCreationTests
{
    [TestClass]
    public class CreateUniquePathTests
    {
        private IFile _testFile;
        private RenameCommand _command;

        [TestInitialize]
        public void SetUp()
        {
            _testFile = new FakeFile();
            _command = new RenameCommand(new[] { CommandNames.Rename, @"D:\TestFile.mp3",
                TagNames.Track + ". " + TagNames.Artist + " - " + TagNames.Title });
        }

        [TestMethod]
        public void CreateUniquePathTest_CreateNameWithIndex()
        {
            var expected = @"D:\TestFile (2).mp3";
            var actual = _command.CreateUniquePath(_testFile, @"D:", "TestFile");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateUniquePathTest_NoSuchFile()
        {
            var expected = @"D:\SomeFile.mp3";
            var actual = _command.CreateUniquePath(_testFile, @"D:", "SomeFile");
            Assert.AreEqual(expected, actual);
        }
    }
}
