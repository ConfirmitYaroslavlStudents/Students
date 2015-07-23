using System;
using System.IO;
using Mp3Lib;
using NUnit.Framework;

namespace Mp3LibTests
{
    [TestFixture]
    public class Mp3LibActionsTests
    {
        string path = @"D:\Test.mp3";       
        Mp3File audiofile = new Mp3File(@"D:\Test.mp3");

        [SetUp]
        public void CreateTestMp3()
        {              
            audiofile.Tag.Album = "testAlbum";
            audiofile.Tag.Title = "testTitle";
            audiofile.Tag.Track = 1;
            audiofile.Tag.Genres = new[]{ "Test genre" };
            audiofile.Tag.Performers = new[] { "Test performer" };
            audiofile.Save();
        }  

        [Test]
        public void ExecuteRenameTest()
        {
            // arrange
            var testArgs = new[] { "rename", path, "{artist} - {title}" };
            var sut = new Application();
            var newpath = @"D:\Test performer - testTitle";
            
            // act
            sut.Execute(testArgs);
            
            FileInfo newfile = new FileInfo(@"D:\Test performer - testTitle.mp3");
            
            // assert
            
            Assert.That(newfile.Exists, Is.True);
            newfile.MoveTo(path);
        }    

        [Test]
        public void ExecuteChangeTagTest_ChangeSuccsesful()
        {
            // arrange
            var sut = new Application();
            string tagToChange = "{album}";
            string newTagValue = "Pop";
            string expected = newTagValue;
            

            // act
            sut.Execute(new []{"changeTag", path, tagToChange, newTagValue} );
            var mp3file = new Mp3File(path);
            // assert
            Assert.AreEqual(expected, mp3file.Tag.Album);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ChangeTagTest_CannotChangeTag_ThrowException()
        {
            // arrange
            var sut = new Application();
            string tagToChange = "{genr}";
            string newTagValue = "Some genre";
            
            // act
            sut.Execute(new[] { "changeTag", path, tagToChange, newTagValue }); 
        }
    }
}
