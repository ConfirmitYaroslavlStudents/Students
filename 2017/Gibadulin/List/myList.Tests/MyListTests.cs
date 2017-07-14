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
            //arrange
            List<object> list;
            //act
            list = new List<object>();
            //assert
            Assert.IsNotNull(list);
        }

        [TestMethod]
        public void List_count_0()
        {
            //arrange
            List<object> list;
            //act
            list = new List<object>();
            //assert
            Assert.IsTrue(list.Count==0);
        }

        [TestMethod]
        public void Enumerate_initList_null() // test for exception of []
        {
            //arrange
            List<object> list = new List<object>();
            //act
            object a = list[0];
            //assert
            Assert.Fail();
        }

        [TestMethod]
        public void PushBack_PushOneItem()
        {
            //arrange
            List<object> list = new List<object>();
            list.PushBack(5);
            object expected = 5;
            //act
            object actual = list[0];
            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PushBack_PushTwoItem()
        {
            //arrange
            List<object> list = new List<object>();
            list.PushBack(1);
            list.PushBack(2);
            object expectedZero=1,
                   expectedFirst = 2;
            //act
            object actualZero = list[0],
                   actualFirst = list[1];
            //assert
            Assert.IsTrue(Equals(expectedZero,actualZero) && Equals(expectedFirst,actualFirst));
        }

        [TestMethod]
        public void PushBack_PushTwoItem_CountEqual2()
        {
            //arrange
            List<object> list = new List<object>();
            list.PushBack(1);
            list.PushBack(2);
            int expected = 2;

            //act
            int actual = list.Count;

            //assert
            Assert.IsTrue(expected==list.Count);
        }

        [TestMethod]
        public void Enumerate_ChangeOneItem()
        {
            //arrange
            List<object> list = new List<object>();
            list.PushBack(1);
            object expected = 2;
            //act
            list[0] = 2;
            object actual = list[0];
            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Contains_PushAndSearchElement_True()
        {
            //arrange
            List<object> list = new List<object>();
            list.PushBack(0);
            //act
            bool actual = list.Contains(0);
            //assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void Contains_PushAndSearchDifferentElements_False()
        {
            //arrange
            List<object> list = new List<object>();
            list.PushBack(0);
            //act
            bool actual = list.Contains(1);
            //assert
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void IndexOfFirstEquals_WithoutEqual()
        {
            //arrange
            List<object> list = new List<object>();
            list.PushBack(0);
            list.PushBack(1);
            list.PushBack(2);
            int expected = -1;
            //act
            int actual = list.IndexOfFirstEquals(3);
            //assert
            Assert.IsTrue(actual == expected);
        }

        [TestMethod]
        public void IndexOfFirstEquals_TwotEqual()
        {
            //arrange
            List<object> list = new List<object>();
            list.PushBack(0);
            list.PushBack(1);
            list.PushBack(0);
            int expected = 0;
            //act
            int actual = list.IndexOfFirstEquals(0);
            //assert
            Assert.IsTrue(actual == expected);
        }

        [TestMethod]
        public void Insert_IndexMoreCounts() // test for exception
        {
            //arrange
            List<object> list = new List<object>();
            list.PushBack(0);
            //act
            list.Insert(3, 3);
            //assert
            Assert.Fail();
        }

        [TestMethod]
        public void Insert_AppendFirst()
        {
            //arrange
            List<object> list = new List<object>();
            list.PushBack(0);
            object expected = 1;
            //act
            list.Insert(1, 0);
            object actual = list[0];
            //assert
            Assert.AreEqual(expected,actual);
        }

        [TestMethod]
        public void Insert_AppendBack()
        {
            //arrange
            List<object> list = new List<object>();
            list.PushBack(0);
            object expected = 1;
            //act
            list.Insert(1, 1);
            object actual = list[1];
            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RemoveFirstEquals_RemoveFirstItem()
        {
            //arrange
            List<object> list = new List<object>();
            list.PushBack(0);
            list.PushBack(1);
            object expectedItem = 1;
            int expectedCount = 1;
            //act
            list.RemoveFirstEquals(0);
            object actualItem = list[0];
            int actualCount = list.Count;
            //assert
            Assert.IsTrue(expectedCount==actualCount && Equals(expectedItem,actualItem));
        }
    }
}
