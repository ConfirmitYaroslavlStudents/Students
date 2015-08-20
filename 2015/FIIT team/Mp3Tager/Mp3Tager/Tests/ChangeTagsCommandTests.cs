using FileLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using CommandCreation;
using System;
using System.Collections.Generic;
using Tests.Fakes;

namespace Tests
{
    [TestClass]
    public class ChangeTagsCommandTest
    {
        private const string SourceFolder = @"D:\music\";
        private readonly FakeUniquePathCreator _checker = new FakeUniquePathCreator();

        [TestMethod]
        public void Change1Tags_Successful()
        {
            // Init
            var mp3Files = new List<IMp3File>
            {
                new FakeMp3File(new Mp3Tags{Title = "title1"}, SourceFolder + "1. newtitle1.mp3", _checker),
                new FakeMp3File(new Mp3Tags{Title = "title2"}, SourceFolder + "2. newtitle2.mp3", _checker),
                new FakeMp3File(new Mp3Tags{Title = "title3"}, SourceFolder + "3. newtitle3.mp3", _checker),
            };

            // Act
            var changetags = new ChangeTagsCommand(mp3Files, "{track}. {title}");
            var message = changetags.Execute();
            changetags.Complete();

            // Assert
            Assert.AreEqual("newtitle1", mp3Files[0].Tags.Title);
            Assert.AreEqual("newtitle2", mp3Files[1].Tags.Title);
            Assert.AreEqual("newtitle3", mp3Files[2].Tags.Title);

            Assert.AreEqual((uint)1, mp3Files[0].Tags.Track);
            Assert.AreEqual((uint)2, mp3Files[1].Tags.Track);
            Assert.AreEqual((uint)3, mp3Files[2].Tags.Track);
        }

        [TestMethod]
        public void Change2Tags_Successful()
        {
            // Init
            var mp3Files = new List<IMp3File>
            {
                new FakeMp3File(new Mp3Tags{Artist = "artist1"}, SourceFolder + "newartist1.mp3", _checker),
                new FakeMp3File(new Mp3Tags{Artist = "artist2"}, SourceFolder + "newartist2.mp3", _checker),
                new FakeMp3File(new Mp3Tags{Artist = "artist3"}, SourceFolder + "newartist3.mp3", _checker),
            };

            // Act
            var changetags = new ChangeTagsCommand(mp3Files, "{artist}");
            var message = changetags.Execute();
            changetags.Complete();

            // Assert
            Assert.AreEqual("newartist1", mp3Files[0].Tags.Artist);
            Assert.AreEqual("newartist2", mp3Files[1].Tags.Artist);
            Assert.AreEqual("newartist3", mp3Files[2].Tags.Artist);
        }

        [TestMethod]
        public void Change3Tags_Successful()
        {
            // Init
            var mp3Files = new List<IMp3File>
            {
                new FakeMp3File(new Mp3Tags(), SourceFolder + "1.newartist1 - newtitle1.mp3", _checker),
                new FakeMp3File(new Mp3Tags(), SourceFolder + "2.newartist2 - newtitle2.mp3", _checker),
                new FakeMp3File(new Mp3Tags(), SourceFolder + "3.newartist3 - newtitle3.mp3", _checker),
            };

            // Act
            var changetags = new ChangeTagsCommand(mp3Files, "{track}.{artist} - {title}");
            var message = changetags.Execute();
            changetags.Complete();

            // Assert
            Assert.AreEqual("newartist1", mp3Files[0].Tags.Artist);
            Assert.AreEqual("newartist2", mp3Files[1].Tags.Artist);
            Assert.AreEqual("newartist3", mp3Files[2].Tags.Artist);

            Assert.AreEqual("newtitle1", mp3Files[0].Tags.Title);
            Assert.AreEqual("newtitle2", mp3Files[1].Tags.Title);
            Assert.AreEqual("newtitle3", mp3Files[2].Tags.Title);

            Assert.AreEqual((uint)1, mp3Files[0].Tags.Track);
            Assert.AreEqual((uint)2, mp3Files[1].Tags.Track);
            Assert.AreEqual((uint)3, mp3Files[2].Tags.Track);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void ChangeTags_WithStrangeSeparator_Successful()
        {
            // Init
            var mp3Files = new List<IMp3File>
            {
                new FakeMp3File(new Mp3Tags(), SourceFolder + "1.newartist.mp3", _checker),
            };

            // Act
            var changetags = new ChangeTagsCommand(mp3Files, "{track}.{artist} - {title}");
            var message = changetags.Execute();
            changetags.Complete();
        }

        [TestMethod]
        public void ChangeTags_SeparatorInName_Successful()
        {
            // Init
            var mp3Files = new List<IMp3File>
            {
                new FakeMp3File(new Mp3Tags(), SourceFolder + "new-artist - newtitle.mp3", _checker),
            };

            // Act
            var changetags = new ChangeTagsCommand(mp3Files, "{artist} - {title}");
            var message = changetags.Execute();
            changetags.Complete();

            // Assert
            Assert.AreEqual("new-artist", mp3Files[0].Tags.Artist);
            Assert.AreEqual("newtitle", mp3Files[0].Tags.Title);
        }

        [TestMethod]
        public void ChangeTags_ComplexMask_SuccessfulChange()
        {
            // Init
            var mp3Files = new List<IMp3File>
            {
                new FakeMp3File(new Mp3Tags(), SourceFolder + "{ssnewartist}-newtitle..mp3", _checker),
            };

            // Act
            var changetags = new ChangeTagsCommand(mp3Files, "{ss{artist}}-{title}.");
            var message = changetags.Execute();
            changetags.Complete();

            // Assert
            Assert.AreEqual("newartist", mp3Files[0].Tags.Artist);
            Assert.AreEqual("newtitle", mp3Files[0].Tags.Title);
        }

        [TestMethod]
        public void ChangeTags_SimilarSplits_SuccessfulChange()
        {
            // Init
            var mp3Files = new List<IMp3File>
            {
                new FakeMp3File(new Mp3Tags(), SourceFolder + "newartist.newtitle.newgenre.mp3", _checker),
            };

            // Act
            var changetags = new ChangeTagsCommand(mp3Files, "{artist}.{title}.{genre}");
            var message = changetags.Execute();
            changetags.Complete();

            // Assert
            Assert.AreEqual("newartist", mp3Files[0].Tags.Artist);
            Assert.AreEqual("newtitle", mp3Files[0].Tags.Title);
            Assert.AreEqual("newgenre", mp3Files[0].Tags.Genre);
        }

        [TestMethod]
        public void ChangeTags_EmptyMask_WithoutChanges()
        {
            // Init
            var mp3Files = new List<IMp3File>
            {
                new FakeMp3File(new Mp3Tags{Artist = "artist", Title = "title"}, SourceFolder + "newartist-newtitle.mp3", _checker),
            };

            // Act
            var changetags = new ChangeTagsCommand(mp3Files, "");
            var message = changetags.Execute();
            changetags.Complete();

            // Assert
            Assert.AreEqual("artist", mp3Files[0].Tags.Artist);
            Assert.AreEqual("title", mp3Files[0].Tags.Title);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void ChangeTags_WrongMask_DifferentOrderOfSplits_InvalidDataException()
        {
            // Init
            var mp3Files = new List<IMp3File>
            {
                new FakeMp3File(new Mp3Tags{Artist = "artist", Title = "title"}, SourceFolder + ".newartist.newtitle.mp3", _checker),
            };

            // Act
            var changetags = new ChangeTagsCommand(mp3Files, "{artist}.{title}.");
            var message = changetags.Execute();
            changetags.Complete();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void ChangeTags_WrongMask_DifferentOrderOfSplits2_InvalidDataException()
        {
            // Init
            var mp3Files = new List<IMp3File>
            {
                new FakeMp3File(new Mp3Tags{Title = "title"}, SourceFolder + "_def_newtitle_abc_.mp3", _checker),
            };

            // Act
            var changetags = new ChangeTagsCommand(mp3Files, "_abc_{artist}_def_");
            var message = changetags.Execute();
            changetags.Complete();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void ChangeTags_WrongMask_InBegin_InvalidDataException()
        {
            // Init
            var mp3Files = new List<IMp3File>
            {
                new FakeMp3File(new Mp3Tags{Artist = "artist", Title = "title"}, SourceFolder + "newartist-newtitle.mp3", _checker),
            };

            // Act
            var changetags = new ChangeTagsCommand(mp3Files, "..{artist}-{title}");
            var message = changetags.Execute();
            changetags.Complete();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void ChangeTags_WrongMask_NearlySplit_InvalidDataException()
        {
            // Init
            var mp3Files = new List<IMp3File>
            {
                new FakeMp3File(new Mp3Tags{Artist = "artist", Title = "title"}, SourceFolder + "newartist-newtitle.mp3", _checker),
            };

            // Act
            var changetags = new ChangeTagsCommand(mp3Files, "{artist}-..{title}");
            var message = changetags.Execute();
            changetags.Complete();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void ChangeTags_WrongMask_DifferentSplits_InvalidDataException()
        {
            // Init
            var mp3Files = new List<IMp3File>
            {
                new FakeMp3File(new Mp3Tags{Artist = "artist", Title = "title"}, SourceFolder + "newartist-newtitle.mp3", _checker),
            };

            // Act
            var changetags = new ChangeTagsCommand(mp3Files, "{artist}.{title}");
            var message = changetags.Execute();
            changetags.Complete();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void ChangeTags_SplitsInFilenameMoreThanInMask_InvalidDataException()
        {
            // Init
            var mp3Files = new List<IMp3File>
            {
                new FakeMp3File(new Mp3Tags{Artist = "artist", Title = "title"}, SourceFolder + "-newartist-newtitle.mp3", _checker),
            };

            // Act
            var changetags = new ChangeTagsCommand(mp3Files, "{artist}-{title}");
            var message = changetags.Execute();
            changetags.Complete();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void ChangeTags_AmbiguousMatchingOfMaskAndFilename_InvalidDataException()
        {
            // Init
            var mp3Files = new List<IMp3File>
            {
                new FakeMp3File(new Mp3Tags{Artist = "artist", Title = "title"}, SourceFolder + "newartist-newtitle.mp3", _checker),
            };

            // Act
            var changetags = new ChangeTagsCommand(mp3Files, "{artist}{title}");
            var message = changetags.Execute();
            changetags.Complete();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ChangeTags_TagDoesNotExist_ArgumentException()
        {
            // Init
            var mp3Files = new List<IMp3File>
            {
                new FakeMp3File(new Mp3Tags{Artist = "artist", Title = "title"}, SourceFolder + "newartist-newtitle.mp3", _checker),
            };

            // Act
            var changetags = new ChangeTagsCommand(mp3Files, "{sometag}-{title}");
            var message = changetags.Execute();
            changetags.Complete();
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ChangeTag_TagTrackIsNotInt_FormatException()
        {
            // Init
            var mp3Files = new List<IMp3File>
            {
                new FakeMp3File(new Mp3Tags{Artist = "artist", Title = "title"}, SourceFolder + "one. newartist-newtitle.mp3", _checker),
            };

            // Act
            var changetags = new ChangeTagsCommand(mp3Files, "{track}. {artist}-{title}");
            var message = changetags.Execute();
            changetags.Complete();
        }
    }
}
