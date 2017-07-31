using MusicFileRenameLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileRenamerTest
{
    [TestClass]
    public class MusicFileRenamerTest
    {
        private MusicFileRenamer _renamer;
        private string filePath;
        private string artistTag;
        private string titleTag;

        [TestInitialize]
        public void InitFile()
        {
            filePath = @"\music\favorite\Petrov-Windows.mp3";
            artistTag = "Ivanov";
            titleTag = "Linux";
        }

        [TestMethod]
        public void ToTagRename()
        {
            _renamer = new MusicFileRenamer(new TagRenamer());
            var actual = _renamer.RenameMusicFile(filePath, artistTag, titleTag);

            var expectedFilePath = filePath;
            var expectedArtistTag = "Petrov";
            var expectedTitleTag = "Windows";

            Assert.AreEqual(expectedFilePath, actual.FullFilePath);
            Assert.AreEqual(expectedArtistTag, actual.TagArtist);
            Assert.AreEqual(expectedTitleTag, actual.TagTitle);
        }

        [TestMethod]
        public void ToFileNameRename()
        {
            _renamer = new MusicFileRenamer(new FileNameRenamer());
            var actual = _renamer.RenameMusicFile(filePath, artistTag, titleTag);

            var expectedFilePath = @"\music\favorite\Ivanov-Linux.mp3";
            var expectedArtistTag = artistTag;
            var expectedTitleTag = titleTag;

            Assert.AreEqual(expectedFilePath, actual.FullFilePath);
            Assert.AreEqual(expectedArtistTag, actual.TagArtist);
            Assert.AreEqual(expectedTitleTag, actual.TagTitle);
        }

        [TestMethod]
        public void DoNotRenameUnknownExtensionFile()
        {
            _renamer = new MusicFileRenamer(new FileNameRenamer());

            var fileWithUnknownExtension = @"\music\favorite\Petrov-Windows.cs";

            var actual = _renamer.RenameMusicFile(fileWithUnknownExtension, artistTag, titleTag);

            var expectedFilePath = fileWithUnknownExtension;
            var expectedArtistTag = artistTag;
            var expectedTitleTag = titleTag;

            Assert.AreEqual(expectedFilePath, actual.FullFilePath);
            Assert.AreEqual(expectedArtistTag, actual.TagArtist);
            Assert.AreEqual(expectedTitleTag, actual.TagTitle);
        }
    }
}
