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

                new FakeMp3File(new Mp3Tags
                {
                    Album = "TestAlbum2", Artist = "TestArtist2", Genre = "TestGenre2", Title = "TestTitle2", Track = 2
                }, sourceFolder + "TestArtist2 - TestTitle2.mp3"),

                //new FakeMp3File(new Mp3Tags
                //{
                //    Album = "TestAlbum3", Artist = "TestArtist3", Genre = "TestGenre3", Title = "TestTitle3", Track = 3
                //}, sourceFolder + "3.mp3"),
            };

            var fakeSystemSource = new FakeSystemSource(sourceFolder, mp3Files);

            var fakeWriter = new FakeWriter();
            var analyser = new AnalyseCommand(fakeSystemSource, @"{artist} - {title}", fakeWriter);

            analyser.Execute();

            Assert.AreEqual("File: " + mp3Files[0].FullName + "\n" + 
                "{title} in file name: TestTitle2; {title} in tags: TestTitle1\n\n", fakeWriter.Stream.ToString());
        }        

        [TestMethod]
        public void Analyse_WithoutDifferences_SuccessfulAnalyse()
        {
            var sourceFolder = @"D:\music\";

            var mp3Files = new List<IMp3File>
            {
                new FakeMp3File(new Mp3Tags
                {
                    Artist = "Alla", Title = "Arlekino"
                }, sourceFolder + "Alla - Arlekino.mp3")
            };

            var fakeSystemSource = new FakeSystemSource(sourceFolder, mp3Files);

            var fakeWriter = new FakeWriter();
            var analyser = new AnalyseCommand(fakeSystemSource, @"{artist} - {title}", fakeWriter);

            analyser.Execute();

            Assert.AreEqual("", fakeWriter.Stream.ToString());
        }

        [TestMethod]
        public void Analyse_ManyDifferences_SuccessfulAnalyse()
        {

            var sourceFolder = @"D:\music\";

            var mp3Files = new List<IMp3File>
            {
                new FakeMp3File(new Mp3Tags
                {
                    Artist = "Alla", Title = "Arlekino", Track = 1
                }, sourceFolder + "2. Alla - Sneg.mp3"),

                new FakeMp3File(new Mp3Tags
                {
                    Artist = "Filipp", Title = "Sneg", Track = 2
                }, sourceFolder + "2. Alla - Arlekino.mp3")
            };

            var fakeSystemSource = new FakeSystemSource(sourceFolder, mp3Files);

            var fakeWriter = new FakeWriter();
            var analyser = new AnalyseCommand(fakeSystemSource, @"{track}. {artist} - {title}", fakeWriter);

            analyser.Execute();

            Assert.AreEqual("File: " + mp3Files[0].FullName + "\n" +
                            "{track} in file name: 2; {track} in tags: 1\n" +
                            "{title} in file name: Sneg; {title} in tags: Arlekino\n\n" +
                            "File: " + mp3Files[1].FullName + "\n" +
                            "{artist} in file name: Alla; {artist} in tags: Filipp\n" +
                            "{title} in file name: Arlekino; {title} in tags: Sneg\n\n",
                            fakeWriter.Stream.ToString());
        }
    }
}
