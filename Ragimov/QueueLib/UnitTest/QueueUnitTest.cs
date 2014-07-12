using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueueLib;


namespace UnitTest
{
    [TestClass]
    public class QueueUnitTest
    {
        [TestMethod]
        public void Queue_EnqueuedDequeued()
        {
            var queue = new Queue<int>();
            queue.Enqueue(42);
            Assert.AreEqual(42, queue.Dequeue());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Queue_DeletedFromEmpty_ShouldThrowInvalidOperation()
        {
            var queue = new Queue<int>();
            queue.Dequeue();
        }

        [TestMethod]
        public void Queue_CountIsProperlyChanged()
        {
            var queue = new Queue<int>();
            queue.Enqueue(42);
            queue.Enqueue(37);
            queue.Enqueue(queue.Dequeue());
            Assert.AreEqual(2, queue.Count);
        }

        [TestMethod]
        public void Queue_MultipleItemsStored()
        {
            var queue = new Queue<int>();
            queue.Enqueue(42);
            queue.Enqueue(37);
            queue.Enqueue(100500);
            Assert.AreEqual(42, queue.Dequeue());
            Assert.AreEqual(37, queue.Dequeue());
            Assert.AreEqual(100500, queue.Dequeue());
        }

        [TestMethod]
        public void Queue_Cleared()
        {
            var queue = new Queue<int>();
            queue.Enqueue(42);
            queue.Enqueue(37);
            queue.Enqueue(100500);
            queue.Clear();
            Assert.AreEqual(0,queue.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Queue_PeekedFromClear_ShouldThrowInvalidOperation()
        {
            var queue = new Queue<int>();
            queue.Enqueue(42);
            queue.Enqueue(37);
            queue.Enqueue(100500);
            queue.Clear();
            queue.Peek();
        }

        [TestMethod]
        public void Queue_PeekedAndDequeuedAreEqual()
        {
            var queue = new Queue<int>();
            queue.Enqueue(42);
            queue.Enqueue(37);
            queue.Enqueue(100500);
            var temp = queue.Peek();
            Assert.AreEqual(temp,queue.Dequeue());
        }

        [TestMethod]
        public void Queue_IEnumerableImplemented()
        {
            var queue = new Queue<int>();
            int[] array = {42, 37, 100500};
            foreach (var t in array)
            {
                queue.Enqueue(t);
            }
            var i = 0;
            foreach (var temp in queue)
            {
                Assert.AreEqual(temp,array[i]);
                i++;
            }
        }

    }
}
