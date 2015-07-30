using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mp3TagLib;

namespace Mp3TagTest
{
    [TestClass]
    public class MaskItemTestClass
    {
        [TestMethod]
        public void EqualsTest()
        {
            MaskItem item1 = new MaskItem() { Type = MaskItemType.Delimiter,Value = "item" };
            MaskItem item2 = new MaskItem() { Type = MaskItemType.Delimiter,Value = "item" };
            Assert.AreEqual(item1,item2);
        }
        [TestMethod]
        public void NotEqualsTest()
        {
            MaskItem item1 = new MaskItem() { Type = MaskItemType.Delimiter, Value = "item" };
            MaskItem item2 = new MaskItem() { Type = MaskItemType.TagName, Value = "item" };
            Assert.AreNotEqual(item1, item2);
        }
        [TestMethod]
        public void EqualsReturnFalseIfArgIsNull()
        {
           Assert.AreEqual(false,new MaskItem().Equals(null));
        }
        [TestMethod]
        public void IfEqualsReturnTrueObjectsHaveSameHash()
        {
            MaskItem item1 = new MaskItem() { Type = MaskItemType.Delimiter, Value = "item" };
            MaskItem item2 = new MaskItem() { Type = MaskItemType.Delimiter, Value = "item" };
            Assert.AreEqual(item1.GetHashCode(), item2.GetHashCode());
        }
        [TestMethod]
        public void IfEqualsReturnTrueObjectsHaveDifferentHash()
        {
            MaskItem item1 = new MaskItem() { Type = MaskItemType.Delimiter, Value = "item" };
            MaskItem item2 = new MaskItem() { Type = MaskItemType.TagName, Value = "item" };
            Assert.AreNotEqual(item1.GetHashCode(), item2.GetHashCode());
        }
    }
}
