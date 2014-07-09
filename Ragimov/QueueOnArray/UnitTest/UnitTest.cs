using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class QueueUnitTest
    {
        [TestMethod]
        public void EnAndDeQueueTest()
        {
            var queue = new QueueOnArray.Queue<int>();
            queue.Enqueue(42);
            Assert.AreEqual(queue.Dequeue(), 42);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DeleteFromEmpty_ShouldThrowInvalidOperation()
        {
            var queue = new QueueOnArray.Queue<int>();
            queue.Dequeue();
        }

        [TestMethod]
        public void CountIsProperlyChangingTest()
        {
            var queue = new QueueOnArray.Queue<int>();
            queue.Enqueue(42);
            var temp = queue.Count;
            queue.Enqueue(37);
            queue.Enqueue(queue.Dequeue());
            Assert.AreEqual(queue.Count, temp + 1);
        }
    }
}
