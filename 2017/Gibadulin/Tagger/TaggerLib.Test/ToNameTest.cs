using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TaggerLib.Test
{
    [TestClass]
    public class ToNameTest
    {
        [TestMethod]
        public void ToName_NewName()
        {
            var file = new File("path");
            file.Title = "Title";
            file.Performers = new []{"Performers One", "Performers Two"};
            var expected = "Performers One, Performers Two - Title";

            var item = new ToName();
            item.Act(file);
            var actual = file.Name;

            Assert.AreEqual(expected, actual);
        }
    }
}
