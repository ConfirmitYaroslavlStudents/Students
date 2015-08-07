using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mp3Lib;

namespace Tests
{
    [TestClass]
    public class Mp3ManipulationsTests
    {
        [TestMethod]
        public void Rename_Successful()
        {
            var fakeMp3File = new FakeMp3File(@"D:\music\audio.mp3", new Mp3Tags
            {
                Artist = "Alla",
                Title = "Arlekino"
            });

            var mp3Manipulations = new Mp3Manipulations(fakeMp3File);
            mp3Manipulations.Rename("{artist}-{title}");

            Assert.AreEqual(fakeMp3File.Path, @"D:\music\Alla-Arlekino.mp3");
        }

        [TestMethod]
        public void ChangeTags_Successful()
        {
            var fakeMp3File = new FakeMp3File(@"D:\music\Alla-Arlekino.mp3", new Mp3Tags());

            var mp3Manipulations = new Mp3Manipulations(fakeMp3File);

            mp3Manipulations.ChangeTags(@"{artist}-{title}");

            Assert.AreEqual(fakeMp3File.Mp3Tags.Artist, "Alla");
            Assert.AreEqual(fakeMp3File.Mp3Tags.Title, "Arlekino");
        }
    }
}
