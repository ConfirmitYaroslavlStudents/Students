using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyQueue;
namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Enqueue_New_item()
        {
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Enqueue(4);

            int actual = queue.Dequeue();
            int expected = 1;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Enqueue_And_Dequeue()
        {
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Enqueue(4);
            queue.Dequeue();
            int actual = queue.Count();
            int expected = 3;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Empty_Queue()
        {
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(1);
            queue.Dequeue();
            
            int actual = queue.Dequeue();
        }
    }
}
