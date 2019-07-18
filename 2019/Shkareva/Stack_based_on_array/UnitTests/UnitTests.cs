using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;
using Stack_based_on_array;
using Assert = Xunit.Assert;

namespace UnitTests
{
    public class UnitTests
    {
        [Fact]
        public void PushElement()
        {
            var stack = new Stack<int>(10);

            stack.Push(1);

            Assert.Equal(1, stack.Pop());
        }

        [Fact]
        public void StackOverflow()
        {
            var stack = new Stack<int>(1);

            stack.Push(1);

            Assert.Throws<StackException>(()=> stack.Push(2));
            Assert.Equal(1, stack.Pop());
        }

        [Fact]
        public void PopEmptyStack()
        {
            var stack = new Stack<int>(10);

            Assert.Throws<StackException>(() => stack.Pop());            
        }

        [Fact]
        public void Peek()
        {
            var stack = new Stack<string>(10);

            stack.Push("one");

            Assert.Equal("one", stack.Peek());
            Assert.Equal(1, stack.Count);
        }

        [Fact]
        [ExpectedException(typeof(Exception))]
        public void PeekEmptyStack()
        {
            var stack = new Stack<int>(10);

            Assert.Throws<StackException>(() => stack.Peek());
        }

        [Fact]
        public void PushPop()
        {
            var stack = new Stack<int>(10);

            stack.Push(1);

            Assert.Equal(1, stack.Pop());
            Assert.Equal(0, stack.Count);
        }

        [Fact]
        public void PushTenElements()
        {
            var stack = new Stack<int>(10);

            for (int i = 0; i < 10; i++)
            {
                stack.Push(i);
            }

            Assert.Equal(10, stack.Count);
        }
    }
}
