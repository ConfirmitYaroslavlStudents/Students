using Microsoft.VisualStudio.TestTools.UnitTesting;
using Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Tests
{
    [TestClass()]
    public class MyQueueTests
    {
        [TestMethod()]
        public void EnqueueTest()
        {
            MyQueue<int> actualInts = new MyQueue<int>();
            for (int i = 1; i <= 5; i++)
            {
                actualInts.Enqueue(i);
            }

            string actual = "";
            foreach (var item in actualInts)
            {
                actual += item.ToString() + " ";
            }

            Assert.AreEqual(5, actualInts.Count);
            Assert.AreEqual("1 2 3 4 5 ", actual);
        }

        [TestMethod()]
        public void DequeueTest()
        {
            MyQueue<int> actualInts = new MyQueue<int>();
            for (int i = 1; i <= 5; i++)
            {
                actualInts.Enqueue(i);
            }

            int actual = actualInts.Dequeue();

            Assert.AreEqual(4, actualInts.Count);
            Assert.AreEqual(1, actual);
        }

        [TestMethod()]
        public void ClearTest()
        {
            MyQueue<int> actualInts = new MyQueue<int>();
            for (int i = 1; i <= 5; i++)
            {
                actualInts.Enqueue(i);
            }

            actualInts.Clear();

            Assert.AreEqual(0, actualInts.Count);
        }

        [TestMethod()]
        public void PeekTest()
        {
            MyQueue<int> actualInts = new MyQueue<int>();
            for (int i = 1; i <= 5; i++)
            {
                actualInts.Enqueue(i);
            }

            int actual = actualInts.Peek();

            Assert.AreEqual(5, actualInts.Count);
            Assert.AreEqual(1, actual);
        }

        [TestMethod()]
        public void ContainsTest()
        {
            MyQueue<int> actualInts = new MyQueue<int>();
            actualInts.Enqueue(5);

            bool actual = actualInts.Contains(5);

            Assert.AreEqual(true, actual);
        }
    }
}