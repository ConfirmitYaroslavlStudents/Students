using System;
using System.Collections.Generic;
using System.Linq;
using mp3lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mp3lib_Tests
{
    [TestClass]
    public class Mp3FileAnalyserTests
    {
        [TestMethod]
        public void Test_Analyser()
        {
            var firstItem   = new TestMp3File("1. name1 - title1.mp3") { TrackId = "1", Title = "title1", Artist = "name1" };
            var secondItem  = new TestMp3File("2. name2 - title2.mp3") { TrackId = "1", Title = "title2", Artist = "name1" };
            var thirdItem   = new TestMp3File("3. name3 - title3.mp3") { TrackId = "1", Title = "title3", Artist = "name3" };

            var files = new TestMp3File[]
            {
                firstItem,
                secondItem,
                thirdItem,
            };

            var analyser = new Mp3FileAnalyser(files, "{id}. {artist} - {title}");
            var diffs = analyser.FindDifferences().ToList();

            var expectedDiff1 = new FileDifferences(secondItem);
            expectedDiff1.Add(TagType.Id, new Diff { FileNameValue = "2", TagValue = "1" });
            expectedDiff1.Add(TagType.Artist, new Diff { FileNameValue = "name2", TagValue = "name1" });

            var expectedDiff2 = new FileDifferences(thirdItem);
            expectedDiff2.Add(TagType.Id, new Diff { FileNameValue = "3", TagValue = "1" });

            var expectedDiffs = new List<FileDifferences>()
            {
                expectedDiff1,
                expectedDiff2,
            };

            Assert.AreEqual(expectedDiffs.Count, diffs.Count);

            for (var i = 0; i < diffs.Count; i++)
            {
                Assert.AreEqual(expectedDiffs[i].Mp3File, diffs[i].Mp3File);

                Assert.AreEqual(expectedDiffs[i].Diffs.Count, diffs[i].Diffs.Count);

                foreach (var diff in expectedDiffs[i].Diffs)
                {
                    Assert.AreEqual(expectedDiffs[i].Diffs[diff.Key].FileNameValue, diffs[i].Diffs[diff.Key].FileNameValue);
                    Assert.AreEqual(expectedDiffs[i].Diffs[diff.Key].TagValue, diffs[i].Diffs[diff.Key].TagValue);
                }
            }
        }
    }
}
