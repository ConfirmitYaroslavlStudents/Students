using System;
using NUnit.Framework;
using StackInterface;

namespace Stack.Tests
{
    public class StackNunitTests
    {
        private static readonly int[] MassOfNumber = {73, 3, 7, 37};

        public readonly object[] StackTestData =
        {
            new StackArrayLibrary.Stack<int>(), new StackLibrary.Stack<int>()
        };

        public readonly object[] StackTestDataForConstructorWithCollection =
        {
            new StackArrayLibrary.Stack<int>(MassOfNumber), new StackLibrary.Stack<int>(MassOfNumber)
        };

        [Test]
        [TestCaseSource("StackTestData")]
        public void Stack_Count_LengthPlusAfterPush(IStack<int> stack)
        {
            const int countOfElement = 1;
            const int element = 37;

            stack.Push(element);
            var result = stack.Count;

            Assert.AreEqual(countOfElement, result);
        }

        [Test]
        [TestCaseSource("StackTestData")]
        public void Stack_Count_LengthMinusAfterPop(IStack<int> stack)
        {
            const int countOfElement = 0;
            const int element = 37;

            stack.Push(element);
            stack.Pop();
            var result = stack.Count;

            Assert.AreEqual(countOfElement, result);
        }

        [Test]
        [TestCaseSource("StackTestData")]
        public void Stack__PushPop_InitialValueAndReturnValueMustBeEqual(IStack<int> stack)
        {
            const int element = 37;

            stack.Push(element);
            var result = stack.Pop();

            Assert.AreEqual(element, result);
        }

        [Test]
        [TestCaseSource("StackTestData")]
        public void Stack__PushPeek_InitialValueAndReturnValueMustBeEqual(IStack<int> stack)
        {
            const int element = 37;

            stack.Push(element);
            var result = stack.Peek();

            Assert.AreEqual(element, result);
        }

        [Test]
        [TestCaseSource("StackTestData")]
        [ExpectedException(typeof (InvalidOperationException))]
        public void Stack_Peek_WhenStackIsEmptyShouldThrowInvalidOperationException(IStack<int> stack)
        {
            stack.Peek();
        }

        [Test]
        [TestCaseSource("StackTestData")]
        [ExpectedException(typeof (InvalidOperationException))]
        public void Stack_Pop_WhenStackIsEmptyShouldThrowInvalidOperationException(IStack<int> stack)
        {
            stack.Pop();
        }

        [Test]
        [TestCaseSource("StackTestData")]
        public void Stack_Clear_CountMustBeZeroAfterClear(IStack<int> stack)
        {
            const int countOfElement = 0;

            foreach (var element in MassOfNumber)
                stack.Push(element);
            stack.Clear();
            var result = stack.Count;

            Assert.AreEqual(countOfElement, result);
        }

        [Test]
        [TestCaseSource("StackTestDataForConstructorWithCollection")]
        public void Stack_Constructor_CollectionMustBeInStack(IStack<int> stack)
        {
            for (var i = 0; i < stack.Count; ++i)
            {
                Assert.AreEqual(stack[i], MassOfNumber[MassOfNumber.Length - i - 1]);
            }
        }

        [Test]
        [TestCaseSource("StackTestData")]
        public void Stack_Contains_ContainsMustWork(IStack<int> stack)
        {
            const int numberIsNotFromMass = 703;

            foreach (var elememt in MassOfNumber)
                stack.Push(elememt);

            var actual = stack.Contains(MassOfNumber[0]);
            Assert.AreEqual(true, actual);

            actual = stack.Contains(numberIsNotFromMass);
            Assert.AreEqual(false, actual);
        }

        [Test]
        [TestCaseSource("StackTestData")]
        public void Stack_IEnumerable_IEnumerableMustWorking(IStack<int> stack)
        {
            foreach (var element in MassOfNumber)
                stack.Push(element);
            var i = 0;

            foreach (var element in stack)
            {
                Assert.AreEqual(element, stack[i++]);
            }
        }
    }
}
