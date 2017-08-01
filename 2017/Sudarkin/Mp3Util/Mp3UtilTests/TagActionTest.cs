using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mp3UtilLib;
using Mp3UtilLib.Actions;
using Mp3UtilTests.Helpers;

namespace Mp3UtilTests
{
    [TestClass]
    public class TagActionTest
    {
        [TestMethod]
        public void ExtractTagsFromName()
        {
            string[] files =
            {
                @"Bullet For My Valentine - Cries in Vain.mp3",
                @"Ciao Adios - Anne-Mari.mp3"
            };

            List<string[]> expectedValues = new List<string[]>
            {
                new[] {"Bullet For My Valentine", "Cries in Vain"},
                new[] {"Ciao Adios", "Anne-Mari"}
            };

            IActionStrategy tagAction = new TagAction();

            int i = 0;
            foreach (string file in files)
            {
                TestableMp3File mp3File = new TestableMp3File(file);

                tagAction.Process(mp3File);
                
                Assert.AreEqual(expectedValues[i][0], mp3File.Artist);
                Assert.AreEqual(expectedValues[i][1], mp3File.Title);
                Assert.AreEqual(true, mp3File.Saved);
                i++;
            }
        }
    }
}