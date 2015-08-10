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
            Assert.AreEqual("Alla", fakeMp3File.Mp3Tags.Artist);
            Assert.AreEqual("Arlekino", fakeMp3File.Mp3Tags.Title);
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
            Assert.AreEqual("Alla", fakeMp3File.Mp3Tags.Artist);
            Assert.AreEqual( "Arlekino",fakeMp3File.Mp3Tags.Title);
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
            Assert.AreEqual("Alla", fakeMp3File.Mp3Tags.Artist);
            Assert.AreEqual("Arlekino", fakeMp3File.Mp3Tags.Title);
            Assert.AreEqual("Pop", fakeMp3File.Mp3Tags.Genre);
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
            Assert.IsNull(fakeMp3File.Mp3Tags.Artist);
            Assert.IsNull(fakeMp3File.Mp3Tags.Title);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void ChangeTags_WrongMaskDifferentOrderOfSplits_InvalidDataException()
        {
            // Init
            var fakeMp3File = new FakeMp3File(@"D:\music\.Alla.Arlekino.mp3", new Mp3Tags());
            var mp3Manipulations = new Mp3Manipulations(fakeMp3File);

            // Act
            mp3Manipulations.ChangeTags(@"{artist}.{title}.");
            
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
        public void ChangeTags_WrongMask_SplitsInDifferentOrder_InvalidDataException()
        {
            // Init
            var fakeMp3File = new FakeMp3File(@"D:\music\_def_Arlekino_abc_.mp3", new Mp3Tags());
            var mp3Manipulations = new Mp3Manipulations(fakeMp3File);

            // Act
            mp3Manipulations.ChangeTags(@"_abc_{artist}_def_");
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
