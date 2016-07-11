using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyListLib;

namespace TestProject
{
    [TestClass]
    public class MyTestClassArray
    {
        [TestMethod]
        public void TestClear()
        {
            MyListArray<int> ints = new MyListArray<int>(new int[] { 1, 2, 3, 4, 5 });
            ints.Clear();
            Assert.AreEqual(0, ints.Count);
        }

        [TestMethod]
        public void TestCount()
        {
            MyListArray<int> ints = new MyListArray<int>(new int[] { 1, 2, 3, 4, 5 });
            Assert.AreEqual(5, ints.Count);
        }

        [TestMethod]
        public void TestAdd()
        {
            MyListArray<int> ints = new MyListArray<int>(new int[] { 1, 2, 3, 4, 5 });
            ints.Add(6);
            Assert.AreEqual(6, ints[ints.Count - 1]);
        }

        [TestMethod]
        public void TestIndex()
        {
            MyListArray<int> ints = new MyListArray<int>(new int[] { 1, 2, 3, 4, 5 });
            Assert.AreEqual(5, ints[ints.Count - 1]);
        }

        [TestMethod]
        public void TestInsert()
        {
            MyListArray<int> ints = new MyListArray<int>(new int[] { 1, 2, 3, 4, 5 });
            ints.Insert(2, 8);
            Assert.AreEqual(8, ints[2]);
            Assert.AreEqual(3, ints[3]);
            Assert.AreEqual(5, ints[5]);
        }

        [TestMethod]
        public void TestContains()
        {
            MyListNodes<int> ints = new MyListNodes<int>(new int[] { 1, 2, 3, 4, 5 });
            Assert.IsTrue(ints.Contains(2));
            Assert.IsFalse(ints.Contains(9));
        }

        [TestMethod]
        public void TestIndexOf()
        {
            MyListNodes<int> ints = new MyListNodes<int>(new int[] { 1, 2, 3, 4, 5 });
            Assert.AreEqual(1, ints.IndexOf(ints[1]));
            Assert.AreEqual(-1, ints.IndexOf(9));
        }
    }
}
