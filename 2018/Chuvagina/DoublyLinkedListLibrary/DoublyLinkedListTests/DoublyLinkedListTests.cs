using System;
using System.Collections.Generic;
using DoublyLinkedListLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DoublyLinkedListTests
{
    [TestClass]
    public class DoublyLinkedListTest
    {
        public List<string> CheckPreviousLinks(DoublyLinkedList.DoublyLinkedList testList)
        {
            List<string> actual = new List<string>();
            Node item = testList.Tail;

            while (item != null)
            {
                actual.Add(item.Value);
                item = item.Previous;
            }

            return actual;
        }

        public List<string> CheckNextLinks(DoublyLinkedList.DoublyLinkedList testList)
        {
            List<string> actual = new List<string>();

            foreach (string item in testList)
            {
                actual.Add(item);
            }

            return actual;
        }

        [TestMethod]
        public void AddElementsToTail_ElementsAreAddedInRightOrder_FromHead()
        {
            List<string> expected = new List<string> { "1", "2", "3" };

            DoublyLinkedList.DoublyLinkedList testList = new DoublyLinkedList.DoublyLinkedList();

            testList.AddToTail("1");
            testList.AddToTail("2");
            testList.AddToTail("3");

            List<string> actual = CheckNextLinks(testList);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AddElementsToTail_ElementsAreAddedInRightOrder_FromTail()
        {
            List<string> expected = new List<string> { "3", "2", "1" };

            DoublyLinkedList.DoublyLinkedList testList = new DoublyLinkedList.DoublyLinkedList();

            testList.AddToTail("1");
            testList.AddToTail("2");
            testList.AddToTail("3");

            List<string> actual = CheckPreviousLinks(testList);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AddElementsToHead_ElementsAreAddedInRightOrder_FromHead()
        {
            List<string> expected = new List<string> { "3", "2", "1" };
            DoublyLinkedList.DoublyLinkedList testList = new DoublyLinkedList.DoublyLinkedList();

            testList.AddToHead("1");
            testList.AddToHead("2");
            testList.AddToHead("3");

            List<string> actual = CheckNextLinks(testList);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AddElementsToHead_ElementsAreAddedInRightOrder_FromTail()
        {
            List<string> expected = new List<string> { "1", "2", "3" };
            DoublyLinkedList.DoublyLinkedList testList = new DoublyLinkedList.DoublyLinkedList();

            testList.AddToHead("1");
            testList.AddToHead("2");
            testList.AddToHead("3");

            List<string> actual = CheckPreviousLinks(testList);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DeleteOneMiddleElement_ElementIsDeletedCorrectly_FromHead()
        {
            List<string> expected = new List<string> { "1", "2", "4", "5" };
            DoublyLinkedList.DoublyLinkedList testList = new DoublyLinkedList.DoublyLinkedList();
            testList.AddToTail("1");
            testList.AddToTail("2");
            testList.AddToTail("3");
            testList.AddToTail("4");
            testList.AddToTail("5");

            testList.DeleteAtIndex(3);

            List<string> actual = CheckNextLinks(testList);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DeleteOneMiddleElement_ElementIsDeletedCorrectly_FromTail()
        {
            List<string> expected = new List<string> { "5", "4", "2", "1" };
            DoublyLinkedList.DoublyLinkedList testList = new DoublyLinkedList.DoublyLinkedList();
            testList.AddToTail("1");
            testList.AddToTail("2");
            testList.AddToTail("3");
            testList.AddToTail("4");
            testList.AddToTail("5");

            testList.DeleteAtIndex(3);

            List<string> actual = CheckPreviousLinks(testList);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DeleteOneMiddleAndOneLastElements_ElementsAreDeletedCorrectly_FromHead()
        {
            List<string> expected = new List<string> { "1", "2", "4" };
            DoublyLinkedList.DoublyLinkedList testList = new DoublyLinkedList.DoublyLinkedList();
            testList.AddToTail("1");
            testList.AddToTail("2");
            testList.AddToTail("3");
            testList.AddToTail("4");
            testList.AddToTail("5");

            testList.DeleteAtIndex(3);
            testList.DeleteAtIndex(4);

            List<string> actual = CheckNextLinks(testList);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DeleteOneMiddleAndOneLastElements_ElementsAreDeletedCorrectly_FromTail()
        {
            List<string> expected = new List<string> { "4", "2", "1" };
            DoublyLinkedList.DoublyLinkedList testList = new DoublyLinkedList.DoublyLinkedList();
            testList.AddToTail("1");
            testList.AddToTail("2");
            testList.AddToTail("3");
            testList.AddToTail("4");
            testList.AddToTail("5");

            testList.DeleteAtIndex(3);
            testList.DeleteAtIndex(4);

            List<string> actual = CheckPreviousLinks(testList);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DeleteTheOnlyExistedElement_TheListIsEmpty_FromHead()
        {
            List<string> expected = new List<string> { };
            DoublyLinkedList.DoublyLinkedList testList = new DoublyLinkedList.DoublyLinkedList();
            testList.AddToTail("1");

            testList.DeleteAtIndex(1);

            List<string> actual = CheckNextLinks(testList);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DeleteTheOnlyExistedElement_TheListIsEmpty_FromTail()
        {
            List<string> expected = new List<string> { };
            DoublyLinkedList.DoublyLinkedList testList = new DoublyLinkedList.DoublyLinkedList();
            testList.AddToTail("1");

            testList.DeleteAtIndex(1);

            List<string> actual = CheckPreviousLinks(testList);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DeleteInEmptyList_IndexOutOfRangeExeption()
        {
            DoublyLinkedList.DoublyLinkedList testList = new DoublyLinkedList.DoublyLinkedList();
            try
            {
                testList.DeleteAtIndex(1);
            }
            catch (IndexOutOfRangeException)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void AddOneElementToTheMiddle_ElementsAreInRightOrder_FromHead()
        {
            List<string> expected = new List<string> { "1", "2", "3", "4", "5" };
            DoublyLinkedList.DoublyLinkedList testList = new DoublyLinkedList.DoublyLinkedList();
            testList.AddToTail("1");
            testList.AddToTail("2");
            testList.AddToTail("4");
            testList.AddToTail("5");

            testList.AddAtIndex("3", 3);

            List<string> actual = CheckNextLinks(testList);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AddOneElementToTheMiddle_ElementsAreInRightOrder_FromTail()
        {
            List<string> expected = new List<string> { "5", "4", "3", "2", "1" };
            DoublyLinkedList.DoublyLinkedList testList = new DoublyLinkedList.DoublyLinkedList();
            testList.AddToTail("1");
            testList.AddToTail("2");
            testList.AddToTail("4");
            testList.AddToTail("5");

            testList.AddAtIndex("3", 3);

            List<string> actual = CheckPreviousLinks(testList);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AddAtWrongIndex_IndexOutOfRangeException()
        {
            DoublyLinkedList.DoublyLinkedList testList = new DoublyLinkedList.DoublyLinkedList();
            testList.AddToTail("1");
            testList.AddToTail("2");
            testList.AddToTail("4");
            testList.AddToTail("5");

            try
            {
                testList.AddAtIndex("3", 7);
            }
            catch (IndexOutOfRangeException)
            {
                Assert.IsTrue(true);
            }


        }
        [TestMethod]
        public void GetTheFirstElement_TheRightElementIsGotten()
        {
            string expected = "1";
            DoublyLinkedList.DoublyLinkedList testList = new DoublyLinkedList.DoublyLinkedList();
            testList.AddToTail("1");
            testList.AddToTail("2");
            testList.AddToTail("3");
            testList.AddToTail("4");
            testList.AddToTail("5");

            string actual = testList.FirstElement();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetTheOnlyFirstElement_TheElementIsGotten()
        {
            string expected = "1";
            DoublyLinkedList.DoublyLinkedList testList = new DoublyLinkedList.DoublyLinkedList();
            testList.AddToTail("1");

            string actual = testList.FirstElement();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetTheFirstElementInEmptyList_OutOfRangeException()
        {
            DoublyLinkedList.DoublyLinkedList testList = new DoublyLinkedList.DoublyLinkedList();
            try
            {
                string testElement = testList.FirstElement();
            }
            catch (IndexOutOfRangeException)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void GetTheLastElement_TheRightElementIsGotten()
        {
            string expected = "5";
            DoublyLinkedList.DoublyLinkedList testList = new DoublyLinkedList.DoublyLinkedList();
            testList.AddToTail("1");
            testList.AddToTail("2");
            testList.AddToTail("3");
            testList.AddToTail("4");
            testList.AddToTail("5");

            string actual = testList.LastElement();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetTheLastElementInEmptyList_OutOfRangeException()
        {
            DoublyLinkedList.DoublyLinkedList testList = new DoublyLinkedList.DoublyLinkedList();
            try
            {
                string testElement = testList.LastElement();
            }
            catch (IndexOutOfRangeException)
            {
                Assert.IsTrue(true);
            }
        }

    }
}
