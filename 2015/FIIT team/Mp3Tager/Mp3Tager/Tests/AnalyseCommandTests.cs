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
        private readonly BaseDirectory _checker = new FakeMp3Directory();
        private const string SourceFolder = @"D:\music\";
        private List<IMp3File> _mp3Files;
        private string _mask;
        private string _expectedMessage;

        [TestCleanup]
        public void CleanUp()
        {
            var analyser = new AnalyseCommand(_mp3Files, _mask);
            var actualMessage = analyser.Execute();
            Assert.AreEqual(_expectedMessage, actualMessage);
        }

        [TestMethod]
        public void Analyse_Common_Successful()
        {
            _mp3Files = new List<IMp3File>
            {
                new FakeMp3File(new Mp3Tags
                {
                    Album = "TestAlbum1", Artist = "TestArtist1", Genre = "TestGenre1", Title = "TestTitle1", Track = 1
                }, SourceFolder + "TestArtist1 - TestTitle2.mp3", _checker),

                new FakeMp3File(new Mp3Tags
                {
                    Album = "TestAlbum2", Artist = "TestArtist2", Genre = "TestGenre2", Title = "TestTitle2", Track = 2
                }, SourceFolder + "TestArtist2 - TestTitle2.mp3", _checker),
            };

            _mask = "{artist} - {title}";
            _expectedMessage = "File: " + _mp3Files[0].FullName + "\n" +
                "{title} in file name: TestTitle2; {title} in tags: TestTitle1\n\n";
        }

        [TestMethod]
        public void Analyse_WithoutDifferences_SuccessfulAnalyse()
        {
            _mp3Files = new List<IMp3File>
            {
                new FakeMp3File(new Mp3Tags
                {
                    Artist = "Alla", Title = "Arlekino"
                }, SourceFolder + "Alla - Arlekino.mp3", _checker)
            };

            _mask = "{artist} - {title}";
            _expectedMessage = "";
        }

        [TestMethod]
        public void Analyse_ManyDifferences_SuccessfulAnalyse()
        {
            _mp3Files = new List<IMp3File>
            {
                new FakeMp3File(new Mp3Tags
                {
                    Artist = "Alla", Title = "Arlekino", Track = 1
                }, SourceFolder + "2. Alla - Sneg.mp3", _checker),

                new FakeMp3File(new Mp3Tags
                {
                    Artist = "Filipp", Title = "Sneg", Track = 2
                }, SourceFolder + "2. Alla - Arlekino.mp3", _checker)
            };

            _mask = "{track}. {artist} - {title}";
            _expectedMessage = "File: " + _mp3Files[0].FullName + "\n" +
                            "{track} in file name: 2; {track} in tags: 1\n" +
                            "{title} in file name: Sneg; {title} in tags: Arlekino\n\n" +
                            "File: " + _mp3Files[1].FullName + "\n" +
                            "{artist} in file name: Alla; {artist} in tags: Filipp\n" +
                            "{title} in file name: Arlekino; {title} in tags: Sneg\n\n";
        }

        [TestMethod]
        public void Analyse_Index_SomeWrong_Successful()
        {
            _mp3Files = new List<IMp3File>
            {
                new FakeMp3File(new Mp3Tags{Title = "title1"}, SourceFolder + "01. title1.mp3", _checker),
                new FakeMp3File(new Mp3Tags{Title = "title2"}, SourceFolder + "2. title2.mp3", _checker),
                new FakeMp3File(new Mp3Tags{Title = "title3"}, SourceFolder + "03. title3.mp3", _checker),
                new FakeMp3File(new Mp3Tags{Title = "title4"}, SourceFolder + "14. title4.mp3", _checker),
                new FakeMp3File(new Mp3Tags{Title = "title5"}, SourceFolder + "5. title5.mp3", _checker),
                new FakeMp3File(new Mp3Tags{Title = "title6"}, SourceFolder + "6. title6.mp3", _checker),
                new FakeMp3File(new Mp3Tags{Title = "title7"}, SourceFolder + "07. title7.mp3", _checker),
                new FakeMp3File(new Mp3Tags{Title = "title8"}, SourceFolder + "08. title8.mp3", _checker),
                new FakeMp3File(new Mp3Tags{Title = "title9"}, SourceFolder + "009. title9.mp3", _checker),
                new FakeMp3File(new Mp3Tags{Title = "title10"}, SourceFolder + "10. title10.mp3", _checker),
            };

            _mask = "{index}. {title}";
            _expectedMessage = "Index out of range. Maximum is 10:\n" +
                         "\t" + "14. title4" + "\n\n" +
                         "Wrong  index, expected 09:\n" +
                          "\t" + "009. title9" + "\n\n" +
                          "Wrong  index, expected 02:\n" +
                          "\t" + "2. title2" + "\n\n" +
                          "Wrong  index, expected 05:\n" +
                          "\t" + "5. title5" + "\n\n" +
                          "Wrong  index, expected 06:\n" +
                          "\t" + "6. title6" + "\n\n" +
                          "Some indexes are missing: " + "02, 04 - 06, 09.\n\n";
        }

        [TestMethod]
        public void Analyse_Index_AllRight_Successful()
        {
            _mp3Files = new List<IMp3File>
            {
                new FakeMp3File(new Mp3Tags{Title = "title1"}, SourceFolder + "01. title1.mp3", _checker),
                new FakeMp3File(new Mp3Tags{Title = "title2"}, SourceFolder + "02. title2.mp3", _checker),
                new FakeMp3File(new Mp3Tags{Title = "title3"}, SourceFolder + "03. title3.mp3", _checker),
                new FakeMp3File(new Mp3Tags{Title = "title4"}, SourceFolder + "04. title4.mp3", _checker),
                new FakeMp3File(new Mp3Tags{Title = "title5"}, SourceFolder + "05. title5.mp3", _checker),
                new FakeMp3File(new Mp3Tags{Title = "title6"}, SourceFolder + "06. title6.mp3", _checker),
                new FakeMp3File(new Mp3Tags{Title = "title7"}, SourceFolder + "07. title7.mp3", _checker),
                new FakeMp3File(new Mp3Tags{Title = "title8"}, SourceFolder + "08. title8.mp3", _checker),
                new FakeMp3File(new Mp3Tags{Title = "title9"}, SourceFolder + "09. title9.mp3", _checker),
                new FakeMp3File(new Mp3Tags{Title = "title10"}, SourceFolder + "10. title10.mp3", _checker),
            };

            _mask = "{index}. {title}";
            _expectedMessage = "";
        }

        [TestMethod]
        public void Analyse_Index_Complex_Successful()
        {
            _mp3Files = new List<IMp3File>
            {
                new FakeMp3File(new Mp3Tags{Title = "title1"}, SourceFolder + "4. anothertitle.mp3", _checker),
                new FakeMp3File(new Mp3Tags{Title = "title2"}, SourceFolder + "2. title2.mp3", _checker),
                new FakeMp3File(new Mp3Tags{Title = "title3"}, SourceFolder + "qw. title3.mp3", _checker),
            };

            _mask = "{index}. {title}";
            _expectedMessage = "File: " + _mp3Files[0].FullName + "\n" +
                         "{title} in file name: anothertitle; {title} in tags: title1\n\n" +
                         "Index out of range. Maximum is 3:\n" +
                         "\t" + "4. anothertitle" + "\n\n" +
                         "Index must be number\n" +
                         "\t" + "qw. title3" + "\n\n" +
                         "Some indexes are missing: " + "1, 3.\n\n";
        }
    }
    /*[TestClass]
    public class AnalyseCommandTests
    {
        private BaseUniquePathCreator _checker = new FakeUniquePathCreator();

        [TestMethod]
        public void Analyse_Common_Successful()
        {
            var sourceFolder = @"D:\music\";            

            var mp3Files = new List<IMp3File>
            {
                new FakeMp3File(new Mp3Tags
                {
                    Album = "TestAlbum1", Artist = "TestArtist1", Genre = "TestGenre1", Title = "TestTitle1", Track = 1
                }, sourceFolder + "TestArtist1 - TestTitle2.mp3", _checker),

                new FakeMp3File(new Mp3Tags
                {
                    Album = "TestAlbum2", Artist = "TestArtist2", Genre = "TestGenre2", Title = "TestTitle2", Track = 2
                }, sourceFolder + "TestArtist2 - TestTitle2.mp3", _checker),
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
                }, sourceFolder + "Alla - Arlekino.mp3", _checker)
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
                }, sourceFolder + "2. Alla - Sneg.mp3", _checker),

                new FakeMp3File(new Mp3Tags
                {
                    Artist = "Filipp", Title = "Sneg", Track = 2
                }, sourceFolder + "2. Alla - Arlekino.mp3", _checker)
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

        [TestMethod]
        public void Analyse_Index_SomeWrong_Successful()
        {
            // Init
            const string sourceFolder = @"D:\music\";
            var mp3Files = new List<IMp3File>
            {
                new FakeMp3File(new Mp3Tags{Title = "title1"}, sourceFolder + "01. title1.mp3", _checker),
                new FakeMp3File(new Mp3Tags{Title = "title2"}, sourceFolder + "2. title2.mp3", _checker),
                new FakeMp3File(new Mp3Tags{Title = "title3"}, sourceFolder + "03. title3.mp3", _checker),
                new FakeMp3File(new Mp3Tags{Title = "title4"}, sourceFolder + "14. title4.mp3", _checker),
                new FakeMp3File(new Mp3Tags{Title = "title5"}, sourceFolder + "5. title5.mp3", _checker),
                new FakeMp3File(new Mp3Tags{Title = "title6"}, sourceFolder + "6. title6.mp3", _checker),
                new FakeMp3File(new Mp3Tags{Title = "title7"}, sourceFolder + "07. title7.mp3", _checker),
                new FakeMp3File(new Mp3Tags{Title = "title8"}, sourceFolder + "08. title8.mp3", _checker),
                new FakeMp3File(new Mp3Tags{Title = "title9"}, sourceFolder + "009. title9.mp3", _checker),
                new FakeMp3File(new Mp3Tags{Title = "title10"}, sourceFolder + "10. title10.mp3", _checker),
            };
            var fakeSystemSource = new FakeSystemSource(sourceFolder, mp3Files);
            var fakeWriter = new FakeWriter();

            var analyser = new AnalyseCommand(fakeSystemSource, @"{index}. {title}", fakeWriter);

            // Act
            analyser.Execute();

            // Assert
            var message = "Index out of range. Maximum is 10:\n" +
                         "\t" + "14. title4" + "\n\n" +
                         "Wrong  index, expected 09:\n" +
                          "\t" + "009. title9" + "\n\n" +
                          "Wrong  index, expected 02:\n" + 
                          "\t" + "2. title2" + "\n\n" +
                          "Wrong  index, expected 05:\n" +
                          "\t" + "5. title5" + "\n\n" +
                          "Wrong  index, expected 06:\n" +
                          "\t" + "6. title6" + "\n\n" +
                          "Some indexes are missing: " + "02, 04 - 06, 09.\n\n";

            Assert.AreEqual(message, fakeWriter.Stream.ToString());
        }

        [TestMethod]
        public void Analyse_Index_AllRight_Successful()
        {
            // Init
            const string sourceFolder = @"D:\music\";
            var mp3Files = new List<IMp3File>
            {
                new FakeMp3File(new Mp3Tags{Title = "title1"}, sourceFolder + "01. title1.mp3", _checker),
                new FakeMp3File(new Mp3Tags{Title = "title2"}, sourceFolder + "02. title2.mp3", _checker),
                new FakeMp3File(new Mp3Tags{Title = "title3"}, sourceFolder + "03. title3.mp3", _checker),
                new FakeMp3File(new Mp3Tags{Title = "title4"}, sourceFolder + "04. title4.mp3", _checker),
                new FakeMp3File(new Mp3Tags{Title = "title5"}, sourceFolder + "05. title5.mp3", _checker),
                new FakeMp3File(new Mp3Tags{Title = "title6"}, sourceFolder + "06. title6.mp3", _checker),
                new FakeMp3File(new Mp3Tags{Title = "title7"}, sourceFolder + "07. title7.mp3", _checker),
                new FakeMp3File(new Mp3Tags{Title = "title8"}, sourceFolder + "08. title8.mp3", _checker),
                new FakeMp3File(new Mp3Tags{Title = "title9"}, sourceFolder + "09. title9.mp3", _checker),
                new FakeMp3File(new Mp3Tags{Title = "title10"}, sourceFolder + "10. title10.mp3", _checker),
            };
            var fakeSystemSource = new FakeSystemSource(sourceFolder, mp3Files);
            var fakeWriter = new FakeWriter();

            var analyser = new AnalyseCommand(fakeSystemSource, @"{index}. {title}", fakeWriter);

            // Act
            analyser.Execute();

            // Assert
            var message = "";

            Assert.AreEqual(message, fakeWriter.Stream.ToString());
        }

        [TestMethod]
        public void Analyse_Index_Complex_Successful()
        {
            // Init
            const string sourceFolder = @"D:\music\";
            var mp3Files = new List<IMp3File>
            {
                new FakeMp3File(new Mp3Tags{Title = "title1"}, sourceFolder + "4. anothertitle.mp3", _checker),
                new FakeMp3File(new Mp3Tags{Title = "title2"}, sourceFolder + "2. title2.mp3", _checker),
                new FakeMp3File(new Mp3Tags{Title = "title3"}, sourceFolder + "qw. title3.mp3", _checker),
            };
            var fakeSystemSource = new FakeSystemSource(sourceFolder, mp3Files);
            var fakeWriter = new FakeWriter();

            var analyser = new AnalyseCommand(fakeSystemSource, @"{index}. {title}", fakeWriter);

            // Act
            analyser.Execute();

            // Assert
            var message = "File: " + mp3Files[0].FullName + "\n" +                         
                         "{title} in file name: anothertitle; {title} in tags: title1\n\n" +
                         "Index out of range. Maximum is 3:\n" +
                         "\t" + "4. anothertitle" + "\n\n" +
                         "Index must be number\n" +
                         "\t" + "qw. title3" + "\n\n" +                         
                         "Some indexes are missing: " + "1, 3.\n\n";

            Assert.AreEqual(message, fakeWriter.Stream.ToString());
        }
    }*/
}
