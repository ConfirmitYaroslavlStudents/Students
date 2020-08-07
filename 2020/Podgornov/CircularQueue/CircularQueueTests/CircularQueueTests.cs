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
            if (count < 1||count > queue.Count)
                throw new ArgumentException("frong size");
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
            Assert.AreEqual(0, queue.Count);
        }

        [TestMethod]
        public void EnqueueWorkCorrectly()
        {
            var queue = Set_Queue();
            Assert.AreEqual(10, queue.Count);
        }

        [TestMethod]
        public void DequeueWorkCorrectly()
        {
            var queue = Set_Queue();
            Assert.AreEqual(0, queue.Dequeue());
            Assert.AreEqual(9, queue.Count);
            CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, Get_Queue(queue,queue.Count));
        }

        [TestMethod]
        public void PeekWorkCorrectly()
        {
            var queue = Set_Queue();
            Assert.AreEqual(0, queue.Peek());
            Assert.AreEqual(10, queue.Count);
        }

        [TestMethod]
        public void CircularQueue_ResizeFalseTest_If_Count_not_MAX()
        {
            var queue = Set_Queue();
            Assert.AreEqual(10, queue.Count);
            Assert.AreEqual(10, queue.Size);
            CollectionAssert.AreEqual(new int[] { 0, 1, 2, 3, 4 }, Get_Queue(queue, 5));
            Assert.AreEqual(5, queue.Count);
            Assert.AreEqual(10, queue.Size);
            for (int i = 0; i < 5; i++) 
            {
                queue.Enqueue(i + 10);
            }
            Assert.AreEqual(10, queue.Count);
            Assert.AreEqual(10, queue.Size);
            CollectionAssert.AreEqual(Enumerable.Range(5, 10).ToArray(), QueueToArray(queue));
        }

        [TestMethod]
        public void CircularQueue_ResizeFalseTes()
        {
            var queue = Set_Queue();
            Assert.AreEqual(10, queue.Count);
            Assert.AreEqual(10, queue.Size);
            queue.Enqueue(10);
            Assert.AreEqual(11, queue.Count);
            Assert.AreEqual(20, queue.Size);
        }

        [TestMethod]
        public void CircularQueue_HardWorkTest()
        {
            var queue = Set_Queue(100);
            Assert.AreEqual(100, queue.Count);
            Assert.AreEqual(160, queue.Size);
            CollectionAssert.AreEqual(Enumerable.Range(0, 100).ToArray(), QueueToArray(queue));
            Get_Queue(queue, 50);
            Assert.AreEqual(50, queue.Count);
            Assert.AreEqual(160, queue.Size);
            for (int i = 0; i < 110; i++)
            {
                queue.Enqueue(i);
            }
            Assert.AreEqual(160, queue.Count);
            Assert.AreEqual(160, queue.Size);
        }
    }
}
