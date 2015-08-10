using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mp3Lib;

namespace Tests
{
    [TestClass]
    public class Mp3ManipulationsTests_ChangeTags
    {
        [TestMethod]
        public void ChangeTags_CommonMask_SuccessfulChange()
        {
            // Init
            var fakeMp3File = new FakeMp3File(@"D:\music\Alla-Arlekino.mp3", new Mp3Tags());
            var mp3Manipulations = new Mp3Manipulations(fakeMp3File);

            // Act
            mp3Manipulations.ChangeTags(@"{artist}-{title}");

            // Assert
            Assert.AreEqual(fakeMp3File.Mp3Tags.Artist, "Alla");
            Assert.AreEqual(fakeMp3File.Mp3Tags.Title, "Arlekino");
        }

        [TestMethod]
        public void ChangeTags_ComplexMask_SuccessfulChange()
        {
            // Init
            var fakeMp3File = new FakeMp3File(@"D:\music\{ssAlla}-Arlekino..mp3", new Mp3Tags());
            var mp3Manipulations = new Mp3Manipulations(fakeMp3File);

            // Act
            mp3Manipulations.ChangeTags(@"{ss{artist}}-{title}.");

            // Assert
            Assert.AreEqual(fakeMp3File.Mp3Tags.Artist, "Alla");
            Assert.AreEqual(fakeMp3File.Mp3Tags.Title, "Arlekino");
        }

        [TestMethod]
        public void ChangeTags_EmptyMask_WithoutChanges()
        {
            // Init
            var fakeMp3File = new FakeMp3File(@"D:\music\Alla-Arlekino.mp3", new Mp3Tags());
            var mp3Manipulations = new Mp3Manipulations(fakeMp3File);

            // Act
            mp3Manipulations.ChangeTags(@"");

            // Assert
            Assert.AreEqual(fakeMp3File.Mp3Tags.Artist, null);
            Assert.AreEqual(fakeMp3File.Mp3Tags.Title, null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void ChangeTags_WrongMask_InBegin_InvalidDataException()
        {
            // Init
            var fakeMp3File = new FakeMp3File(@"D:\music\Alla-Arlekino.mp3", new Mp3Tags());
            var mp3Manipulations = new Mp3Manipulations(fakeMp3File);

            // Act
            mp3Manipulations.ChangeTags(@"..{artist}-{title}");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void ChangeTags_WrongMask_NearlySplit_InvalidDataException()
        {
            // Init
            var fakeMp3File = new FakeMp3File(@"D:\music\Alla-Arlekino.mp3", new Mp3Tags());
            var mp3Manipulations = new Mp3Manipulations(fakeMp3File);

            // Act
            mp3Manipulations.ChangeTags(@"{artist}-..{title}");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void ChangeTags_WrongMask_DifferentSplits_InvalidDataException()
        {
            // Init
            var fakeMp3File = new FakeMp3File(@"D:\music\Alla-Arlekino.mp3", new Mp3Tags());
            var mp3Manipulations = new Mp3Manipulations(fakeMp3File);

            // Act
            mp3Manipulations.ChangeTags(@"{artist}.{title}");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void ChangeTags_SplitsInFilenameMoreThanInMask_InvalidDataException()
        {
            // Init
            var fakeMp3File = new FakeMp3File(@"D:\music\-Alla-Arlekino.mp3", new Mp3Tags());
            var mp3Manipulations = new Mp3Manipulations(fakeMp3File);

            // Act
            mp3Manipulations.ChangeTags(@"{artist}-{title}");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void ChangeTags_AmbiguousMatchingOfMaskAndFilename_InvalidDataException()
        {
            // Init
            var fakeMp3File = new FakeMp3File(@"D:\music\Alla-Arlekino.mp3", new Mp3Tags());
            var mp3Manipulations = new Mp3Manipulations(fakeMp3File);

            // Act
            mp3Manipulations.ChangeTags(@"{artist}{title}");
        }
    }
}
