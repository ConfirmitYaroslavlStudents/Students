using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stack_1;

namespace StackTests
{
    [TestClass]
    public class PopTest
    {
        [TestMethod]
        public void PopMethod()
        {
            IStack<int> stack = new Stack<int>();
            stack.Push(5);
            stack.Push(7);
            stack.Push(10);
            Assert.AreEqual(10, stack.Pop());
            Assert.AreEqual(7, stack.Pop());
            stack.Push(3);
            Assert.AreEqual(3, stack.Pop());
            Assert.AreEqual(5, stack.Pop());
        }
    }
}
