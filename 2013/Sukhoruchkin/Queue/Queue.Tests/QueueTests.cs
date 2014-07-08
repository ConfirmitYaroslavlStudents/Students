using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Queue;

namespace Queue.Tests
{
    [TestClass]
    public class QueueTests
    {
        [TestMethod]
        public void PassCreateQueueGetQueue()
        {
            var queue = Queue.Queue<int>.GetQueue();

            Assert.IsNotNull(queue);
        }

        [TestMethod]
        public void PassTwoQueuesGetIdentity()
        {
            var firstQueue = Queue.Queue<int>.GetQueue();
            var secondQueue = Queue.Queue<int>.GetQueue();

            Assert.AreSame(firstQueue, secondQueue);
        }

        [TestMethod]
        public void PassEnqueueItemGetCountIncreaseBy1()
        {
            var queue = Queue.Queue<int>.GetQueue();
            queue.Clear();
            queue.Enqueue(2);

            Assert.AreEqual(queue.Count, 1);
        }
        [TestMethod]
        public void PassClearQueueGetCountEqually0()
        {
            var queue = Queue.Queue<int>.GetQueue();
            queue.Clear();
            queue.Enqueue(2);
            queue.Enqueue(2);
            queue.Clear();

            Assert.AreEqual(queue.Count, 0);
        }
        [TestMethod]
        public void PassDequeueItemGetCountDecreasedBy1()
        {
            var queue = Queue.Queue<int>.GetQueue();
            queue.Clear();
            queue.Enqueue(2);
            queue.Dequeue();

            Assert.AreEqual(queue.Count, 0);
        }
        [TestMethod]
        public void PassDequeueItemGetThisItem()
        {
            var queue = Queue.Queue<int>.GetQueue();
            queue.Clear();
            queue.Enqueue(2);
            var result = queue.Dequeue();

            Assert.AreEqual(result, 2);
        }

        [TestMethod]
        public void PassPeekItemGetThisItem()
        {
            var queue = Queue.Queue<int>.GetQueue();
            queue.Clear();
            queue.Enqueue(5);
            var result = queue.Peek();

            Assert.AreEqual(result, 5);
        }

        [TestMethod]
        public void PassPeekItemGetCountNotChanged()
        {
            var queue = Queue.Queue<int>.GetQueue();
            queue.Clear();
            queue.Enqueue(5);
            queue.Peek();

            Assert.AreEqual(queue.Count, 1);
        }

        [TestMethod]
        public void PassToArrayGetArray()
        {
            var queue = Queue.Queue<int>.GetQueue();
            queue.Clear();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            var result = queue.ToArray();
            CollectionAssert.AreEqual(result, new int[3] { 1, 2, 3 });
        }

        [TestMethod]
        public void PassForeachGetArray()
        {
            var queue = Queue.Queue<int>.GetQueue();
            queue.Clear();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            int[] result = new int[3];
            int i = 0;
            foreach (int item in queue)
            {
                result[i] = item;
                i++;
            }

            CollectionAssert.AreEqual(result, new int[3] { 1, 2, 3 });
        }
    }
}
