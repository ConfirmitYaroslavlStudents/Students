using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackLibrary;

namespace StackTests
{
    [TestClass]
    public class StackTest
    {
        [TestMethod]
        public void PassPushGetLengthMinus()
        {
            var stack = new Stack<int>();
            stack.Push(1);
            stack.Pop();
            var result = stack.Length;
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void PassPushGetLengthPeek()
        {
            var stack = new Stack<int>();
            stack.Push(1);
            var result = stack.Pop();
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void PassPushGetLengthPlus()
        {
            var stack = new Stack<int>();
            stack.Push(1);
            var result = stack.Length;
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void PassPushGetPeek()
        {
            var stack = new Stack<int>();
            stack.Push(1);
            var result = stack.Peek();
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void PassForeachGetArray()
        {
            var stack = new Stack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            var result = new int[stack.Length];
            int i = stack.Length-1;
            foreach (int temp in stack)
            {
                result[i] = temp;
                i--;
            }
            for(i=stack.Length-1;i>0;i--)
            Assert.AreEqual(stack._array[i], result[i]);
        }
    }
}
