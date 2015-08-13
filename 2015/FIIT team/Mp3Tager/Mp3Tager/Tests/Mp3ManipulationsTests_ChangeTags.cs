/*
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileLib;
using System;

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
            Assert.AreEqual("Alla", fakeMp3File.Tags.Artist);
            Assert.AreEqual("Arlekino", fakeMp3File.Tags.Title);
        }

        [TestMethod]
        public void ChangeTags_OneTag_SuccessfulChange()
        {
            // Init
            var fakeMp3File = new FakeMp3File(@"D:\music\Alla.mp3", new Mp3Tags());
            var mp3Manipulations = new Mp3Manipulations(fakeMp3File);

            // Act
            mp3Manipulations.ChangeTags(@"{artist}");

            // Assert
            Assert.AreEqual("Alla", fakeMp3File.Tags.Artist);            
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
            Assert.AreEqual("Alla", fakeMp3File.Tags.Artist);
            Assert.AreEqual("Arlekino", fakeMp3File.Tags.Title);
        }

        [TestMethod]
        public void ChangeTags_SimilarSplits_SuccessfulChange()
        {
            // Init
            var fakeMp3File = new FakeMp3File(@"D:\music\Alla.Arlekino.Pop.mp3", new Mp3Tags());
            var mp3Manipulations = new Mp3Manipulations(fakeMp3File);

            // Act
            mp3Manipulations.ChangeTags(@"{artist}.{title}.{genre}");

            // Assert
            Assert.AreEqual("Alla", fakeMp3File.Tags.Artist);
            Assert.AreEqual("Arlekino", fakeMp3File.Tags.Title);
            Assert.AreEqual("Pop", fakeMp3File.Tags.Genre);
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
            Assert.IsNull(fakeMp3File.Tags.Artist);
            Assert.IsNull(fakeMp3File.Tags.Title);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void ChangeTags_WrongMask_DifferentOrderOfSplits_InvalidDataException()
        {
            // Init
            var fakeMp3File = new FakeMp3File(@"D:\music\.Alla.Arlekino.mp3", new Mp3Tags());
            var mp3Manipulations = new Mp3Manipulations(fakeMp3File);

            // Act
            mp3Manipulations.ChangeTags(@"{artist}.{title}.");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void ChangeTags_WrongMask_DifferentOrderOfSplits2_InvalidDataException()
        {
            // Init
            var fakeMp3File = new FakeMp3File(@"D:\music\_def_Arlekino_abc_.mp3", new Mp3Tags());
            var mp3Manipulations = new Mp3Manipulations(fakeMp3File);

            // Act
            mp3Manipulations.ChangeTags(@"_abc_{artist}_def_");
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

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ChangeTags_TagDoesNotExist_ArgumentException()
        {
            // Init
            var fakeMp3File = new FakeMp3File(@"D:\music\Alla-Arlekino.mp3", new Mp3Tags());
            var mp3Manipulations = new Mp3Manipulations(fakeMp3File);

            // Act
            mp3Manipulations.ChangeTags(@"{sometag}-{title}");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ChangeTag_TagTrackIsNotInt_FormatException()
        {
            // Init
            var fakeMp3File = new FakeMp3File(@"D:\music\one. Alla-Arlekino.mp3", new Mp3Tags());
            var mp3Manipulations = new Mp3Manipulations(fakeMp3File);

            // Act
            mp3Manipulations.ChangeTags(@"{track}. {sometag}-{title}");

        }
    }
}
*/
