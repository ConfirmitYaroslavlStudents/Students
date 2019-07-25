using System;
using Xunit;
using Queue;

namespace XUnitTestQueue
{
    public class UnitTestQueue

    {
        [Fact]
        public void ItemEnqueue()
        {
            var queue = new Queue<int>();

            queue.Enqueue(0);
            queue.Enqueue(1);

            Assert.Equal(0, queue.Peek());
        }
        [Fact]
        public void PeekEmptyQueue()
        {
            var queue = new Queue<int>();

            Assert.Throws<InvalidOperationException>(() => queue.Peek());
        }
        [Fact]
        public void DequeueEmptyQueue()
        {
            var queue = new Queue<int>();

            Assert.Throws<InvalidOperationException>(() => queue.Dequeue());
        }
        [Fact]
        public void ItemPeek()
        {
            var queue = new Queue<int>();

            queue.Enqueue(0);
            queue.Enqueue(1);

            Assert.Equal(0, queue.Peek());
            Assert.Equal(2, queue.Count);
        }
        [Fact]
        public void ItemDequeue()
        {
            var queue = new Queue<int>();

            queue.Enqueue(0);
            queue.Enqueue(1);

            Assert.Equal(0, queue.Dequeue());
            Assert.Equal(1, queue.Count);
        }
        [Fact]
        public void CountThreeItems()
        {
            var queue = new Queue<int>();

            queue.Enqueue(0);
            queue.Enqueue(1);
            queue.Enqueue(2);

            Assert.Equal(3, queue.Count);
        }
        [Fact]
        public void IncreaseArray()
        {
            var queue = new Queue<int>();

            for (int i = 0; i < 100; i++)
                queue.Enqueue(i);

            Assert.Equal(100, queue.Count);
            Assert.Equal(0, queue.Peek());
        }
    }
}
