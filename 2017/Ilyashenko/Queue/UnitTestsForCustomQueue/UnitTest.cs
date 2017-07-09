using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Queue;

namespace UnitTestsForCustomQueue
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestCountAfterCreation()
        {
            var queue = new CustomQueue<int>();
            Assert.AreEqual(0, queue.Count());
        }

        [TestMethod]
        public void TestCreationWithIncorrectCapacity()
        {
            var actualException = new Exception();
            var expectedException = new ArgumentOutOfRangeException();
            try
            {
                var queue = new CustomQueue<int>(-1);
            }
            catch (Exception exception)
            {
                actualException = exception;
            }
            Assert.AreEqual(expectedException.GetType(), actualException.GetType());
        }

        [TestMethod]
        public void TestCountAfterEnqueue()
        {
            var queue = new CustomQueue<int>();
            for (int i = 0; i < 10; i++)
                queue.Enqueue(i);
            Assert.AreEqual(10, queue.Count());
        }

        [TestMethod]
        public void TestCountAfterEnqueueAndDequeue()
        {
            var queue = new CustomQueue<int>();
            for (int i = 0; i < 10; i++)
                queue.Enqueue(i);
            for (int i = 0; i < 4; i++)
                queue.Dequeue();
            Assert.AreEqual(6, queue.Count());
        }

        [TestMethod]
        public void TestDequeueFromEmptyQueue()
        {
            var queue = new CustomQueue<int>();
            var actualException = new Exception();
            var expectedException = new InvalidOperationException();
            try
            {
                queue.Dequeue();
            }
            catch (Exception exception)
            {
                actualException = exception;
            }
            Assert.AreEqual(expectedException.GetType(), actualException.GetType());
        }

        [TestMethod]
        public void TestCountAfterClear()
        {
            var queue = new CustomQueue<int>();
            for (int i = 0; i < 10; i++)
                queue.Enqueue(i);
            queue.Clear();
            Assert.AreEqual(0, queue.Count());
        }

        [TestMethod]
        public void TestCountAfterCreationWithArray()
        {
            var testArray = new int[] {0, 3, 5, 7, 9};
            var queue = new CustomQueue<int>(testArray);
            Assert.AreEqual(testArray.Length, queue.Count());
        }

        [TestMethod]
        public void TestDequeueAfterCreationWithArray()
        {
            var testArray = new int[] { 0, 3, 5, 7, 9 };
            var queue = new CustomQueue<int>(testArray);
            Assert.AreEqual(testArray[0], queue.Dequeue());
        }

        [TestMethod]
        public void TestPeek()
        {
            var testArray = new int[] { 0, 3, 5, 7, 9 };
            var queue = new CustomQueue<int>(testArray);
            Assert.AreEqual(testArray[0], queue.Peek());
        }

        [TestMethod]
        public void TestQueueOnLargeDataAmount()
        {
            var queue = new CustomQueue<int>();
            var array = new int[10000];
            var arrayAsString = String.Empty;
            var queueAsString = String.Empty;

            for (int i = 0; i < 10000; i++)
            {
                queue.Enqueue(i);
                array[i] = i;
            }
            foreach (var item in array)
            {
                arrayAsString += item;
            }
            while (queue.Count() > 0)
            {
                queueAsString += queue.Dequeue();
            }
            Assert.AreEqual(arrayAsString, queueAsString);
        }

        [TestMethod]
        public void TestQueueToArray()
        {
            var queue = new CustomQueue<int>();
            var array = new int[] {0, 3, 5, 7, 9, 11};
            var arrayAsString = String.Empty;
            var queueAsString = String.Empty;

            foreach (var item in array)
                queue.Enqueue(item);
            var newArray = queue.ToArray();

            foreach (var item in array)
            {
                arrayAsString += item;
            }
            foreach (var item in newArray)
            {
                queueAsString += item;
            }
            Assert.AreEqual(arrayAsString, queueAsString);
        }
    }
}
