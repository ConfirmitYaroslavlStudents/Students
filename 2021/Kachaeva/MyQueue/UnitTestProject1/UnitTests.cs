using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyQueue;
using System;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void CountIsZeroAfterEmptyQueueCreation()
        {
            var queue = new MyQueue<int>();
            Assert.AreEqual(0, queue.Count);
        }

        [TestMethod]
        public void CollectionIsCorrectAfterQueueCreation()
        {
            var queue = new MyQueue<int>(new List<int>{ 0, 1, 2, 3, 4 });
            Assert.AreEqual(5, queue.Count);
            for (int i = 0; i < 5; i++)
                Assert.AreEqual(i, queue.Dequeue());
        }

        [TestMethod]
        public void ItemsExistAfterEnqueueing()
        {
            var queue = new MyQueue<int>();
            for (int i = 0; i < 10; i++)
                queue.Enqueue(i);
            Assert.AreEqual(10, queue.Count);
            for(int i=0;i<10;i++)
                Assert.AreEqual(queue.Dequeue(), i);
        }

        [TestMethod]
        public void CountDecreasesAfterDequeueing()
        {
            var queue = new MyQueue<int>();
            for (int i = 0; i < 10; i++)
            {
                queue.Enqueue(i);
            }
            queue.Dequeue();
            Assert.AreEqual(9, queue.Count);
        }

        [TestMethod]
        public void ItemsDoNotExistAfterDequeueing()
        {
            var queue = new MyQueue<int>();
            for (int i = 0; i < 10; i++) 
            {
                queue.Enqueue(i);
            }
            queue.Dequeue();
            queue.Dequeue();
            Assert.AreEqual(2, queue.Peek());
        }
        
        [TestMethod]
        public void ItemsAreInRightOrder()
        {
            var queue = new MyQueue<int>();
            for (int i = 0; i < 10; i++) 
            {
                queue.Enqueue(i);
            }
            queue.Dequeue();
            queue.Dequeue();
            queue.Enqueue(10);
            queue.Enqueue(11);
            for (int i = 2; i < 12; i++)
            {
                Assert.AreEqual(i, queue.Dequeue());
            }

        }

        [TestMethod]
        public void ItemsAreInRightOrderAfterCapacityIncrease()
        {
            var queue = new MyQueue<int>();
            for (int i = 0; i < 4; i++)
            {
                queue.Enqueue(i);
            }
            queue.Dequeue();
            queue.Dequeue();
            queue.Enqueue(4);
            queue.Enqueue(5);
            queue.Enqueue(6);
            for (int i = 2; i < 7; i++)
            {
                Assert.AreEqual(i, queue.Dequeue());
            }

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CapacityCanNotBeNegative()
        {
            var queue = new MyQueue<int>(-4);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CollectionCanNotBeNull()
        {
            int[] array = null;
            var queue = new MyQueue<int>(array);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CanNotDequeueFromEmptyQueue()
        {
            var queue = new MyQueue<int>();
            queue.Dequeue();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CanNotPeekFromEmptyQueue()
        {
            var queue = new MyQueue<int>();
            queue.Peek();
        }

    }
}
