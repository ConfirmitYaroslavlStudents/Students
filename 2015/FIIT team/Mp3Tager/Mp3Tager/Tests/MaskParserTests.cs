using CommandCreation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;

namespace Tests
{
    [TestClass]
    public class MaskParserTests
    {
        private string _expectedMask;
        private List<string> _expectedTags;
        private List<string> _expectedSplits;

        [TestCleanup]
        public void CleanUp()
        {
            var parser = new MaskParser(_expectedMask);

            Assert.AreEqual(_expectedMask, parser.GetMaskFromTagsAndSplits());
            CollectionAssert.AreEqual(_expectedTags, parser.GetTags());
            CollectionAssert.AreEqual(_expectedSplits, parser.GetSplits());

        }

        [TestMethod]
        public void MaskParser_RigthMask()
        {
            _expectedMask = "start {track}. {artist} - {title} finish";
            _expectedTags = new List<string> { "{track}", "{artist}", "{title}" };
            _expectedSplits = new List<string> { "start ", ". ", " - ", " finish" };

        }

        [TestMethod]
        public void MaskParser_RigthMask2()
        {
            _expectedMask = "{track}. {artist} - {title}";
            _expectedTags = new List<string> { "{track}", "{artist}", "{title}" };
            _expectedSplits = new List<string> { "", ". ", " - ", "" };
        }

        [TestMethod]
        public void MaskParser_RigthMask3()
        {
            _expectedMask = "start {track}{artist} - {title}";
            _expectedTags = new List<string> { "{track}", "{artist}", "{title}" };
            _expectedSplits = new List<string> { "start ", "", " - ", "" };
        }

        [TestMethod]
        public void MaskParser_RigthMask4()
        {
            _expectedMask = "{...{artist}..}";
            _expectedTags = new List<string> { "{artist}" };
            _expectedSplits = new List<string> { "{...", "..}" };
        }

        [TestMethod]
        public void MaskParser_WrongMask_ExtraLeftBraceInTheBegining()
        {
            _expectedMask = "{ttt{track}{title}";
            _expectedTags = new List<string> { "{track}", "{title}" };
            _expectedSplits = new List<string> { "{ttt", "", "" };

        }

        [TestMethod]
        public void MaskParser_WrongMask_ExtraRightBraceInTheBegining()
        {
            _expectedMask = "}ttt{track}{title}";
            _expectedTags = new List<string> { "{track}", "{title}" };
            _expectedSplits = new List<string> { "}ttt", "", "" };

        }

        [TestMethod]
        public void MaskParser_WrongMask_ExtraLeftBraceInTheEnd()
        {
            _expectedMask = "{track}{title}hh{hh";
            _expectedTags = new List<string> { "{track}", "{title}" };
            _expectedSplits = new List<string> { "", "", "hh{hh" };
        }

        [TestMethod]
        public void MaskParser_WrongMask_ExtraRightBraceInTheEnd()
        {
            _expectedMask = "{ttt{track}{title}}h";
            _expectedTags = new List<string> { "{track}", "{title}" };
            _expectedSplits = new List<string> { "{ttt", "", "}h" };
        }

        
    }

    [TestClass]
    public class MaskParserTests_IsEqualNumberOfSplitsInMaskAndFileName
    {
        [TestMethod]
        public void MaskParser_IsEqualNumberOfSplitsInMaskAndFileName_ReturnTrue()
        {
            // arrange
            var _expectedMask = "{track}.{artist}.{title}";
            var parser = new MaskParser(_expectedMask);

            // act
            var actual = parser.IsEqualNumberOfSplitsInMaskAndFileName(".", "Alla.Pop.Arlekino");

            // assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void MaskParser_IsEqualNumberOfSplitsInMaskAndFileName_ReturnFalse()
        {
            // arrange
            var _expectedMask = "{track}.{artist}{title}";
            var parser = new MaskParser(_expectedMask);

            // act
            var actual = parser.IsEqualNumberOfSplitsInMaskAndFileName(".", "Alla.Pop.Arlekino");

            // assert
            Assert.IsFalse(actual);
        }
    }

    [TestClass]
    public class MaskValidates
    {
        [TestMethod]
        public void MaskParser_DifferentSplits_Successful()
        {
            var parser = new MaskParser("{artist} - {title} . {track}");
            Assert.IsTrue(parser.ValidateFileName("TestArtist - TestTitle . 1"));
        }

        [TestMethod]
        public void MaskParser_Common_Successful()
        {
            var parser = new MaskParser("{artist} - {title}");
            Assert.IsTrue(parser.ValidateFileName("TestArtist - TestTitle"));
        }

        [TestMethod]
        public void MaskParser_SplitInBegin_Successful()
        {
            var parser = new MaskParser("a{artist}");
            Assert.IsTrue(parser.ValidateFileName("aTestArtist"));
        }
        
        [TestMethod]
        public void MaskParser_SameSplits_Successful()
        {
            var parser = new MaskParser("{artist} - {title} - {track}");
            Assert.IsTrue(parser.ValidateFileName("TestArtist - TestTitle - 1"));
        }

        [TestMethod]
        public void MaskParser_LetterInCenter_Successful()
        {
            var parser = new MaskParser("{artist}Q{track}");
            Assert.IsTrue(parser.ValidateFileName("TestArtistQ1"));
        }

        [TestMethod]
        public void MaskParser_SplitInBeginAndEnd_Successful()
        {
            var parser = new MaskParser("-{artist}.{track}-");
            Assert.IsTrue(parser.ValidateFileName("-TestArtist.1-"));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void MaskParser_ManySplits_Exception()
        {
            var parser = new MaskParser("{artist}-{track}");
            parser.ValidateFileName("Test-Artist-1");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void MaskParser_WithoutSplits_Exception()
        {
            var parser = new MaskParser("{artist}{track}");
            parser.ValidateFileName("TestArtist1");
        }
    }
}
