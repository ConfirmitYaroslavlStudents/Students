using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParseInputLib;

namespace Tagger.Tests
{
    [TestClass]
    public class ParseInputTests
    {
        [TestMethod]
        public void Parse_correctInput()
        {
            var input = @"D:\testsDir *.m* -n -r";
            var expected = new ParseInput.InputData();
            expected.Path = @"D:\testsDir";
            expected.Mask = "*.m*";
            expected.Modifier = "-n";
            expected.Subfolders = true;

            var actual = ParseInput.Parse(input);

            Assert.AreEqual(actual,expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Parse_uncorrectModifier()
        {
            var input = @"D:\testsDir *.m* UNCORRECT -r";

            var actual = ParseInput.Parse(input);
        }
    }
}
