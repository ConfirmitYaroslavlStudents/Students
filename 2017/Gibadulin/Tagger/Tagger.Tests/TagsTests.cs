using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TagsLib;

namespace Tagger.Tests
{
    [TestClass]
    public class TagsTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ToTag_UnCorrectName()
        {
            var path = @"D:\testsDir";

            Tags.ToTag(path);
        }
    }
}
