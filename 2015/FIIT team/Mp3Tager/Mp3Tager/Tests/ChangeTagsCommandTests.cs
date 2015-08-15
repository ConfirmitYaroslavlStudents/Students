
using FileLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using CommandCreation;
using System;

namespace Tests
{
    [TestClass]
    public class ChangeTagsCommandTest
    {
        private ChangeTagsCommand _command;
        private FakeMp3File _file;

        [TestInitialize]
        public void SetMp3File()
        {
            _file = new FakeMp3File(new Mp3Tags(), @"D:\TestFile.mp3");
            _file.Tags.Album = "TestAlbum";
            _file.Tags.Artist = "TestPerformer";
            _file.Tags.Genre = "TestGenre";
            _file.Tags.Title = "TestTitle";
            _file.Tags.Track = 1;
        }

        [TestMethod]
        public void Change1Tags_Successful()
        {
            _command = new ChangeTagsCommand(_file, @"{artist}");
            _file.FullName = @"D:\music\Alla.mp3";

            _command.Execute();

            Assert.AreEqual("Alla", _file.Tags.Artist);
        }

        [TestMethod]
        public void Change2Tags_Successful()
        {
            _command = new ChangeTagsCommand(_file, "{artist} - {title}");
            _file.FullName = @"D:\Art - ist.mp3";

            _command.Execute();

            Assert.AreEqual("Art", _file.Tags.Artist);
            Assert.AreEqual("ist", _file.Tags.Title);
        }

        [TestMethod]
        public void Change3Tags_Successful()
        {
            _command = new ChangeTagsCommand(_file, "{track}.{artist} - {title}");
            _file.FullName = @"D:\1.Art - ist.mp3";
            uint expectedTrack = 1;

            _command.Execute();

            Assert.AreEqual("Art", _file.Tags.Artist);
            Assert.AreEqual("ist", _file.Tags.Title);
            Assert.AreEqual(expectedTrack, _file.Tags.Track);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void ChangeTags_WithStrangeSeparator_Successful()
        {
            _command = new ChangeTagsCommand(_file, "{track}.{artist} - {title}");
            _file.FullName = @"D:\1.Artabcist.mp3";

            _command.Execute();
        }

        [TestMethod]
        public void ChangeTags_SeparatorInName_Successful()
        {
            _command = new ChangeTagsCommand(_file, "{artist} - {title}");
            _file.FullName = @"D:\Hi-fi - song one.mp3";

            _command.Execute();

            Assert.AreEqual("Hi-fi", _file.Tags.Artist);
            Assert.AreEqual("song one", _file.Tags.Title);
        }

        [TestMethod]
        public void ChangeTags_ComplexMask_SuccessfulChange()
        {
            _command = new ChangeTagsCommand(_file, @"{ss{artist}}-{title}.");
            _file.FullName = @"D:\music\{ssAlla}-Arlekino..mp3";

            _command.Execute();

            // Assert
            Assert.AreEqual("Alla", _file.Tags.Artist);
            Assert.AreEqual("Arlekino", _file.Tags.Title);
        }

        [TestMethod]
        public void ChangeTags_SimilarSplits_SuccessfulChange()
        {
            _command = new ChangeTagsCommand(_file, @"{artist}.{title}.{genre}");
            _file.FullName = @"D:\music\Alla.Arlekino.Pop.mp3";

            _command.Execute();

            // Assert
            Assert.AreEqual("Alla", _file.Tags.Artist);
            Assert.AreEqual("Arlekino", _file.Tags.Title);
            Assert.AreEqual("Pop", _file.Tags.Genre);
        }

        [TestMethod]
        public void ChangeTags_EmptyMask_WithoutChanges()
        {
            _command = new ChangeTagsCommand(_file, @"");
            _file.FullName = @"D:\music\Alla-Arlekino.mp3";

            _command.Execute();

            // Assert
            Assert.AreEqual("TestPerformer", _file.Tags.Artist);
            Assert.AreEqual("TestTitle", _file.Tags.Title);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void ChangeTags_WrongMask_DifferentOrderOfSplits_InvalidDataException()
        {
            _command = new ChangeTagsCommand(_file, @"{artist}.{title}.");
            _file.FullName = @"D:\music\.Alla.Arlekino.mp3";

            _command.Execute();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void ChangeTags_WrongMask_DifferentOrderOfSplits2_InvalidDataException()
        {
            _command = new ChangeTagsCommand(_file, @"_abc_{artist}_def_");
            _file.FullName = @"D:\music\_def_Arlekino_abc_.mp3";

            _command.Execute();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void ChangeTags_WrongMask_InBegin_InvalidDataException()
        {
            _command = new ChangeTagsCommand(_file, @"..{artist}-{title}");
            _file.FullName = @"D:\music\Alla-Arlekino.mp3";

            _command.Execute();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void ChangeTags_WrongMask_NearlySplit_InvalidDataException()
        {
            _command = new ChangeTagsCommand(_file, @"{artist}-..{title}");
            _file.FullName = @"D:\music\Alla-Arlekino.mp3";

            _command.Execute();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void ChangeTags_WrongMask_DifferentSplits_InvalidDataException()
        {
            _command = new ChangeTagsCommand(_file, @"{artist}.{title}");
            _file.FullName = @"D:\music\Alla-Arlekino.mp3";

            _command.Execute();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void ChangeTags_SplitsInFilenameMoreThanInMask_InvalidDataException()
        {
            _command = new ChangeTagsCommand(_file, @"{artist}-{title}");
            _file.FullName = @"D:\music\-Alla-Arlekino.mp3";

            _command.Execute();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void ChangeTags_AmbiguousMatchingOfMaskAndFilename_InvalidDataException()
        {
            _command = new ChangeTagsCommand(_file, @"{artist}{title}");
            _file.FullName = @"D:\music\Alla-Arlekino.mp3";

            _command.Execute();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ChangeTags_TagDoesNotExist_ArgumentException()
        {
            _command = new ChangeTagsCommand(_file, @"{sometag}-{title}");
            _file.FullName = @"D:\music\Alla-Arlekino.mp3";

            _command.Execute();
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ChangeTag_TagTrackIsNotInt_FormatException()
        {
            _command = new ChangeTagsCommand(_file, @"{track}. {sometag}-{title}");
            _file.FullName = @"D:\music\one. Alla-Arlekino.mp3";

            _command.Execute();
        }
    }
}
