using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyQueue;
namespace MyQueueTestProject
{
    [TestClass]
    public class MyQueueTests
    {
        [TestMethod]
        public void EnqueueFewElement_CheckCorrectCount ()
        {
            const int CountEnqueue= 4;
            Queue<int> queue = new Queue<int>();

            for(int i=1;i<= CountEnqueue; i++)
            queue.Enqueue(i);

            int actual = queue.Count;
            int expected = CountEnqueue;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EnqueueFourElementDequeueOne_CheckFirstNumber()
        {
            int ControlNumber = 5;
            Queue<int> queue = new Queue<int>();

            queue.Enqueue(84324);
            queue.Enqueue(ControlNumber);
            queue.Enqueue(828);
            queue.Enqueue(82);

            queue.Dequeue();

            int actual = queue.Dequeue();
            int expected = ControlNumber;

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
