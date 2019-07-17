using System;
using Queue;
using Xunit;

namespace UnitTestQueue
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
            Assert.Throws<ArgumentOutOfRangeException>(()=>queue.Peek());
        }
        [Fact]
        public void DequeueEmptyQueue()
        {
            var queue = new Queue<int>();
            Assert.Throws<ArgumentOutOfRangeException>(() => queue.Dequeue());
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
        public void Count_AddThreeItems()
        {
            var queue = new Queue<int>();
            queue.Enqueue(0);
            queue.Enqueue(1);
            queue.Enqueue(2);
            Assert.Equal(3, queue.Count);
        }
        [Fact]
        public void Increase_Add100Items()
        {
            var queue = new Queue<int>();
            for (int i=0;i<100;i++)
            queue.Enqueue(i);
            Assert.Equal(100, queue.Count);
            Assert.Equal(0, queue.Peek());
        }
    }
}
