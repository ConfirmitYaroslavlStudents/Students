using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mp3TagLib;

namespace Mp3TagTest
{
    [TestClass]
    public class MaskItemTestClass
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EqualsTest()
        {
            MaskItem item1 = new MaskItem() { Type = MaskItemType.Delimiter,Value = "item" };
            MaskItem item2 = new MaskItem() { Type = MaskItemType.Delimiter,Value = "item" };
            MaskItem item3 = new MaskItem() { Type = MaskItemType.TagName, Value = "item" };
            Assert.AreEqual(item1,item2);
            Assert.AreNotEqual(item1,item3);
            item1.Equals(null);
        }
        [TestMethod]
        public void HashTest()
        {
            MaskItem item1 = new MaskItem() { Type = MaskItemType.Delimiter, Value = "item" };
            MaskItem item2 = new MaskItem() { Type = MaskItemType.Delimiter, Value = "item" };
            MaskItem item3 = new MaskItem() { Type = MaskItemType.TagName, Value = "item" };
            Assert.AreEqual(item1.GetHashCode(), item2.GetHashCode());
            Assert.AreNotEqual(item1.GetHashCode(), item3.GetHashCode());
        }
    }
}
