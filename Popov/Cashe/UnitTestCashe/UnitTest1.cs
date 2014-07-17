
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cashe;

namespace UnitTestCashe
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestEqualsKeys()
        {
            var temp = new Cashe<int, string>();
            for (var i = 0; i < 20; ++i)
            {
                temp.Add(i, i+" letter");
            }
            Assert.AreEqual(temp[2], 2 + " letter");            
        }

        [TestMethod]
        public void TestCount()
        {
            var temp = new Cashe<int, string>();
            for (var i = 0; i < 20; ++i)
            {
                temp.Add(i, i + " letter");
            }
            Assert.AreEqual(temp.Count, 20);
        }

        [TestMethod]
        public void TestRemoveElement()
        {
            var temp = new Cashe<int, string>();
            for (var i = 0; i < 20; ++i)
            {
                temp.Add(i, i + " letter");
            }
            temp.Remove(0);
            Assert.AreEqual(temp.ContainsKey(0), false);
        }

        [TestMethod]
        public void TestRemoveElementAndEqualsCount()
        {
            const int length = 20;
            var temp = new Cashe<int, string>();
            for (var i = 0; i < length; ++i)
            {
                temp.Add(i, i + " letter");
            }
            temp.Remove(0);
            Assert.AreEqual(temp.Count, length-1);
        }

        [TestMethod]
        public void TestChangeCapacity()
        {
            const int length = 20;
            var temp = new Cashe<int, string>();
            for (var i = 0; i < length; ++i)
            {
                temp.Add(i, i + " letter");
            }
            temp.Capacity = 5;
            Assert.AreEqual(temp.Capacity, 5);
        }

        [TestMethod]
        public void TestClear()
        {
            var temp = new Cashe<int, string>();
            for (var i = 0; i < 20; ++i)
            {
                temp.Add(i, i + " letter");
            }
            temp.ClearOrInitial();
            Assert.AreEqual(temp.Count, 0);
        }



    }
}
