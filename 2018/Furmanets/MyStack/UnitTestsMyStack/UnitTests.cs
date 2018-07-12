using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackClass;

namespace UnitTestsMyStack
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void InitStackTest()
        {
            var stack = new MyStack<int>();
        }

        [TestMethod]
        public void PushStackTest()
        {
            var stack = new MyStack<int>();

            for (int i = 0; i < 10; i++)
            {
                stack.Push(i);
            }

            Assert.AreEqual(10, stack.Count);
        }

        [TestMethod]
        public void PopStackTest()
        {
            var stack = new MyStack<int>();
            var rand = new Random();
            int last = 0;

            for (int i = 0; i < 20; i++)
            {
                last = rand.Next(0, 100);
                stack.Push(last);
            }

            Assert.AreEqual(20, stack.Count);
            Assert.AreEqual(last, stack.Pop());
            Assert.AreEqual(19, stack.Count);
        }

        [TestMethod]
        public void PeekStackTest()
        {
            var stack = new MyStack<int>();
            var rand = new Random();
            int last = 0;

            for (int i = 0; i < 46; i++)
            {
                last = rand.Next(0, 1000);
                stack.Push(last);
            }

            Assert.AreEqual(46, stack.Count);
            Assert.AreEqual(last, stack.Peek());
            Assert.AreEqual(46, stack.Count);
        }

        [TestMethod]
        public void StackClearTest()
        {
            var stack = new MyStack<int>();
            var rand = new Random();

            for (int i = 0; i < 46; i++)
            {
                stack.Push(rand.Next(0, 1000));
            }

            Assert.AreEqual(46, stack.Count);
            stack.Clear();
            Assert.AreEqual(0, stack.Count);
        }

        [TestMethod]
        public void StackContainsTest()
        {
            var stack = new MyStack<int>();
            var rand = new Random();
            var list = new List<int>();

            for (int i = 0; i < 46; i++)
            {
                var last = rand.Next(0, 1000);
                stack.Push(last);
                list.Add(last);
            }

            list.Sort();

            foreach (var obj in list)
            {
                Assert.IsTrue(stack.Contains(obj));
            }
        }

        [TestMethod]
        public void TestContains()
        {
            var stack = new MyStack<int>();

            for (int i = 0; i < 10; i++)
            {
                stack.Push(i);
            }

            Assert.AreEqual(10, stack.Count);

            for (int i = 0; i < 10; i++)
            {
                Assert.AreEqual(10 - i, stack.Count);
                Assert.IsTrue(stack.Contains(9 - i));
                stack.Pop();
                Assert.IsFalse(stack.Contains(9 - i));
                Assert.AreEqual(9 - i, stack.Count);
            }

        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void PopFromEmptyStack()
        {
            var stack = new MyStack<int>();

            stack.Pop();
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void PeekFromEmptyStack()
        {
            var stack = new MyStack<int>();

            stack.Peek();
        }
    }
}
