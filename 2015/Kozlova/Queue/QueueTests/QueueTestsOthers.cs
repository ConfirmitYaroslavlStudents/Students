using System;
using System.Text;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Queue;

namespace QueueTests
{
    [TestClass]
    public class QueueTestsOthers
    {
        [TestMethod]
        public void Enumerator_EmptyQueue()
        {
            // arrange
            Queue<int> testQueue = new Queue<int>();
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
        public void Enumerator_QueueWithItems()
        {
            // arrange
            Queue<int> testQueue = new Queue<int>(1,2,3);
            string actual = null;

            // act
            foreach (int item in testQueue)
            {
                actual += item.ToString();
                actual += " ";
            }

            // assert
            Assert.IsNotNull(actual);
            Assert.AreEqual("1 2 3 ", actual);
        }

        [TestMethod]
        public void ToStringTest()
        {
            //arrange
            Queue<int> actual = new Queue<int>(2,3,4);
            string expectedWord = "2 3 4 ";

            // act
            string actualWord = actual.ToString();

            // assert
            Assert.AreEqual(expectedWord, actualWord);

        }

        [TestMethod]
        public void CreateAnEmptyQueue()
        {
            // arrange and act
            Queue<int> actual = new Queue<int>();
            
            // assert
            Assert.AreEqual(0, actual.Count);
            Assert.AreEqual(5, actual.Items.Length);
        }

        [TestMethod]
        public void CreatQueueWithItems()
        {
            // arrange and act
            Queue<int> actual = new Queue<int>(1,2,3,4,5,6);

            // assert
            Assert.AreEqual(6, actual.Count);
            Assert.AreEqual("1 2 3 4 5 6 ", actual.ToString());
            Assert.AreEqual(10, actual.Items.Length);
        }

        [TestMethod]
        public void PeekTest()
        {
            // arrange
            Queue<int> testQueue = new Queue<int>(6,1,2,3,4);
            int expected = 6;

            // act
            int actual = testQueue.Peek();

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ClearTest()
        {
            // arrange
            Queue<int> actual = new Queue<int>(6, 1, 2, 3, 4);
            Queue<int> expected = new Queue<int>();

            // act
           actual.Clear();

            // assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(0, actual.Count);
        }
    }
}
