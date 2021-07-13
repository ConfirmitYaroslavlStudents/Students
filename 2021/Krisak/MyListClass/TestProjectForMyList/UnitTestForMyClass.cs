using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyListClass;
using System;

namespace TestProjectForMyList
{
    [TestClass]
    public class UnitTestForMyClass
    {
        private MyList<int> _list;

        [TestInitialize]
        public void Init()
        {
            _list = new MyList<int>();
        }

        [TestMethod]
        public void CountEqualsZeroAfterListCreation()
        {
            Assert.AreEqual(0, _list.Count);
        }

        [TestMethod]
        public void CountShouldIncreaseAfterAdding()
        {
            var n = 10;
            for (var i = 0; i < n; i++)
                _list.Add(i);

            Assert.AreEqual(n, _list.Count);
        }

        [TestMethod]
        public void CountShouldIncreaseAfterAddingIn()
        {
            var n = 5;
            int[] ints = { 4, 5, 2, 1, 3 };

            _list.Add(1);
            _list.AddIn(2, 0);
            _list.AddIn(3, 1);
            _list.AddIn(4, 0);
            _list.AddIn(5, 1);

            Assert.AreEqual(n, _list.Count);
        }

        [TestMethod]
        public void ItemsExistsAfterAdding()
        {
            var n = 10;
            for (int i = 0; i < n; i++)
                _list.Add(i);

            for (var i = 0; i < n; i++)
                Assert.AreEqual(i, _list[i]);
        }

        [TestMethod]
        public void ItemsExistsAfterAddingIn()
        {
            var n = 5;
            int[] ints = { 4, 5, 2, 3, 1 };

            _list.Add(1);
            _list.AddIn(2, 0);
            _list.AddIn(3, 1);
            _list.AddIn(4, 0);
            _list.AddIn(5, 1);

            for (var i = 0; i < n; i++)
                Assert.AreEqual(ints[i], _list[i]);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void ExceptionAfterRequestingNegativeIndex()
        {
            _list.Add(1);
            var a = _list[-1];
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void ExceptionAfterRequestingIndexGreaterCount()
        {
            _list.Add(1);
            var a = _list[2];
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void ExceptionChangingAtNegativeIndex()
        {
            _list.Add(1);
            _list[-1] = 1;
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void ExceptionChangingAtIndexGreaterCount()
        {
            _list.Add(1);
            _list[2] = 1;
        }
    }
}

