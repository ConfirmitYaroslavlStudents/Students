using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackLibrary;
//using StackArrayLibrary;

namespace Stack.Tests
{
    [TestClass]
    public class StackTest
    {
        [TestMethod]
        public void Stack_Count_LengthPlusAfterPush()
        {
            var stack = new Stack<int>();
            stack.Push(1);
            var result = stack.Count;

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Stack_Count_LengthMinusAfterPop()
        {
            var stack = new Stack<int>();
            stack.Push(1);
            stack.Pop();
            var result = stack.Count;

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Stack__PushPop_InitialValueAndReturnValueMustBeEqual()
        {
            var stack = new Stack<string>();
            stack.Push("test");
            var result = stack.Pop();

            Assert.AreEqual("test", result);
        }

        [TestMethod]
        public void Stack__PushPeek_InitialValueAndReturnValueMustBeEqual()
        {
            var stack = new Stack<String>();
            stack.Push("test");
            var result = stack.Peek();

            Assert.AreEqual("test", result);
        }

        [TestMethod]
        [ExpectedException(typeof (InvalidOperationException))]
        public void Stack_Peek_WhenStackIsEmptyShouldThrowInvalidOperationException()
        {
            var stack = new Stack<String>();
            stack.Peek();
        }

        [TestMethod]
        [ExpectedException(typeof (InvalidOperationException))]
        public void Stack_Pop_WhenStackIsEmptyShouldThrowInvalidOperationException()
        {
            var stack = new Stack<int>();
            stack.Pop();
        }

        [TestMethod]
        public void Stack_Clear_CountMustBeZeroAfterClear()
        {
            var stack = new Stack<int>();
            int[] mass = { 73, 3, 7, 37 };

            for (int i = 0; i < mass.Length; ++i)
                stack.Push(mass[i]);
            stack.Clear();
            var result = stack.Count;

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Stack_Constructor_CollectionMustBeInStack()
        {
            var stack = new Stack<int>();
            int[] mass = {73, 3, 7, 37};
            var expectedStack = new Stack<int>(mass);

            for (int i = 0; i < mass.Length; ++i)
                stack.Push(mass[i]);

            for (int i = 0; i < stack.Count; ++i)
            {
                Assert.AreEqual(stack[i], expectedStack[i]);
            }
        }

        [TestMethod]
        public void Stack_Contains_ContainsMustReturnTrue()
        {
            var stack = new Stack<int>();
            int[] mass = { 73, 3, 7, 37 };
            for (int i = 0; i < mass.Length; ++i)
                stack.Push(mass[i]);
           
            bool actual = stack.Contains(73);
            Assert.AreEqual(true, actual);
            
            actual = stack.Contains(703);
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void Stack_Contains_ContainsWithNull()
        {
            var stack = new Stack<string>();
            string[] mass = { "34", "cartoon", "raccoon", "joke", null };
            for (int i = 0; i < mass.Length; ++i)
                stack.Push(mass[i]);

            bool actual = stack.Contains(null);
            Assert.AreEqual(true, actual);

            actual = stack.Contains("joke");
            Assert.AreEqual(true, actual);
        }

    }
}
