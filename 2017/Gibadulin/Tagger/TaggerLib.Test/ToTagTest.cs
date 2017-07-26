using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TaggerLib.Test
{
    [TestClass]
    public class ToTagTest
    {
        [TestMethod]
        public void ToTag_NewTitleAndPerformers()
        {
            var file = new File("path");
            file.Name = "Performers One, Performers Two - Title";
            var expectedTitle = "Title";
            var expectedPerformers = new[] {"Performers One", "Performers Two"};

            var item = new ToTag();
            item.Act(file);
            var actualTitle = file.Title;
            var actualPerformers = file.Performers;

            Assert.AreEqual(expectedTitle, actualTitle);
            if (expectedPerformers.Length!=actualPerformers.Length)
                Assert.Fail();
            for (int i=0;i<expectedPerformers.Length;i++)
                if (expectedPerformers[i]!=actualPerformers[i])
                    Assert.Fail();
        }
    }
}
