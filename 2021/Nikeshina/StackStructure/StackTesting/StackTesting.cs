using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackStructure;
using System;
using System.Collections.Generic;

namespace StackTesting
{
    [TestClass]
    public class StackTesting
    {
        [TestMethod]
        public void CompareCountAfterAddingElements()
        {
            var stack = new StackStructure.Stack<int>();

            for (var i = 0; i < 100; i++)
                stack.Push(i);

            Assert.AreEqual(100, stack.Count());
        }

        [TestMethod]
        public void CountOfEmptyStack()
        {
            var stack = new StackStructure.Stack<int>();

            Assert.AreEqual(0, stack.Count());
        }

        public void CompareTheContents()
        {
            var stack = new StackStructure.Stack<int>();

            for (var i = 0; i < 10; i++)
                stack.Push(i);

            for (var i = 9; i>= 0; i--)
                 Assert.AreEqual(i, stack.Pop());
        }

        [TestMethod]
        public void CountAfterClearingStack()
        {
            var stack = new StackStructure.Stack<int>();

            for (var i = 0; i < 100; i++)
            {
                stack.Push(i);
                stack.Pop();
            }

            Assert.AreEqual(0, stack.Count());
        }

        [TestMethod]
        public void CompareTheContentAfterPop()
        {
            var stack = new StackStructure.Stack<int>();
            var list = new List<int>();

            for (var i = 0; i < 100; i++)
            {
                stack.Push(i);
            }
            stack.Pop();

            for (var i = 98; i >-1; i--)
                Assert.AreEqual(stack.Pop(), i);
        }

        [TestMethod]
        public void NewPeekAfterPop()
        {
            var stack = new StackStructure.Stack<int>();

            for (var i = 0; i < 100; i++)
                stack.Push(i);
            for (var i = 99; i > 50; i--)
                stack.Pop();

            Assert.AreEqual(50, stack.Peek());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ProhibitedDeleteElementFromEmptyStack()
        {
            var stack = new StackStructure.Stack<int>();

            stack.Pop();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PeekOfEmptyStack()
        {
            var stack = new StackStructure.Stack<int>();

            stack.Peek();
        }
    }
}
