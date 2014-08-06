using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Extensions;
using StackInterface;

namespace Stack.Tests
{
    public class StackXunitTests
    {
        private static readonly int[] MassOfNumber = { 73, 3, 7, 37 };

        public static IEnumerable<object[]> StackTestData
        {
            get
            {
                yield return new object[] { new StackArrayLibrary.Stack<int>() };
                yield return new object[] { new StackLibrary.Stack<int>() };
            }
        }

        public static IEnumerable<object[]> StackTestDataForConstructorWithCollection
        {
            get
            {
                yield return new object[] { new StackArrayLibrary.Stack<int>(MassOfNumber) };
                yield return new object[] { new StackLibrary.Stack<int>(MassOfNumber) };
            }
        }

        [Theory]
        [PropertyData("StackTestData")]
        public void Stack_Count_LengthPlusAfterPush(IStack<int> stack)
        {
            const int countOfElement = 1;
            const int element = 37;

            stack.Push(element);
            var result = stack.Count;

            Assert.Equal(countOfElement, result);
        }

        [Theory]
        [PropertyData("StackTestData")]
        public void Stack_Count_LengthMinusAfterPop(IStack<int> stack)
        {
            const int countOfElement = 0;
            const int element = 37;

            stack.Push(element);
            stack.Pop();
            var result = stack.Count;

            Assert.Equal(countOfElement, result);
        }

        [Theory]
        [PropertyData("StackTestData")]
        public void Stack__PushPop_InitialValueAndReturnValueMustBeEqual(IStack<int> stack)
        {
            const int element = 37;

            stack.Push(element);
            var result = stack.Pop();

            Assert.Equal(element, result);
        }

        [Theory]
        [PropertyData("StackTestData")]
        public void Stack__PushPeek_InitialValueAndReturnValueMustBeEqual(IStack<int> stack)
        {
            const int element = 37;

            stack.Push(element);
            var result = stack.Peek();

            Assert.Equal(element, result);
        }

        [Theory]
        [PropertyData("StackTestData")]
        public void Stack_Peek_WhenStackIsEmptyShouldThrowInvalidOperationException(IStack<int> stack)
        {
            Assert.Throws(typeof(InvalidOperationException), () =>
            {
                stack.Peek();
            });
        }

        [Theory]
        [PropertyData("StackTestData")]
        public void Stack_Pop_WhenStackIsEmptyShouldThrowInvalidOperationException(IStack<int> stack)
        {
            Assert.Throws(typeof(InvalidOperationException), () =>
            {
                stack.Pop();
            });

        }

        [Theory]
        [PropertyData("StackTestData")]
        public void Stack_Clear_CountMustBeZeroAfterClear(IStack<int> stack)
        {
            const int countOfElement = 0;

            foreach (var element in MassOfNumber)
                stack.Push(element);
            stack.Clear();
            var result = stack.Count;


            Assert.Equal(countOfElement, result);
        }

        [Theory]
        [PropertyData("StackTestDataForConstructorWithCollection")]
        public void Stack_Constructor_CollectionMustBeInStack(IStack<int> stack)
        {

            for (var i = 0; i < stack.Count; ++i)
            {
                Assert.Equal(stack[i], MassOfNumber[MassOfNumber.Length - i - 1]);
            }
        }

        [Theory]
        [PropertyData("StackTestData")]
        public void Stack_Contains_ContainsMustWork(IStack<int> stack)
        {
            const int numberIsNotFromMass = 703;

            foreach (var elememt in MassOfNumber)
                stack.Push(elememt);

            var actual = stack.Contains(MassOfNumber[0]);
            Assert.Equal(true, actual);

            actual = stack.Contains(numberIsNotFromMass);
            Assert.Equal(false, actual);
        }

        [Theory]
        [PropertyData("StackTestData")]
        public void Stack_IEnumerable_IEnumerableMustWorking(IStack<int> stack)
        {
            foreach (var element in MassOfNumber)
                stack.Push(element);
            var i = 0;

            foreach (var element in stack)
            {
                Assert.Equal(element, stack[i++]);
            }
        }
    }
}
