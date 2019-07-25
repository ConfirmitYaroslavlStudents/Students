using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;
using Stack_based_on_array;
using Assert = Xunit.Assert;

namespace StackTests
{
    public class StackOnArrayTests
    {
        [Fact]
        public void PushElement()
        {
            var stack = new StackOnArray<int>();

            stack.Push(1);

            Assert.Equal(1, stack.Pop());
        }

        [Fact]
        public void PopEmptyStack()
        {
            var stack = new StackOnArray<int>();

            Assert.Throws<StackException>(() => stack.Pop());            
        }

        [Fact]
        public void Peek()
        {
            var stack = new StackOnArray<string>();

            stack.Push("one");

            Assert.Equal("one", stack.Peek());
            Assert.Equal(1, stack.Count);
        }

        [Fact]
        public void PeekEmptyStack()
        {
            var stack = new StackOnArray<int>();

            Assert.Throws<StackException>(() => stack.Peek());
        }

        [Fact]
        public void PushPop()
        {
            var stack = new StackOnArray<int>();

            stack.Push(1);

            Assert.Equal(1, stack.Pop());
            Assert.Equal(0, stack.Count);
        }

        [Fact]
        public void PushTenElements()
        {
            var stack = new StackOnArray<int>();

            for (int i = 0; i < 10; i++)
            {
                stack.Push(i);
            }

            Assert.Equal(10, stack.Count);
        }
    }
}
