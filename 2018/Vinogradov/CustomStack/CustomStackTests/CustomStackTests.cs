using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackClassLibrary;

namespace StackUnitTestProject
{
    [TestClass]
    public class CustomStackTests
    {
        [TestMethod]
        public void PushPop_CorrectWork()
        {
            CustomStack<int> stack = new CustomStack<int>();
            stack.Push(1);
            stack.Push(2);
            Assert.AreEqual(2, stack.Pop());
            Assert.AreEqual(1, stack.Pop());
        }

        [TestMethod]
        public void Push_CountIncrease()
        {
            CustomStack<int> stack = new CustomStack<int>();
            stack.Push(1);
            stack.Push(2);
            Assert.AreEqual(2, stack.Count);
        }

        [TestMethod]
        public void Pop_CountDecrease()
        {
            CustomStack<int> stack = new CustomStack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Pop();
            Assert.AreEqual(1, stack.Count);
        }

        [TestMethod]
        public void Peek_CorrectWork()
        {
            CustomStack<int> stack = new CustomStack<int>();
            stack.Push(1);
            stack.Push(2);
            Assert.AreEqual(2, stack.Peek());
        }

        [TestMethod]
        public void Clear_CorrectWork()
        {
            CustomStack<int> stack = new CustomStack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Clear();
            try
            {
                stack.Pop();
                Assert.Fail();
            }
            catch (InvalidOperationException)
            { }
        }

        [TestMethod]
        public void Contains_CorrectWork()
        {
            CustomStack<int> stack = new CustomStack<int>();
            stack.Push(1);
            stack.Push(2);
            Assert.AreEqual(true, stack.Contains(1));
            Assert.AreEqual(false, stack.Contains(6));
        }

        [TestMethod]
        public void ToString_CorrectWork()
        {
            CustomStack<int> stack = new CustomStack<int>();
            stack.Push(1);
            stack.Push(2);
            Assert.AreEqual("2 1 ", stack.ToString());
        }

        [TestMethod]
        public void ToArray_CorrectWork()
        {
            CustomStack<int> stack = new CustomStack<int>();
            stack.Push(1);
            stack.Push(2);
            var arr = stack.ToArray();
            Assert.AreEqual(2, arr[0]);
            Assert.AreEqual(1, arr[1]);
        }
    }
}
