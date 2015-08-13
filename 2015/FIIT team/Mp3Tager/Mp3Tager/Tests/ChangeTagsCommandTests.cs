using CommandCreation;
using FileLib;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class ChangeTagsCommandTest
    {
        private ChangeTagsCommand _command;
        private FakeMp3File _file;

        [SetUp]
        public void SetMp3File()
        {
            _file = new FakeMp3File( new Mp3Tags(), @"D:\TestFile.mp3");
            _file.Tags.Album = "TestAlbum";
            _file.Tags.Artist = "TestPerformer";
            _file.Tags.Genre = "TestGenre";
            _file.Tags.Title = "TestTitle";
            _file.Tags.Track = 1; 
        }

        [Test]
        public void Change2Tags_Successful()
        {
            _command = new ChangeTagsCommand(_file, "{artist} - {title}");
            _file.FullName = @"D:\Art - ist.mp3";        

            _command.Execute();

            Assert.AreEqual("Art", _file.Tags.Artist);
            Assert.AreEqual("ist", _file.Tags.Title);
        }

         [Test]
         public void Change3Tags_Successful()
         {
             _command = new ChangeTagsCommand(_file, "{track}.{artist} - {title}");
             _file.FullName = @"D:\1.Art - ist.mp3";
             
             _command.Execute();

             Assert.AreEqual("Art", _file.Tags.Artist);
             Assert.AreEqual("ist", _file.Tags.Title);
             Assert.AreEqual(1, _file.Tags.Track);
         }

         [Test]
         [ExpectedException]
         public void ChangeTags_WithStrangeSeparator_Successful()
         {
             _command = new ChangeTagsCommand(_file, "{track}.{artist} - {title}");
             _file.FullName = @"D:\1.Artabcist.mp3";         

             _command.Execute();
         }

         [Test]
         public void ChangeTags_SeparatorInName_Successful()
         {
             _command = new ChangeTagsCommand(_file, "{artist} - {title}");
             _file.FullName = @"D:\Hi-fi - song one.mp3";

             _command.Execute();

             Assert.AreEqual("Hi-fi", _file.Tags.Artist);
             Assert.AreEqual("song one", _file.Tags.Title);
         }
    }
}
