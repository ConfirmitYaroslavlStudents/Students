using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mp3Lib;
using Tests.Fakes;

namespace Tests
{
    [TestClass]
    class Mp3ManipulationsTests_ChangeTags
    {
        private IMp3File _fakeMp3File;
        private Mp3Manipulations _mp3Manipulations;
        [TestInitialize]
        public void SetUp()
        {
            _fakeMp3File = new FakeMp3File(@"D:\music\Alla-Arlekino.mp3", new Mp3Tags());
            _mp3Manipulations = new Mp3Manipulations(_fakeMp3File);

        }

        [TestMethod]
        public void ChangeTags_Successful()
        {            
            _mp3Manipulations.ChangeTags(@"{artist}-{title}");

            Assert.AreEqual(_fakeMp3File.Mp3Tags.Artist, "Alla");
            Assert.AreEqual(_fakeMp3File.Mp3Tags.Title, "Arlekino");
        }        
    }
}
