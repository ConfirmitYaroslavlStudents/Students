using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mp3TagLib;

namespace Mp3TagTest
{
    [TestClass]
    public class MaskItemTestClass
    {
        private MaskItem _item1;
        private MaskItem _item2;
        private MaskItem _item3;

        [TestInitialize]
        public void Init()
        {
            _item1 = new MaskItem() { Type = MaskItemType.Delimiter, Value = "item" };
            _item2 = new MaskItem() { Type = MaskItemType.Delimiter, Value = "item" };
            _item3 = new MaskItem() { Type = MaskItemType.TagName, Value = "item" };
        }

        [TestMethod]
        public void EqualsTest()
        {
            Assert.AreEqual(_item1,_item2);
        }

        [TestMethod]
        public void NotEqualsTest()
        {
            Assert.AreNotEqual(_item1, _item3);
        }

        [TestMethod]
        public void EqualsReturnFalseIfArgIsNull()
        {
           Assert.AreEqual(false,new MaskItem().Equals(null));
        }

        [TestMethod]
        public void IfEqualsReturnTrueObjectsHaveSameHash()
        {
            Assert.AreEqual(_item1.GetHashCode(), _item2.GetHashCode());
        }

        [TestMethod]
        public void IfEqualsReturnTrueObjectsHaveDifferentHash()
        {
            Assert.AreNotEqual(_item1.GetHashCode(), _item3.GetHashCode());
        }
    }
}
