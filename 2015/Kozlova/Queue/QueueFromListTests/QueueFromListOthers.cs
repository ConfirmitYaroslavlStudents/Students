using System;
using System.Text;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Queue;

namespace QueueFromListTests
{
    [TestClass]
    public class QueueFromListOthers
    {
        [TestMethod]
        public void Enumerator_EmptyQueue()
        {
            // arrange
            QueueFromList<int> testQueue = new QueueFromList<int>();
            string actual = null;

            // act
            foreach (var item in testQueue)
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
            QueueFromList<string> testQueue = new QueueFromList<string>("cat","dog","bird");
            string actual = null;

            // act
            foreach (var item in testQueue)
            {
                actual += item;
                actual += " ";
            }

            // assert
            Assert.IsNotNull(actual);
            Assert.AreEqual("cat dog bird ", actual);
        }

        [TestMethod]
        public void ToStringTest()
        {
            //arrange
            QueueFromList<int> actual = new QueueFromList<int>(2, 3, 4);
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
            QueueFromList<int> actual = new QueueFromList<int>();

            // assert
            Assert.AreEqual(0, actual.Count);
        }

        [TestMethod]
        public void CreatQueueWithItems()
        {
            // arrange and act
            QueueFromList<int> actual = new QueueFromList<int>(1, 2, 3, 4, 5);

            // assert
            Assert.AreEqual(5, actual.Count);
            Assert.AreEqual("1 2 3 4 5 ", actual.ToString());
        }

        [TestMethod]
        public void PeekTest()
        {
            // arrange
            QueueFromList<int> testQueue = new QueueFromList<int>(6, 1, 2, 3, 4);
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
            QueueFromList<int> actual = new QueueFromList<int>(6, 1, 2, 3, 4);
            QueueFromList<int> expected = new QueueFromList<int>();

            // act
            actual.Clear();

            // assert
            Assert.AreEqual(expected, actual);
        }
    }
}
