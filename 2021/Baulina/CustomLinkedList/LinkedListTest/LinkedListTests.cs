using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LinkedListBaulina;
using msLinkedList = System.Collections.Generic.LinkedList<int>;

namespace LinkedListTest
{
    [TestClass]
    public class LinkedListTests
    {
        [TestMethod]
        public void IsEmptyIsTrueForEmptyLinkedList()
        {
            var list = new LinkedList<int>();

            Assert.IsTrue(list.IsEmpty);
        }

        [TestMethod]
        public void RemoveRemovesTheFirstOccurrenceOfTheSpecifiedValue()
        {
            var list = new LinkedList<int>(new []{1,2,3,2,2});
            var expectedResult = new msLinkedList(new [] {1, 3, 2, 2});

            list.Remove(2);

            CollectionAssert.AreEqual(expectedResult, list);
        }

        [TestMethod]
        public void RemoveDecreasesCount()
        {
            var list = new LinkedList<int>(new [] {0,1,2,3,4});
            var expectedCount = 4;

            list.Remove(2);

            Assert.AreEqual(expectedCount, list.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RemoveFromEmptyListThrowsException()
        {
            LinkedList<int> list = new LinkedList<int>();

            list.Remove(5);
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
            var expectedResult = new msLinkedList(new []{0,1,2,3,4,5});

            list.Add(5);
            
            CollectionAssert.AreEqual(expectedResult, list);
        }

        [TestMethod]
        public void AddIncreasesCount()
        {
            var list = new LinkedList<int>(new[] { 0, 1, 2, 3, 4 });
            var expectedCount = 6;

            list.Add(5);

            Assert.AreEqual(expectedCount, list.Count);
        }

        [TestMethod]
        public void AppendFirstIncreasesCount()
        {
            var list = new LinkedList<int>(new[] { 0, 1, 2, 3, 4 });
            var expectedCount = 6;

            list.AppendFirst(5);

            Assert.AreEqual(expectedCount, list.Count);
        }

        [TestMethod]
        public void AppendFirstKeepsRemainingElementsInOrder()
        {
            var list = new LinkedList<int>(new[] { 0, 1, 2, 3, 4 });
            var expectedResult = new msLinkedList(new[] {5, 0, 1, 2, 3, 4});

            list.AppendFirst(5);

            CollectionAssert.AreEqual(expectedResult, list);
        }

        [TestMethod]
        public void InsertIncreasesCount()
        {
            var list = new LinkedList<int>(new[] { 0, 1, 2, 3, 4 });
            var expectedCount = 6;
            var indexToInsertAt = 3;

            list.Insert(indexToInsertAt, 6);

            Assert.AreEqual(expectedCount, list.Count);
        }

        [TestMethod]
        public void InsertDoesNotChangeOrderOfElementsBeforeAndAfterIndex()
        {
            var list = new LinkedList<int>(new[] { 0, 1, 2, 3, 4 });
            var expectedResult = new msLinkedList(new[] {0, 1, 2, 6, 3, 4});
            var indexToInsertAt = 3;

            list.Insert(indexToInsertAt, 6);

            CollectionAssert.AreEqual(expectedResult, list);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void InsertAtNegativeIndexThrowsException()
        {
            var list = new LinkedList<int>(new[] { 0, 1, 2, 3, 4 });
            var indexToInsertAt = -3;

            list.Insert(indexToInsertAt,6);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void InsertAtIndexGreaterThanOrEqualToCountThrowsException()
        {
            var list = new LinkedList<int>(new[] { 0, 1, 2, 3, 4 });
            var indexToInsertAt = list.Count;

            list.Insert(indexToInsertAt, 6);
        }

        [TestMethod]
        public void InsertAtZeroIndexInEmptyListDoesNotThrowException()
        {
            var list = new LinkedList<int>();
            var indexToInsertAt = list.Count;

            list.Insert(indexToInsertAt, 6);
        }
    }
}
