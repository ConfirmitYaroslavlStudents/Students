using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mp3Lib;
using Tests.Fakes;

namespace Tests
{
    [TestClass]
    public class Mp3ManipulationsTests_Rename
    {
        private IMp3File _fakeMp3File;
        private Mp3Manipulations _mp3Manipulations;

        [TestInitialize]
        public void SetUp()
        {
            _fakeMp3File = new FakeMp3File(@"D:\music\audio.mp3", new Mp3Tags
            {
                Artist = "Alla",
                Title = "Arlekino"
            });

            _mp3Manipulations = new Mp3Manipulations(_fakeMp3File);
        }

        [TestMethod]
        public void Rename_Successful()
        {
            _mp3Manipulations.Rename("{artist}-{title}", new FakeFileExistenceChecker());

            Assert.AreEqual(_fakeMp3File.Path, @"D:\music\Alla-Arlekino.mp3");
        }

        [TestMethod] 
        public void Rename_FileWithSuchNameAlreadyExists_UniquePathCreated()
        {            
            _mp3Manipulations.Rename("{artist}", new FakeFileExistenceChecker());

            Assert.AreEqual(_fakeMp3File.Path, @"D:\music\Alla (2).mp3");
        }        
    }
}
