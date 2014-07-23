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

        public static IEnumerable<object[]> SampleTestData
        {
            get
            {
                yield return new object[] { new StackArrayLibrary.Stack<string>() };
                yield return new object[] { new StackLibrary.Stack<string>() };
            }
        }

        public static IEnumerable<object[]> TestDataWithCollection
        {
            get
            {
                yield return new object[]
                {
                    new StackArrayLibrary.Stack<int>(),
                    new StackArrayLibrary.Stack<int>(MassOfNumber)
                };

                yield return new object[]
                {
                    new StackLibrary.Stack<int>(),
                    new StackLibrary.Stack<int>(MassOfNumber)
                };
            }
        }

        [Theory]
        [PropertyData("SampleTestData")]
        public void Stack_Count_LengthPlusAfterPush(IStack<string> stack)
        {
            stack.Push("1");
            var result = stack.Count;
            Assert.Equal(1, result);
        }

        [Theory]
        [PropertyData("SampleTestData")]
        public void Stack_Count_LengthMinusAfterPop(IStack<string> stack)
        {
            stack.Push("1");
            stack.Pop();
            var result = stack.Count;

            Assert.Equal(0, result);
        }

        [Theory]
        [PropertyData("SampleTestData")]
        public void Stack__PushPop_InitialValueAndReturnValueMustBeEqual(IStack<string> stack)
        {
            stack.Push("test");
            var result = stack.Pop();

            Assert.Equal("test", result);
        }

        [Theory]
        [PropertyData("SampleTestData")]
        public void Stack__PushPeek_InitialValueAndReturnValueMustBeEqual(IStack<string> stack)
        {
            stack.Push("test");
            var result = stack.Peek();

            Assert.Equal("test", result);
        }

        [Theory]
        [PropertyData("SampleTestData")]
        public void Stack_Peek_WhenStackIsEmptyShouldThrowInvalidOperationException(IStack<string> stack)
        {
            Assert.Throws(typeof(InvalidOperationException), () =>
            {
                stack.Peek();
            });
        }

        [Theory]
        [PropertyData("SampleTestData")]
        public void Stack_Pop_WhenStackIsEmptyShouldThrowInvalidOperationException(IStack<string> stack)
        {
            Assert.Throws(typeof(InvalidOperationException), () =>
            {
                stack.Pop();
            });

        }

        [Theory]
        [PropertyData("SampleTestData")]
        public void Stack_Clear_CountMustBeZeroAfterClear(IStack<string> stack)
        {
            string[] mass = { "73", "3", "7", "37" };

            foreach (var element in mass)
                stack.Push(element);
            stack.Clear();
            var result = stack.Count;

            Assert.Equal(0, result);
        }

        [Theory]
        [PropertyData("TestDataWithCollection")]
        public void Stack_Constructor_CollectionMustBeInStack(IStack<int> actualStack, IStack<int> expectedStack)
        {

            foreach (var element in MassOfNumber)
                actualStack.Push(element);

            for (int i = 0; i < actualStack.Count; ++i)
            {
                Assert.Equal(actualStack[i], expectedStack[i]);
            }
        }

        [Theory]
        [PropertyData("SampleTestData")]
        public void Stack_Contains_ContainsMustReturnTrue(IStack<string> stack)
        {
            string[] mass = { "73", "3", "7", "37" };
            foreach (var elememt in mass)
                stack.Push(elememt);

            bool actual = stack.Contains("73");
            Assert.Equal(true, actual);

            actual = stack.Contains("703");
            Assert.Equal(false, actual);
        }

        [Theory]
        [PropertyData("SampleTestData")]
        public void Stack_Contains_ContainsWithNull(IStack<string> stack)
        {
            string[] mass = { "37", "cartoon", "raccoon", "joke", null };
            foreach (var element in mass)
                stack.Push(element);

            bool actual = stack.Contains(null);
            Assert.Equal(true, actual);

            actual = stack.Contains("joke");
            Assert.Equal(true, actual);
        }

        
    }
}
