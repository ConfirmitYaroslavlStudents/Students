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
        public void InitStack()
        {
            var stack = new MyStack<int>();
        }

        [TestMethod]
        public void PushInStackTenElements_StackCountEquals10()
        {
            var stack = new MyStack<int>();

            for (int i = 0; i < 10; i++)
            {
                stack.Push(i);
            }

            Assert.AreEqual(10, stack.Count);
        }

        [TestMethod]
        public void PushInStackTwentyElements_PopOneElement_CountEquals19()
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
        public void PushInStack46Elements_PeekOneElements_StackCountEquals46()
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
        public void PushInStack46Elemets_StackClear_StackCountEquals0()
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
        public void PushInStack46Elemets_StackContains_ContainsReturnTrue()
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
        public void PushInStack46Elemets_StackContains_ContainsReturnFalse()
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
        public void PopFromEmptyStack_Exeption()
        {
            var stack = new MyStack<int>();
            try
            {
                stack.Pop();
            }
            catch (IndexOutOfRangeException e)
            {
            }

        }

        [TestMethod]
        public void PeekFromEmptyStack_Exeption()
        {
            var stack = new MyStack<int>();
            try
            {
                stack.Pop();
            }
            catch (IndexOutOfRangeException e)
            {
            }
        }
    }
}
