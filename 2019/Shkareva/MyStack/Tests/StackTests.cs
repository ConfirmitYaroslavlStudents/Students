using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyStack;


namespace Tests
{
    [TestClass]
    public class StackTests
    {
        [TestMethod]
        public void PushElement()
        {
            var stack = new Stack<int>();
            stack.Push(1);
            Assert.AreEqual(1, stack.Pop());
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void PopEmptyStack()
        {
            var stack = new Stack<int>();
            stack.Pop();
        }

        [TestMethod]
        public void Peek()
        {
            var stack = new Stack<string>();
            stack.Push("one");
            Assert.AreEqual("one", stack.Peek());
            Assert.AreEqual(1, stack.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void PeekEmptyStack()
        {
            var stack = new Stack<int>();
            stack.Peek();
        }

        [TestMethod]
        public void PushPop()
        {
            var stack = new Stack<int>();
            stack.Push(1);
            Assert.AreEqual(1, stack.Pop());
            Assert.AreEqual(0, stack.Count);
        }

        [TestMethod]
        public void PushTenElements()
        {
            var stack = new Stack<int>();

            for(int i = 0; i < 10; i++)
            {
                stack.Push(i);
            }

            Assert.AreEqual(10, stack.Count);
        }

    }
}
