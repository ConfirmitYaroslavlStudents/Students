using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CircularQueue;

namespace CircularQueueTests
{
    [TestClass]
    public class CircularQueueTests
    {
        private CircularQueue<int> Set_Queue(int count = 10)
        {
            var queue = new CircularQueue<int>();
            for (int i = 0; i < count; i++)
            {
                queue.Enqueue(i);
            }
            return queue;
        }

        private int[] Get_Queue(CircularQueue<int> queue , int count)
        {
            if (count < 1||count > queue.Size)
                throw new ArgumentException("wrong size");
            var result = new int[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = queue.Dequeue();
            }
            return result;
        }

        private int[] QueueToArray(CircularQueue<int> queue) => (from i in queue select i).ToArray();

        [TestMethod]
        public void CreateZeroQueue()
        {
            var queue = new CircularQueue<int>();
            Assert.AreEqual(0, queue.Size);
        }

        [TestMethod]
        public void EnqueueWorkCorrectly()
        {
            var queue = Set_Queue();
            Assert.AreEqual(10, queue.Size);
        }

        [TestMethod]
        public void DequeueWorkCorrectly()
        {
            var queue = Set_Queue();
            Assert.AreEqual(0, queue.Dequeue());
            Assert.AreEqual(9, queue.Size);
            CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, Get_Queue(queue,queue.Size));
        }

        [TestMethod]
        public void PeekWorkCorrectly()
        {
            var queue = Set_Queue();
            Assert.AreEqual(0, queue.Peek());
            Assert.AreEqual(10, queue.Size);
        }

        [TestMethod]
        public void CircularQueue_ResizeFalseTest_If_Count_not_MAX()
        {
            var queue = Set_Queue();
            Assert.AreEqual(10, queue.Size);
            Assert.AreEqual(10, queue.Capacity);
            CollectionAssert.AreEqual(new int[] { 0, 1, 2, 3, 4 }, Get_Queue(queue, 5));
            Assert.AreEqual(5, queue.Size);
            Assert.AreEqual(10, queue.Capacity);
            for (int i = 0; i < 5; i++) 
            {
                queue.Enqueue(i + 10);
            }
            Assert.AreEqual(10, queue.Size);
            Assert.AreEqual(10, queue.Capacity);
            CollectionAssert.AreEqual(Enumerable.Range(5, 10).ToArray(), QueueToArray(queue));
        }

        [TestMethod]
        public void CircularQueue_ResizeFalseTes()
        {
            var queue = Set_Queue();
            Assert.AreEqual(10, queue.Size);
            Assert.AreEqual(10, queue.Capacity);
            queue.Enqueue(10);
            Assert.AreEqual(11, queue.Size);
            Assert.AreEqual(20, queue.Capacity);
        }

    }
}
