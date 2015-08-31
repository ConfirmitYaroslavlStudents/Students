using FileLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using CommandCreation;
using System;
using System.Collections.Generic;
using System.Linq;
using Tests.Fakes;

namespace Tests
{
    [TestClass]
    public class ChangeTagsCommandTest
    {
        private const string SourceFolder = @"D:\music\";

        [TestMethod]
        public void Change1Tags_Successful()
        {
            // Init
            var mp3Files = new List<IMp3File>
            {
                new FakeMp3File(new Mp3Tags{Title = "title1"}, SourceFolder + "1. newtitle1.mp3"),
                new FakeMp3File(new Mp3Tags{Title = "title2"}, SourceFolder + "2. newtitle2.mp3"),
                new FakeMp3File(new Mp3Tags{Title = "title3"}, SourceFolder + "3. newtitle3.mp3"),
            };

            // Act
            var commandPool = mp3Files.Select(mp3File => new ChangeTagsCommand(mp3File, "{track}. {title}")).Cast<Command>().ToList();
            commandPool.ForEach(command => command.Execute());

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
                new FakeMp3File(new Mp3Tags{Artist = "artist1"}, SourceFolder + "newartist1.mp3"),
                new FakeMp3File(new Mp3Tags{Artist = "artist2"}, SourceFolder + "newartist2.mp3"),
                new FakeMp3File(new Mp3Tags{Artist = "artist3"}, SourceFolder + "newartist3.mp3"),
            };

            // Act
            var commandPool = mp3Files.Select(mp3File => new ChangeTagsCommand(mp3File, "{artist}")).Cast<Command>().ToList();
            commandPool.ForEach(command => command.Execute());

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
                new FakeMp3File(new Mp3Tags(), SourceFolder + "1.newartist1 - newtitle1.mp3"),
                new FakeMp3File(new Mp3Tags(), SourceFolder + "2.newartist2 - newtitle2.mp3"),
                new FakeMp3File(new Mp3Tags(), SourceFolder + "3.newartist3 - newtitle3.mp3"),
            };

            // Act
            var commandPool = mp3Files.Select(mp3File => new ChangeTagsCommand(mp3File, "{track}.{artist} - {title}")).Cast<Command>().ToList();
            commandPool.ForEach(command => command.Execute());

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
                new FakeMp3File(new Mp3Tags(), SourceFolder + "1.newartist.mp3"),
            };

            // Act
            var commandPool = mp3Files.Select(mp3File => new ChangeTagsCommand(mp3File, "{track}.{artist} - {title}")).Cast<Command>().ToList();
            commandPool.ForEach(command => command.Execute());
        }

        [TestMethod]
        public void ChangeTags_SeparatorInName_Successful()
        {
            // Init
            var mp3Files = new List<IMp3File>
            {
                new FakeMp3File(new Mp3Tags(), SourceFolder + "new-artist - newtitle.mp3"),
            };

            // Act
            var commandPool = mp3Files.Select(mp3File => new ChangeTagsCommand(mp3File, "{artist} - {title}")).Cast<Command>().ToList();
            commandPool.ForEach(command => command.Execute());

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
                new FakeMp3File(new Mp3Tags(), SourceFolder + "{ssnewartist}-newtitle..mp3"),
            };

            // Act
            var commandPool = mp3Files.Select(mp3File => new ChangeTagsCommand(mp3File, "{ss{artist}}-{title}.")).Cast<Command>().ToList();
            commandPool.ForEach(command => command.Execute());

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
                new FakeMp3File(new Mp3Tags(), SourceFolder + "newartist.newtitle.newgenre.mp3"),
            };

            // Act
            var commandPool = mp3Files.Select(mp3File => new ChangeTagsCommand(mp3File, "{artist}.{title}.{genre}")).Cast<Command>().ToList();
            commandPool.ForEach(command => command.Execute());

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
                new FakeMp3File(new Mp3Tags{Artist = "artist", Title = "title"}, SourceFolder + "newartist-newtitle.mp3"),
            };

            // Act
            var commandPool = mp3Files.Select(mp3File => new ChangeTagsCommand(mp3File, "")).Cast<Command>().ToList();
            commandPool.ForEach(command => command.Execute());

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
                new FakeMp3File(new Mp3Tags{Artist = "artist", Title = "title"}, SourceFolder + ".newartist.newtitle.mp3"),
            };

            // Act
            var commandPool = mp3Files.Select(mp3File => new ChangeTagsCommand(mp3File, "{artist}.{title}")).Cast<Command>().ToList();
            commandPool.ForEach(command => command.Execute());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void ChangeTags_WrongMask_DifferentOrderOfSplits2_InvalidDataException()
        {
            // Init
            var mp3Files = new List<IMp3File>
            {
                new FakeMp3File(new Mp3Tags{Title = "title"}, SourceFolder + "_def_newtitle_abc_.mp3"),
            };

            // Act
            var commandPool = mp3Files.Select(mp3File => new ChangeTagsCommand(mp3File, "_abc_{artist}_def_")).Cast<Command>().ToList();
            commandPool.ForEach(command => command.Execute());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void ChangeTags_WrongMask_InBegin_InvalidDataException()
        {
            // Init
            var mp3Files = new List<IMp3File>
            {
                new FakeMp3File(new Mp3Tags{Artist = "artist", Title = "title"}, SourceFolder + "newartist-newtitle.mp3"),
            };

            // Act
            var commandPool = mp3Files.Select(mp3File => new ChangeTagsCommand(mp3File, "..{artist}-{title}")).Cast<Command>().ToList();
            commandPool.ForEach(command => command.Execute());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void ChangeTags_WrongMask_NearlySplit_InvalidDataException()
        {
            // Init
            var mp3Files = new List<IMp3File>
            {
                new FakeMp3File(new Mp3Tags{Artist = "artist", Title = "title"}, SourceFolder + "newartist-newtitle.mp3"),
            };

            // Act
            var commandPool = mp3Files.Select(mp3File => new ChangeTagsCommand(mp3File, "{artist}-..{title}")).Cast<Command>().ToList();
            commandPool.ForEach(command => command.Execute());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void ChangeTags_WrongMask_DifferentSplits_InvalidDataException()
        {
            // Init
            var mp3Files = new List<IMp3File>
            {
                new FakeMp3File(new Mp3Tags{Artist = "artist", Title = "title"}, SourceFolder + "newartist-newtitle.mp3"),
            };

            // Act
            var commandPool = mp3Files.Select(mp3File => new ChangeTagsCommand(mp3File, "{artist}.{title}")).Cast<Command>().ToList();
            commandPool.ForEach(command => command.Execute());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void ChangeTags_SplitsInFilenameMoreThanInMask_InvalidDataException()
        {
            // Init
            var mp3Files = new List<IMp3File>
            {
                new FakeMp3File(new Mp3Tags{Artist = "artist", Title = "title"}, SourceFolder + "-newartist-newtitle.mp3"),
            };

            // Act
            var commandPool = mp3Files.Select(mp3File => new ChangeTagsCommand(mp3File, "{artist}-{title}")).Cast<Command>().ToList();
            commandPool.ForEach(command => command.Execute());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void ChangeTags_AmbiguousMatchingOfMaskAndFilename_InvalidDataException()
        {
            // Init
            var mp3Files = new List<IMp3File>
            {
                new FakeMp3File(new Mp3Tags{Artist = "artist", Title = "title"}, SourceFolder + "newartist-newtitle.mp3"),
            };

            // Act
            var commandPool = mp3Files.Select(mp3File => new ChangeTagsCommand(mp3File, "{artist}{title}")).Cast<Command>().ToList();
            commandPool.ForEach(command => command.Execute());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ChangeTags_TagDoesNotExist_ArgumentException()
        {
            // Init
            var mp3Files = new List<IMp3File>
            {
                new FakeMp3File(new Mp3Tags{Artist = "artist", Title = "title"}, SourceFolder + "newartist-newtitle.mp3"),
            };

            // Act
            var commandPool = mp3Files.Select(mp3File => new ChangeTagsCommand(mp3File, "{sometag}-{title}")).Cast<Command>().ToList();
            commandPool.ForEach(command => command.Execute());
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ChangeTag_TagTrackIsNotInt_FormatException()
        {
            // Init
            var mp3Files = new List<IMp3File>
            {
                new FakeMp3File(new Mp3Tags{Artist = "artist", Title = "title"}, SourceFolder + "one. newartist-newtitle.mp3"),
            };

            // Act
            var commandPool = mp3Files.Select(mp3File => new ChangeTagsCommand(mp3File, "{track}. {artist}-{title}")).Cast<Command>().ToList();
            commandPool.ForEach(command => command.Execute());
        }
    }
}
