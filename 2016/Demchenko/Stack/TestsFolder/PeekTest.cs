using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stack_1;

namespace StackTests
{
    [TestClass]
    public class PeekTest
    {
        [TestMethod]
        public void PeekMethod()
        {
            IStack<int> stack = new Stack<int>();
            stack.Push(5);
            stack.Push(7);
            stack.Push(10);
            Assert.AreEqual(10, stack.Peek());
            Assert.AreEqual(10, stack.Peek());
            stack.Push(3);
            stack.Pop();
            Assert.AreEqual(10, stack.Peek());
        }
    }
}