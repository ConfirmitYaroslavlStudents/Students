/*
  using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileLib;

namespace Tests
{
    [TestClass]
    public class Mp3ManipulationsTests_Rename
    {
        [TestMethod]
        public void Rename_CommonPattern_SuccessfulRename()
        {
            // Init
            var fakeMp3File = new FakeMp3File(@"D:\music\audio.mp3", new Mp3Tags
            {
                Artist = "Alla",
                Title = "Arlekino"
            });
            var mp3Manipulations = new Mp3Manipulations(fakeMp3File);


            // Act
            mp3Manipulations.Rename("{artist}-{title}");


            // Assert
            Assert.AreEqual(fakeMp3File.Path, @"D:\music\Alla-Arlekino.mp3");
        }

        [TestMethod]
        public void Rename_ComplexPattern_SuccessfulRename()
        {
            // Init
            var fakeMp3File = new FakeMp3File(@"D:\music\audio.mp3", new Mp3Tags
            {
                Artist = "Alla",
                Title = "Arlekino"
            });
            var mp3Manipulations = new Mp3Manipulations(fakeMp3File);

            // Act
            mp3Manipulations.Rename(@"{asd{artist}}-..{title}");

            // Assert
            Assert.AreEqual(fakeMp3File.Path, @"D:\music\{asdAlla}-..Arlekino.mp3");
        }

        [TestMethod] 
        public void Rename_FileWithSuchNameAlreadyExists_UniquePathCreated()
        {
            // Init
            var fakeMp3File = new FakeMp3File(@"D:\music\audio.mp3", new Mp3Tags
            {
                Artist = "Alla",
                Title = "Arlekino"
            });
            var mp3Manipulations = new Mp3Manipulations(fakeMp3File);

            // Act
            mp3Manipulations.Rename("{artist}");

            // Assert
            Assert.AreEqual(fakeMp3File.Path, @"D:\music\Alla (2).mp3");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Rename_EmptyPattern_ArgumentException()
        {
            // Init
            var fakeMp3File = new FakeMp3File(@"D:\music\audio.mp3", new Mp3Tags
            {
                Artist = "Alla",
                Title = "Arlekino"
            });
            var mp3Manipulations = new Mp3Manipulations(fakeMp3File);

            // Act
            mp3Manipulations.Rename(@"");
        }
    }
}
*/
