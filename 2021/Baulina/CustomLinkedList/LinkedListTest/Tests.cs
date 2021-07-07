using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LinkedListBaulina;

namespace LinkedListTest
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void IsNullIsTrueForEmptyLinkedList()
        {
            var list = new LinkedList<int>();
            Assert.IsTrue(list.IsEmpty);
        }

        [TestMethod]
        public void RemoveVoidRemovesFirstAppropriateValue()
        {
            var list = new LinkedList<int>(new []{1,2,3,2,2});
            string expectedResult = "1322";
            var dataToBeRemoved = 2;
            list.Remove(dataToBeRemoved);
            var actual = new StringBuilder();
            foreach (var item in list)
            {
                actual.Append(item);
            }
            Assert.AreEqual(expectedResult, actual.ToString());
        }

        [TestMethod]
        public void RemoveDecreasesCount()
        {
            var list = new LinkedList<int>(new [] {0,1,2,3,4});
            var expectedCount = 4;
            var dataToBeRemoved = 2;
            list.Remove(dataToBeRemoved);
            Assert.AreEqual(expectedCount, list.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RemoveFromEmptyListThrowsException()
        {
            LinkedList<int> list = new LinkedList<int>();
            var dataToBeRemoved = 5;
            list.Remove(dataToBeRemoved);
        }

        [TestMethod]
        public void CountIsZeroAfterClear()
        {
            var list = new LinkedList<int>(new [] {0,1,2,3,4});
            var expectedCount = 0;
            list.Clear();
            Assert.AreEqual(expectedCount, list.Count);
        }

        [TestMethod]
        public void ContainsExistingDataReturnsTrue()
        {
            var list = new LinkedList<int>(new []{0,1,2,3,4});
            var sampleDataFromList = 1;
            Assert.IsTrue(list.Contains(sampleDataFromList));
        }

        [TestMethod]
        public void ContainsNonExistingDataReturnsFalse()
        {
            var list = new LinkedList<int>(new[] { 0, 1, 2, 3, 4 });
            var sampleDataNotFromList = 6;
            Assert.IsFalse(list.Contains(sampleDataNotFromList));
        }

        [TestMethod]
        public void AddInsertsDataToTheEndOfList()
        {
            var list = new LinkedList<int>(new[] { 0, 1, 2, 3, 4 });
            string expectedElementsOrder = "012345";
            var dataToBeAdded = 5;
            list.Add(dataToBeAdded);
            var actual = new StringBuilder();
            foreach (var item in list)
            {
                actual.Append(item);
            }
            Assert.AreEqual(expectedElementsOrder, actual.ToString());
        }

        [TestMethod]
        public void AddIncreasesCount()
        {
            var list = new LinkedList<int>(new[] { 0, 1, 2, 3, 4 });
            var expectedCount = 6;
            var dataToBeAdded = 5;
            list.Add(dataToBeAdded);
            Assert.AreEqual(expectedCount, list.Count);
        }

        [TestMethod]
        public void AppendFirstIncreasesCount()
        {
            var list = new LinkedList<int>(new[] { 0, 1, 2, 3, 4 });
            var expectedCount = 6;
            var dataToBeAdded = 5;
            list.AppendFirst(dataToBeAdded);
            Assert.AreEqual(expectedCount, list.Count);
        }

        [TestMethod]
        public void AppendFirstKeepsRemainingElementsInOrder()
        {
            var list = new LinkedList<int>(new[] { 0, 1, 2, 3, 4 });
            string expectedElementsOrder = "501234";
            var dataToBeAdded = 5;
            list.AppendFirst(dataToBeAdded);
            var actual = new StringBuilder();
            foreach (var item in list)
            {
                actual.Append(item);
            }
            Assert.AreEqual(expectedElementsOrder, actual.ToString());
        }

        [TestMethod]
        public void InsertIncreasesCount()
        {
            var list = new LinkedList<int>(new[] { 0, 1, 2, 3, 4 });
            var expectedCount = 6;
            var indexToInsertAt = 3;
            var dataToBeInsertedAtGivenIndex = 6;
            list.Insert(indexToInsertAt, dataToBeInsertedAtGivenIndex);
            Assert.AreEqual(expectedCount, list.Count);
        }

        [TestMethod]
        public void InsertDoesNotChangeOrderOfElementsBeforeAndAfterIndex()
        {
            var list = new LinkedList<int>(new[] { 0, 1, 2, 3, 4 });
            string expectedElementsOrder = "012634";
            var indexToInsertAt = 3;
            var dataToBeInsertedAtGivenIndex = 6;
            list.Insert(indexToInsertAt, dataToBeInsertedAtGivenIndex);
            var actual = new StringBuilder();
            foreach (var item in list)
            {
                actual.Append(item);
            }
            Assert.AreEqual(expectedElementsOrder, actual.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void InsertAtNegativeIndexThrowsException()
        {
            var list = new LinkedList<int>(new[] { 0, 1, 2, 3, 4 });
            var indexToInsertAt = -3;
            var dataToBeInsertedAtGivenIndex = 6;
            list.Insert(indexToInsertAt, dataToBeInsertedAtGivenIndex);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void InsertAtIndexGreaterThanOrEqualToCountThrowsException()
        {
            var list = new LinkedList<int>(new[] { 0, 1, 2, 3, 4 });
            var indexToInsertAt = list.Count;
            var dataToBeInsertedAtGivenIndex = 6;
            list.Insert(indexToInsertAt, dataToBeInsertedAtGivenIndex);
        }

        [TestMethod]
        public void InsertAtZeroIndexInEmptyListDoesNotThrowException()
        {
            var list = new LinkedList<int>();
            var indexToInsertAt = list.Count;
            var dataToBeInsertedAtGivenIndex = 6;
            list.Insert(indexToInsertAt, dataToBeInsertedAtGivenIndex);
        }
    }
}
