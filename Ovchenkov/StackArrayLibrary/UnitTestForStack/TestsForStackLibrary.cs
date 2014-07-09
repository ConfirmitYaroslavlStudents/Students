using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackArrayLibrary;

namespace UnitTestForStack
{
    [TestClass]
    public class StackTest
    {
        [TestMethod]
        public void StackCount_LengthPlusAfterPush()
        {
            var stack = new Stack<int>();
            stack.Push(1);
            var result = stack.Count;
            Assert.AreEqual(1, result);
        }
        [TestMethod]
        public void StackCount_LengthMinusAfterPop()
        {
            var stack = new Stack<int>();
            stack.Push(1);
            stack.Pop();
            var result = stack.Count;
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void StackPushPop__InitialValueAndReturnValueMustBeEqual()
        {
            var stack = new Stack<string>();
            stack.Push("test");
            var result = stack.Pop();
            Assert.AreEqual("test", result);
        }
        [TestMethod]
        public void StackPushPeek__InitialValueAndReturnValueMustBeEqual()
        {
            var stack = new Stack<String>();
            stack.Push("test");
            var result = stack.Peek();
            Assert.AreEqual("test", result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void StackPeek_WhenStackIsEmpty_ShouldThrowInvalidOperationException()
        {
            var stack = new Stack<String>();
            stack.Peek();
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void StackPop_WhenStackIsEmpty_ShouldThrowInvalidOperationException()
        {
            var stack = new Stack<int>();
            stack.Pop();
        }

    }
}
