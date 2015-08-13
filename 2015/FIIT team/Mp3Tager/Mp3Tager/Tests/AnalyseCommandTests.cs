using System.Collections.Generic;
using CommandCreation;
using FileLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.Fakes;

namespace Tests
{
    [TestClass]
    public class AnalyseCommandTests
    {
        [TestMethod]
        public void Analyse_Common_Successful()
        {
            var sourceFolder = @"D:\music\";

            var mp3Files = new List<IMp3File>
            {
                new FakeMp3File(new Mp3Tags
                {
                    Album = "TestAlbum1", Artist = "TestArtist1", Genre = "TestGenre1", Title = "TestTitle1", Track = 1
                }, sourceFolder + "TestArtist1 - TestTitle2.mp3"),

                //new FakeMp3File(new Mp3Tags
                //{
                //    Album = "TestAlbum2", Artist = "TestArtist2", Genre = "TestGenre2", Title = "TestTitle2", Track = 2
                //}, sourceFolder + "2.mp3"),

                //new FakeMp3File(new Mp3Tags
                //{
                //    Album = "TestAlbum3", Artist = "TestArtist3", Genre = "TestGenre3", Title = "TestTitle3", Track = 3
                //}, sourceFolder + "3.mp3"),
            };

            var fakeSystemSource = new FakeSystemSource(sourceFolder, mp3Files);

            var fakeWriter = new FakeWriter();
            var analyser = new AnalyseCommand(fakeSystemSource, @"{artist} - {title}", fakeWriter);

            analyser.Execute();

            Assert.AreEqual("{title} in file name: TestTitle2; {title} in tags: TestTitle1\n", fakeWriter.Stream.ToString());
        }
    }
}
