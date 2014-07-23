using System;
using NUnit.Framework;
using StackInterface;

namespace Stack.Tests
{
    public class StackNunitTests
    {
        private static readonly int[] MassOfNumber = { 73, 3, 7, 37 };

        private readonly object[] _simpleTestData =
        {
            new StackArrayLibrary.Stack<string>(), new StackLibrary.Stack<string>()
        };

        private readonly object[] _testDataWithCollection =
        {
            new object[] {new StackArrayLibrary.Stack<int>(), new StackArrayLibrary.Stack<int>(MassOfNumber)},
            new object[] {new StackLibrary.Stack<int>(), new StackLibrary.Stack<int>(MassOfNumber)}
        };
     
        [Test]
        [TestCaseSource("_simpleTestData")]
        public void Stack_Count_LengthPlusAfterPush(IStack<string> stack)
        {
            stack.Push("1");
            var result = stack.Count;
            Assert.AreEqual(1, result);
        }

        [Test]
        [TestCaseSource("_simpleTestData")]
        public void Stack_Count_LengthMinusAfterPop(IStack<string> stack)
        {
            stack.Push("1");
            stack.Pop();
            var result = stack.Count;

            Assert.AreEqual(0, result);
        }

        [Test]
        [TestCaseSource("_simpleTestData")]
        public void Stack__PushPop_InitialValueAndReturnValueMustBeEqual(IStack<string> stack)
        {
            stack.Push("test");
            var result = stack.Pop();

            Assert.AreEqual("test", result);
        }

        [Test]
        [TestCaseSource("_simpleTestData")]
        public void Stack__PushPeek_InitialValueAndReturnValueMustBeEqual(IStack<string> stack)
        {
            stack.Push("test");
            var result = stack.Peek();

            Assert.AreEqual("test", result);
        }

        [Test]
        [TestCaseSource("_simpleTestData")]
        [ExpectedException(typeof (InvalidOperationException))]
        public void Stack_Peek_WhenStackIsEmptyShouldThrowInvalidOperationException(IStack<string> stack)
        {
            stack.Peek();
        }

        [Test]
        [TestCaseSource("_simpleTestData")]
        [ExpectedException(typeof (InvalidOperationException))]
        public void Stack_Pop_WhenStackIsEmptyShouldThrowInvalidOperationException(IStack<string> stack)
        {
            stack.Pop();
        }

        [Test]
        [TestCaseSource("_simpleTestData")]
        public void Stack_Clear_CountMustBeZeroAfterClear(IStack<string> stack)
        {
            string[] mass = { "73", "3", "7", "37" };

            foreach (var element in mass)
                stack.Push(element);
            stack.Clear();
            var result = stack.Count;

            Assert.AreEqual(0, result);
        }

        [Test]
        [TestCaseSource("_testDataWithCollection")]
        public void Stack_Constructor_CollectionMustBeInStack(IStack<int> actualStack, IStack<int> expectedStack)
        {
            foreach (var element in MassOfNumber)
                actualStack.Push(element);

            for (int i = 0; i < actualStack.Count; ++i)
            {
                Assert.AreEqual(actualStack[i], expectedStack[i]);
            }
        }

        [Test]
        [TestCaseSource("_simpleTestData")]
        public void Stack_Contains_ContainsMustReturnTrue(IStack<string> stack)
        {
            string[] mass = { "73", "3", "7", "37" };
            foreach (var elememt in mass)
                stack.Push(elememt);

            bool actual = stack.Contains("73");
            Assert.AreEqual(true, actual);
            
            actual = stack.Contains("703");
            Assert.AreEqual(false, actual);
        }

        [Test]
        [TestCaseSource("_simpleTestData")]
        public void Stack_Contains_ContainsWithNull(IStack<string> stack)
        {
            string[] mass = { "37", "cartoon", "raccoon", "joke", null };
            foreach (var element in mass)
                stack.Push(element);

            bool actual = stack.Contains(null);
            Assert.AreEqual(true, actual);

            actual = stack.Contains("joke");
            Assert.AreEqual(true, actual);
        }
    }
}
