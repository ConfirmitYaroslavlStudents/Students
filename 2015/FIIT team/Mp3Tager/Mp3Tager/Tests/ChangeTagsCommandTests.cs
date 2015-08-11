using CommandCreation;
using Mp3Lib;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    class ChangeTagsCommandTest
    {
        private ChangeTagsCommand _command;
        private FakeMp3File _file;

        [SetUp]
        public void SetMp3File()
        {
            _file = new FakeMp3File(@"D:\TestFile.mp3", new Mp3Tags());
            _file.Mp3Tags.Album = "TestAlbum";
            _file.Mp3Tags.Artist = "TestPerformer";
            _file.Mp3Tags.Genre = "TestGenre";
            _file.Mp3Tags.Title = "TestTitle";
            _file.Mp3Tags.Track = 1; 
        }

        [Test]
        public void Change2Tags_Successful()
        {
            _command = new ChangeTagsCommand(_file, "{artist} - {title}");
            _file.Path = @"D:\Art - ist.mp3";        

            _command.Execute();

            Assert.AreEqual("Art", _file.Mp3Tags.Artist);
            Assert.AreEqual("ist", _file.Mp3Tags.Title);
        }

         [Test]
         public void Change3Tags_Successful()
         {
             _command = new ChangeTagsCommand(_file, "{track}.{artist} - {title}");
             _file.Path = @"D:\1.Art - ist.mp3";
             
             _command.Execute();

             Assert.AreEqual("Art", _file.Mp3Tags.Artist);
             Assert.AreEqual("ist", _file.Mp3Tags.Title);
             Assert.AreEqual(1, _file.Mp3Tags.Track);
         }

         [Test]
         [ExpectedException]
         public void ChangeTags_WithStrangeSeparator_Successful()
         {
             _command = new ChangeTagsCommand(_file, "{track}.{artist} - {title}");
             _file.Path = @"D:\1.Artabcist.mp3";         

             _command.Execute();
         }

         [Test]
         public void ChangeTags_SeparatorInName_Successful()
         {
             _command = new ChangeTagsCommand(_file, "{artist} - {title}");
             _file.Path = @"D:\Hi-fi - song one.mp3";

             _command.Execute();

             Assert.AreEqual("Hi-fi", _file.Mp3Tags.Artist);
             Assert.AreEqual("song one", _file.Mp3Tags.Title);
         }
    }
}
