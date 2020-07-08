using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Queue;

namespace QueueTest
{
    [TestClass]
    public class QueueTest
    {
        [TestMethod]
        public void CreateZeroQueue()
        {
            Queue<int> queue = new Queue<int>();
            Assert.AreEqual(0, queue.Count);
        }
        [TestMethod]
        public void EnqueueWorkCorrectly()
        {
            Queue<int> queue = new Queue<int>();
            for (int i = 0; i < 10; i++)
            {
                queue.Enqueue(i);
            }
            Assert.AreEqual(10, queue.Count);
        }
        [TestMethod]
        public void DequeueWorkCorrectly()
        {
            Queue<int> queue = new Queue<int>();
            for (int i = 1; i < 11; i++)
            {
                queue.Enqueue(i);
            }
            Assert.AreEqual(1, queue.Dequeue());
            Assert.AreEqual(9, queue.Count);
        }
        [TestMethod]
        public void PeekWorkCorrectly()
        {
            Queue<int> queue = new Queue<int>();
            for (int i = 1; i < 11; i++)
            {
                queue.Enqueue(i);
            }
            Assert.AreEqual(1, queue.Peek());
            Assert.AreEqual(10, queue.Count);
        }
        [TestMethod]
        public void CleerWorkCorrectly()
        {
            Queue<int> queue = new Queue<int>();
            for (int i = 1; i < 11; i++)
            {
                queue.Enqueue(i);
            }
            queue.Clear();
            Assert.AreEqual(0, queue.Count);
        }
        [TestMethod]
        public void ConteinsTryWorkCorrectly()
        {
            Queue<int> queue = new Queue<int>();
            for (int i = 1; i < 11; i++)
            {
                queue.Enqueue(i);
            }
            Assert.AreEqual(true, queue.Contains(3));
        }
        [TestMethod]
        public void ConteinsFalseWorkCorrectly()
        {
            Queue<int> queue = new Queue<int>();
            for (int i = 1; i < 11; i++)
            {
                queue.Enqueue(i);
            }
            Assert.AreEqual(false, queue.Contains(12));
        }
    }
}
