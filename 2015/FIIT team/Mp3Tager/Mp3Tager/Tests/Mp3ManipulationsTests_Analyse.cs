/*
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileLib;

namespace Tests
{
    [TestClass]
    public class Mp3ManipulationsTests_Analyse
    {
        [TestMethod]
        public void Analyse_DifferentArtists_SuccessfulAnalyse()
        {
            // Init
            var fakeMp3File = new FakeMp3File(@"D:\music\Fillip-Arlekino.mp3", new Mp3Tags
            {
                Artist = "Alla",
                Title = "Arlekino"
            });
            var mp3Manipulations = new Mp3Manipulations(fakeMp3File);

            // Act
            var message = mp3Manipulations.Analyse(@"{artist}-{title}");

            // Assert
            Assert.AreEqual("{artist} in file name: Fillip; {artist} in tags: Alla", message);
        }

        [TestMethod]
        public void Analyse_WithoutDifferences_SuccessfulAnalyse()
        {
            // Init
            var fakeMp3File = new FakeMp3File(@"D:\music\Alla-Arlekino.mp3", new Mp3Tags
            {
                Artist = "Alla",
                Title = "Arlekino"
            });
            var mp3Manipulations = new Mp3Manipulations(fakeMp3File);

            // Act
            var message = mp3Manipulations.Analyse(@"{artist}-{title}");

            // Assert
            Assert.AreEqual("", message);
        }

        [TestMethod]
        public void Analyse_ManyDifferences_SuccessfulAnalyse()
        {
            // Init
            var fakeMp3File = new FakeMp3File(@"D:\music\2. Fillip-Tap.mp3", new Mp3Tags
            {
                Track = 1,
                Artist = "Alla",
                Title = "Arlekino"
            });
            var mp3Manipulations = new Mp3Manipulations(fakeMp3File);

            // Act
            var message = mp3Manipulations.Analyse(@"{track}. {artist}-{title}");

            // Assert
            Assert.AreEqual("{track} in file name: 2; {track} in tags: 1" +
                            "{artist} in file name: Fillip; {artist} in tags: Alla" +
                            "{title} in file name: Tap; {title} in tags: Arlekino", message);
        }
    }
}
*/
