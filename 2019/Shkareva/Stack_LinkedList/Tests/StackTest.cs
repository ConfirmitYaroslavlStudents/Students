using Xunit;
using Stack_LinkedList;
using Assert = Xunit.Assert;

namespace Tests
{
    public class StackTest
    {
        [Fact]
        public void PushElement()
        {
            var stack = new Stack<int>();

            stack.Push(1);

            Assert.Equal(1, stack.Pop());
        }

        [Fact]
        public void PopEmptyStack()
        {
            var stack = new Stack<int>();

            Assert.Throws<StackException>(() => stack.Pop());
        }

        [Fact]
        public void Peek()
        {
            var stack = new Stack<string>();

            stack.Push("one");

            Assert.Equal("one", stack.Peek());
            Assert.Equal(1, stack.Count);
        }

        [Fact]
        public void PeekEmptyStack()
        {
            var stack = new Stack<int>();

            Assert.Throws<StackException>(() => stack.Peek());
        }

        [Fact]
        public void PushPop()
        {
            var stack = new Stack<int>();

            stack.Push(1);

            Assert.Equal(1, stack.Pop());
            Assert.Equal(0, stack.Count);
        }

        [Fact]
        public void PushTenElements()
        {
            var stack = new Stack<int>();

            for (int i = 0; i < 10; i++)
            {
                stack.Push(i);
            }

            Assert.Equal(10, stack.Count);
        }
    }
}
