using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using CircularQueueTDD;
using System;

namespace CircularQueue.Test
{
    [TestClass]
    public class CircularQueueTest
    {
        [TestMethod]
        public void CreateZeroQueue()
        {
            var Queue = new CircularQueue<int>();
            var list = new List<int>();

            Assert.AreEqual(0, Queue.Count);
        }
        [TestMethod]
        public void CreateQueueWithDefaultCapasity()
        {
            var Queue = new CircularQueue<int>();

            Assert.AreEqual(10, Queue.Capasity);
        }
        [TestMethod]
        public void CreateQueueWith1000Capasity()
        {
            var Queue = new CircularQueue<int>(1000);

            Assert.AreEqual(1000, Queue.Capasity);
        }
        [TestMethod]
        public void Enqueue_IncCount()
        {
            var Queue = new CircularQueue<int>();
            Queue.Enqueue(10);

            Assert.AreEqual(1, Queue.Count);
        }
        [TestMethod]
        public void Dequeue_DecCount()
        {
            var Queue = new CircularQueue<int>();
            Queue.Enqueue(0);
            Queue.Enqueue(3);
            Queue.Dequeue();

            Assert.AreEqual(1, Queue.Count);
        }
        [TestMethod]
        public void Dequeue_ReturnCorrectItem()
        {
            var Queue = new CircularQueue<int>();
            Queue.Enqueue(1);
            Queue.Enqueue(3);
            var inem = Queue.Dequeue();

            Assert.AreEqual(1, inem);
        }
        [TestMethod]
        public void Dequeue_throwInvalidOperationException()
        {
            var Queue = new CircularQueue<int>();

            Assert.ThrowsException<InvalidOperationException>(() => Queue.Dequeue());
        }
        [TestMethod]
        public void Peek_ReturnCorrectItem()
        {
            var Queue = new CircularQueue<int>();
            Queue.Enqueue(1);
            Queue.Enqueue(3);
            var inem = Queue.Peek();

            Assert.AreEqual(1, inem);
        }
        [TestMethod]
        public void TestQueueOnCirculary()
        {
            var Queue = new CircularQueue<int>(2);
            Queue.Enqueue(1);
            Queue.Enqueue(3);
            Queue.Dequeue();
            Queue.Enqueue(2);

            Assert.AreEqual(2, Queue.Capasity);
        }
        [TestMethod]
        public void QueueWorkCorrectly()
        {
            var queue = new CircularQueue<int>();
            var masQueue = new int[5];
            for (int i = 0; i < 80; i++)
            {
                queue.Enqueue(i);
            }
            for (int i = 0; i < 5; i++)
            {
                masQueue[i] = queue.Dequeue();
            }
            for (int i = 0; i < 5; i++)
            {
                queue.Enqueue(i);
            }

            Assert.AreEqual(80, queue.Count);
            Assert.AreEqual(80, queue.Capasity);
            CollectionAssert.AreEqual(new int[] { 0, 1, 2, 3, 4 }, masQueue);
        }
    }
}
