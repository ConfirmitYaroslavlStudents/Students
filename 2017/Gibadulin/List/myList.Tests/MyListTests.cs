using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace myList.Tests
{
    [TestClass]
    public class MyListTests
    {
        [TestMethod]
        public void List_initList_NotNull()
        {
            List<object> list;

            list = new List<object>();

            Assert.IsNotNull(list);
        }

        [TestMethod]
        public void List_count_0()
        {
            List<object> list;

            list = new List<object>();

            Assert.IsTrue(list.Count == 0);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void Enumerate_initList_null()
        {
            var list = new List<object>();

            var item = list[0];
        }

        [TestMethod]
        public void Insert_InsertOneItem()
        {
            var list = new List<object>();
            list.Insert(5, 0);
            var expected = 5;

            var actual = list[0];

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Insert_PushBackTwoItem()
        {
            var list = new List<object>();
            list.Insert(1, 0);
            list.Insert(2, 1);
            var expectedZero = 1;
            var expectedFirst = 2;

            var actualZero = list[0];
            var actualFirst = list[1];

            Assert.AreEqual(expectedZero, actualZero);
            Assert.AreEqual(expectedFirst, actualFirst);
        }

        [TestMethod]
        public void Insert_InsertTwoItem_CountEqual2()
        {
            var list = new List<object>();
            list.Insert(1,0);
            list.Insert(2,0);
            var expected = 2;

            var actual = list.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Enumerate_ChangeOneItem()
        {
            var list = new List<object>();
            list.Insert(1, 0);
            var expected = 2;

            list[0] = 2;
            var actual = list[0];

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Contains_InsertAndSearchElement_True()
        {
            var list = new List<object>();
            list.Insert(0, 0);

            var actual = list.Contains(0);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void Contains_InsertAndSearchDifferentElements_False()
        {
            var list = new List<object>();
            list.Insert(0, 0);

            var actual = list.Contains(1);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void IndexOf_WithoutEqual()
        {
            var list = new List<object>();
            list.Insert(0, 0);
            list.Insert(0, 1);
            list.Insert(0, 2);
            var expected = -1;

            var actual = list.IndexOf(3);

            Assert.IsTrue(actual == expected);
        }

        [TestMethod]
        public void IndexOf_TwotEqual()
        {
            var list = new List<object>();
            list.Insert(0, 0);
            list.Insert(1, 1);
            list.Insert(0, 2);
            var expected = 0;

            var actual = list.IndexOf(0);

            Assert.IsTrue(actual == expected);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void Insert_IndexMoreCounts()
        {
            var list = new List<object>();
            list.Insert(0, 0);

            list.Insert(3, 3);
        }

        [TestMethod]
        public void Insert_AppendFirst()
        {
            var list = new List<object>();
            list.Insert(0, 0);
            object expected = 1;

            list.Insert(1, 0);
            var actual = list[0];

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Insert_AppendBack()
        {
            var list = new List<object>();
            list.Insert(0, 0);
            var expected = 1;

            list.Insert(1, 1);
            var actual = list[1];

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Remove_RemoveFirstItem()
        {
            var list = new List<object>();
            list.Insert(0, 0);
            list.Insert(1, 1);
            var expectedItem = 1;
            var expectedCount = 1;

            list.Remove(0);
            var actualItem = list[0];
            var actualCount = list.Count;

            Assert.IsTrue(expectedCount == actualCount);
            Assert.IsTrue(Equals(expectedItem, actualItem));
        }
    }
}
