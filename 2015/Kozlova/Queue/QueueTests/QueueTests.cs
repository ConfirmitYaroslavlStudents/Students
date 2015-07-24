using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueueLibrary;

namespace QueueTests
{
    [TestClass]
    public abstract class QueueTestsBaseEmptyQueue
    {
        /// <summary>
        /// Override this method to implement the tests
        /// </summary>
        /// <returns></returns>
        protected abstract IQueue<int> GetQueueInstance();

        [TestMethod]
        public void Enqueue_NextIndexLessThanArrayLength()
        {
            // arrange
            IQueue<int> actual = GetQueueInstance();

            // act
            actual.Enqueue(5);

            // assert
            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual("5 ", actual.ToString());
        }

        [TestMethod]
        public void Dequeue_QueueIsEmpty_ShouldThrowExeption()
        {
            // arrange
            IQueue<int> actual = GetQueueInstance();

            // act
            try
            {
                actual.Dequeue();
            }
            catch (Exception e)
            {
                // assert
                StringAssert.Contains(e.Message, "Queue is empty!");
                return;
            }
            Assert.Fail("No exception was thrown.");
        }

        [TestMethod]
        public void Enumerator_EmptyQueue()
        {
            // arrange
            IQueue<int> testQueue = GetQueueInstance();
            string actual = null;

            // act
            foreach (int item in testQueue)
            {
                actual = item.ToString();
            }

            // assert
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void ToStringTest_QueueIsEmpty()
        {
            // arrange
            IQueue<int> actual = GetQueueInstance();
            
            // act
            string actualWord = actual.ToString();

            // assert
            Assert.AreEqual("", actualWord);
        }

        [TestMethod]
        public void CreateAnEmptyQueue()
        {
            // arrange
            IQueue<int> actual = GetQueueInstance();

            // assert
            Assert.AreEqual(0, actual.Count);
        }

        [TestMethod]
        public void PeekTest_EmptyQueue_ShouldThrowExeption()
        {
            // arrange
            IQueue<int> testQueue = GetQueueInstance();

            // act
            try
            {
                testQueue.Peek();
            }
            catch (Exception e)
            {
                // assert
                StringAssert.Contains(e.Message, "Queue is empty!");
                return;
            }
            Assert.Fail("No exception was thrown.");
        }
    }

    [TestClass]
    public abstract class QueueTestsBaseQueueIsNotEmpty
    {
        /// <summary>
        /// Override this method to implement the tests
        /// </summary>
        /// <returns></returns>
        protected abstract IQueue<int> GetQueueInstance();
        protected abstract IQueue<int> GetEmptyQueueInstance();
        protected abstract IQueue<int> GetDequeuedQueue();
            
            [TestMethod]
        public void Enqueue_NextIndexGreaterThanArrayLength_ResizeArray()
        {
            // arrange
            IQueue<int> actual = GetQueueInstance();

            // act
            actual.Enqueue(6);

            // assert
            Assert.AreEqual(6, actual.Count);
            Assert.AreEqual("1 2 3 4 5 6 ", actual.ToString());
        }

        [TestMethod]
        public void Dequeue_QueueIsNotEmpty()
        {
            // arrange
            IQueue<int> actual = GetQueueInstance();
            IQueue<int> expected = GetDequeuedQueue();
            int expectedItem = 1;
            
            // act
            int actualItem = actual.Dequeue();

            // assert
            Assert.AreEqual(4, actual.Count);
            Assert.AreEqual(expectedItem, actualItem);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Enumerator_QueueWithItems()
        {
            // arrange
            IQueue<int> testQueue = GetQueueInstance();
            string actual = null;

            // act
            foreach (int item in testQueue)
            {
                actual += item.ToString();
                actual += " ";
            }

            // assert
            Assert.IsNotNull(actual);
            Assert.AreEqual("1 2 3 4 5 ", actual);
        }

        [TestMethod]
        public void ToStringTest()
        {
            // arrange
            IQueue<int> actual = GetQueueInstance();
            string expectedWord = "1 2 3 4 5 ";

            // act
            string actualWord = actual.ToString();

            // assert
            Assert.AreEqual(expectedWord, actualWord);
        }

        [TestMethod]
        public void CreatQueueWithItems()
        {
            // arrange
            IQueue<int> actual = GetQueueInstance();

            // assert
            Assert.AreEqual(5, actual.Count);
            Assert.AreEqual("1 2 3 4 5 ", actual.ToString());
        }

        [TestMethod]
        public void PeekTest_NotEmptyQueue()
        {
            // arrange
            IQueue<int> testQueue = GetQueueInstance();
            int expected = 1;

            // act
            int actual = testQueue.Peek();

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ClearTest()
        {
            // arrange
            IQueue<int> actual = GetQueueInstance();
            IQueue<int> expected = GetEmptyQueueInstance();
            
            // act
            actual.Clear();

            // assert
            Assert.AreEqual(0, actual.Count);
            Assert.AreEqual(expected, actual);
        }
    }

    [TestClass]
    public class QueueTestsEmpty : QueueTestsBaseEmptyQueue
    {
        protected override IQueue<int> GetQueueInstance()
        {
            return new Queue<int>();
        }
    }

    [TestClass]
    public class QueueTestsNotEmpty : QueueTestsBaseQueueIsNotEmpty
    {
        protected override IQueue<int> GetQueueInstance()
        {
            return new Queue<int>(1, 2, 3, 4, 5);
        }

        protected override IQueue<int> GetEmptyQueueInstance()
        {
            return new Queue<int>();
        }

        protected override IQueue<int> GetDequeuedQueue()
        {
            return new Queue<int>(2, 3, 4, 5);
        }
    }

    [TestClass]
    public class QueueFromListTestsEmpty : QueueTestsBaseEmptyQueue
    {
        protected override IQueue<int> GetQueueInstance()
        {
            return new QueueFromList<int>();
        }
    }

    [TestClass]
    public class QueueFromListTestsNotEmpty : QueueTestsBaseQueueIsNotEmpty
    {
        protected override IQueue<int> GetQueueInstance()
        {
            return new QueueFromList<int>(1, 2, 3, 4, 5);
        }

        protected override IQueue<int> GetEmptyQueueInstance()
        {
            return new QueueFromList<int>();
        }

        protected override IQueue<int> GetDequeuedQueue()
        {
            return new QueueFromList<int>(2, 3, 4, 5);
        }
    }

    [TestClass]
    public class QueueTestsSpecialCases
    {
        [TestMethod]
        public void QueueFromListDequeue_QueueHasOneItem()
        {
            // arrange
            QueueFromList<string> actual = new QueueFromList<string>("cat");
            QueueFromList<string> expected = new QueueFromList<string>();
            const string expectedWord = "cat";

            // act
            string actualWord = actual.Dequeue();

            // assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expectedWord, actualWord);
        }

        [TestMethod]
        public void QueueEnqueueDequeue_CheckForCyclicArray()
        {
            // arrange and act
            QueueFromList<int> actual = new QueueFromList<int>(1, 2, 3, 4);
            QueueFromList<int> expected = new QueueFromList<int>(4, 5, 6, 7, 8);
            actual.Dequeue();
            actual.Dequeue();
            actual.Enqueue(5);
            actual.Enqueue(6);
            actual.Enqueue(7);
            actual.Dequeue();
            actual.Enqueue(8);
           
            // assert
            Assert.AreEqual("4 5 6 7 8 ", actual.ToString());
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(5, actual.Count);
        }
    
    }
}