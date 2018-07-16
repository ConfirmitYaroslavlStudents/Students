using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueueImplementations;

namespace UnitTestQueueImplementations
{
    [TestClass]
    public class UnitTestQueue
    {
        IQueue<int> queue = new Queue<int>();
        //IQueue<int> queue = new ArrayQueue<int>();

        [TestMethod]
        public void TestEnqueue()
        {
            int expected = 5;
            queue.Enqueue(expected);
            Assert.AreEqual(expected, queue.Last());
        }

        [TestMethod]
        public void TestDequeue()
        {
            int expected = 6;
            queue.Enqueue(expected);
            queue.Enqueue(4);
            queue.Enqueue(3);
            Assert.AreEqual(expected, queue.Dequeue());
        }

        [TestMethod]
        public void TestCount()
        {
            int expected = 15;
            for (int i = 0; i < expected; i++)
                queue.Enqueue(i);
            Assert.AreEqual(expected, queue.Count());
        }

        [TestMethod]
        public void TestContains()
        {
            for (int i = 0; i < 15; i++)
                queue.Enqueue(i);
            Assert.IsTrue(queue.Contains(10));
        }

        public void TestNotContains()
        {
            for (int i = 0; i < 15; i++)
                queue.Enqueue(i);
            Assert.IsFalse(queue.Contains(30));
        }

        [TestMethod]
        public void TestLast()
        {
            int expected = 15;
            for (int i = 0; i <=expected; i++)
                queue.Enqueue(i);
            Assert.AreEqual(expected, queue.Last());
        }

        [TestMethod]
        public void TestFirst()
        {
            int expected = 3;
            for (int i = expected; i < expected+10; i++)
                queue.Enqueue(i);
            Assert.AreEqual(expected, queue.First());
        }
    }
}
