using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkedListTest
{
    [TestClass]
    public class LinkedListTestClass
    {
        [TestMethod]
        public void CountShouldIncAfterAdding()
        {
            var testList = new Homework1.LinkedList<int>();
            var countExpected = 1;
            testList.Add(0);
            var countActual = testList.Count;
            Assert.AreEqual(countExpected, countActual);
        }

        [TestMethod]
        public void CountShouldDecAfterRemoving()
        {
            var testList = new Homework1.LinkedList<int>(0);
            var countExpected = 1;
            var countActual = testList.Count;
            Assert.AreEqual(countExpected, countActual);
            testList.RemoveAt(0);
            countExpected--;
            countActual = testList.Count;
            Assert.AreEqual(countExpected, countActual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RangeAddedCorectly()
        {
            var range = new int[] { 0, 1, 2, 3, 4 };
            var testList = new Homework1.LinkedList<int>();
            ArgumentNullException expectedException=null;
            testList.AddRange(range);
            for (int i = 0; i < range.Length; i++)
            {
                Assert.AreEqual(range[i], testList.ElementAt(i));
            }
            testList.AddRange(null);
        }

        [TestMethod]
        public void ClearWorkCorectly()
        {
            var range = new int[] { 0, 1, 2, 3, 4 };
            var testList = new Homework1.LinkedList<int>(range);
            testList.Clear();
            Assert.AreEqual(0,testList.Count);
            Assert.AreEqual(true,testList.IsEmpty);
            Assert.AreEqual(false,testList.Contains(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ElementInsertedCorectly()
        {
            var range = new int[] { 0, 1, 2, 3, 5,6 };
            var expectedRange = new int[] { -1, 0, 1, 2, 3, 4, 5, 6,7 };
            var testList = new Homework1.LinkedList<int>(range);
            testList.InsertAt(0, -1);
            testList.InsertAt(5, 4);
            testList.InsertAt(testList.Count, 7);
            for (int i = 0; i < expectedRange.Length; i++)
            {
                Assert.AreEqual(expectedRange[i], testList.ElementAt(i));
            }
            testList.InsertAt(10, 8);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RangeInsertedCorectly()
        {
            var range = new int[] { 0, 1, 2, 6, 7, 8 };
            var firstRange = new int[] { -4,-3,-2,-1 };
            var secondRange = new int[] { 3,4,5 };
            var thirdRange = new int[] { 9,10,11 };
            var expectedRange = new int[] { -4,-3,-2,-1, 0, 1, 2, 3, 4, 5, 6, 7,8,9,10,11 };
            var testList = new Homework1.LinkedList<int>(range);
            testList.InsertRange(3,secondRange);
            testList.InsertRange(0, firstRange);
            testList.InsertRange(testList.Count, thirdRange);
            for (int i = 0; i < expectedRange.Length; i++)
            {
                Assert.AreEqual(expectedRange[i], testList.ElementAt(i));
            }
            testList.InsertRange(0, null);
          

        }
        [TestMethod]
        public void RemovingElementByIndexCorectly()
        {
            var expectedRange = new int[] { 0, 1, 2, 3, 5 };
            var range = new int[] { -1, 0, 1, 2, 3, 4, 5, 6 };
            var testList = new Homework1.LinkedList<int>(range);
            testList.RemoveAt(0);
            testList.RemoveAt(6);
            testList.RemoveAt(4);
            for (int i = 0; i < expectedRange.Length; i++)
            {
                Assert.AreEqual(expectedRange[i], testList.ElementAt(i));
            }

        }
        [TestMethod]
        public void RemovingElementByValueCorectly()
        {
            var expectedRange = new int[] { 0, 1, 2, 3, 5 ,6};
            var range = new int[] { -1, 0, 1, 2, 3, 4, 5, 6 ,7};
            var testList = new Homework1.LinkedList<int>(range);
            testList.Remove(-1);
            testList.Remove(7);
            testList.Remove(4);
            for (int i = 0; i < expectedRange.Length; i++)
            {
                Assert.AreEqual(expectedRange[i], testList.ElementAt(i));
            }

        }
        [TestMethod]
        public void MethodsWorkCorectlyWhithEmptyList()
        {
            var testList = new Homework1.LinkedList<int>();
            Assert.AreEqual(0, testList.Count);
            Assert.AreEqual(true, testList.IsEmpty);
            List<Exception> expectedExceptions = new List<Exception>();
            try
            {
                testList.RemoveAt(0);
            }
            catch (ArgumentOutOfRangeException e)
            {
                expectedExceptions.Add(e);
            }
            try
            {
                testList.ElementAt(0);
            }
            catch (ArgumentOutOfRangeException e)
            {
                expectedExceptions.Add(e);
            }
            Assert.AreEqual(false, testList.Contains(0));
            testList.InsertAt(0, 0);
            Assert.AreEqual(expectedExceptions.Count, 2);
            Assert.AreEqual(1, testList.Count);
            Assert.AreEqual(false, testList.IsEmpty);
        }

        [TestMethod]
        public void EmptyLinkedListEnumeratorTest()
        {
            var testList = new Homework1.LinkedList<int>();
            var expectedIterationCount = 0;
            var actualIterationCount = 0;
            foreach (var item in testList)
            {
                actualIterationCount++;
            }
            Assert.AreEqual(expectedIterationCount, actualIterationCount);
        }
        [TestMethod]
        public void LinkedListEnumeratorTest()
        {
            var range = new int[] { 0, 1, 2, 3, 4 };
            var testList = new Homework1.LinkedList<int>(range);
            var expectedIterationCount = range.Length;
            var actualIterationCount = 0;
            foreach (var item in testList)
            {
                actualIterationCount++;
            }
            Assert.AreEqual(expectedIterationCount, actualIterationCount);
        }
    }
}
