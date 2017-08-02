using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TaggerLib.Test
{
    [TestClass]
    public class ToNameTest
    {
        [TestMethod]
        public void Change_NewName()
        {
            var file = new File("path");
            file.Title = "Title";
            file.Performers = new []{"Performers One", "Performers Two"};
            var expected = "Performers One, Performers Two - Title";

            var item = new ToName {FileForChange = file};
            item.Change();
            var actual = file.Name;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Change_NullFileToChange_Exception()
        {
            var item = new ToName();
            item.Change();
        }
    }
}
