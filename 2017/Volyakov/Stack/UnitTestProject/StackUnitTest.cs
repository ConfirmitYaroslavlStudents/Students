using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackLibrary;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest
    {
        private int[] _initArray = new int[] { 1, 2, 3, 4, 5 };

        [TestMethod]
        public void InitialCountEqualsZero()
        {
            Stack<int> stk = new Stack<int>();
            Assert.AreEqual(stk.Count, 0);
        }
        [TestMethod]
        public void InitWithArrayTest()
        {
            Stack<int> stk = new Stack<int>(_initArray);
            Assert.AreEqual(stk.Count, _initArray.Length);
        }
        [TestMethod]
        public void PushCountTest()
        {
            Stack<int> stk = new Stack<int>(_initArray);
            int expectedCount = stk.Count + 1;
            stk.Push(6);
            Assert.AreEqual(stk.Count, expectedCount);
        }
        [TestMethod]
        public void PopCountTest()
        {
            Stack<int> stk = new Stack<int>(_initArray);
            int expectedCount = stk.Count - 1;
            stk.Pop();
            Assert.AreEqual(stk.Count, expectedCount);
        }
        [TestMethod]
        public void PeekCountTest()
        {
            Stack<int> stk = new Stack<int>(_initArray);
            int expectedCount = stk.Count;
            stk.Peek();
            Assert.AreEqual(stk.Count, expectedCount);
        }
        [TestMethod]
        public void PushItemTest()
        {
            Stack<int> stk = new Stack<int>(_initArray);
            int expectedItem = 6;
            stk.Push(expectedItem);
            int actualItem = stk.Pop();
            Assert.AreEqual(actualItem, expectedItem);
        }
        [TestMethod]
        public void PopItemTest()
        {
            Stack<int> stk = new Stack<int>(_initArray);
            int firstItem = stk.Pop();
            int secondItem = stk.Pop();
            Assert.AreNotEqual(firstItem, secondItem);
        }
        [TestMethod]
        public void PeekItemTest()
        {
            Stack<int> stk = new Stack<int>(_initArray);
            int firstItem = stk.Peek();
            int secondItem = stk.Peek();
            Assert.AreEqual(firstItem, secondItem);
        }
        [TestMethod]
        public void ContainsTest()
        {
            Stack<int> stk = new Stack<int>(_initArray);
            int item = _initArray[0];
            bool expectedResult = true;
            Assert.AreEqual(stk.Contains(item), expectedResult);
        }
        [TestMethod]
        public void ClearTest()
        {
            Stack<int> stk = new Stack<int>(_initArray);
            stk.Clear();
            Assert.AreEqual(stk.Count, 0);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PopExceptionTest()
        {
            Stack<int> stk = new Stack<int>();
            stk.Pop();
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PeekExceptionTest()
        {
            Stack<int> stk = new Stack<int>();
            stk.Peek();
        }
    }
}
