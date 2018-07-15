using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DoublyLinkedListLibrary
{
    [TestClass]
    public class DoublyLinkedListTest
    {
        public List<string> CheckPreviousLinks(DoublyLinkedList<string> testList)
        {
            List<string> actual = new List<string>();
            foreach (string item in testList.GetList(true))
            {
                actual.Add(item);
            }

            return actual;
        }

        public List<string> CheckNextLinks(DoublyLinkedList<string> testList)
        {
            List<string> actual = new List<string>();

            foreach (string item in testList.GetList(false))
            {
                actual.Add(item);
            }

            return actual;
        }

        [TestMethod]
        public void AddElementsToTail_ElementsAreAddedInRightOrder_FromHead()
        {
            List<string> expected = new List<string> { "1", "2", "3" };

            DoublyLinkedList<string> testList = new DoublyLinkedList<string>();

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

            DoublyLinkedList<string> testList = new DoublyLinkedList<string>();

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
            DoublyLinkedList<string> testList = new DoublyLinkedList<string>();

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
            DoublyLinkedList<string> testList = new DoublyLinkedList<string>();

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
            DoublyLinkedList<string> testList = new DoublyLinkedList<string>();
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
            DoublyLinkedList<string> testList = new DoublyLinkedList<string>();
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
            DoublyLinkedList<string> testList = new DoublyLinkedList<string>();
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
            DoublyLinkedList<string> testList = new DoublyLinkedList<string>();
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
            DoublyLinkedList<string> testList = new DoublyLinkedList<string>();
            testList.AddToTail("1");

            testList.DeleteAtIndex(1);

            List<string> actual = CheckNextLinks(testList);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DeleteTheOnlyExistedElement_TheListIsEmpty_FromTail()
        {
            List<string> expected = new List<string> { };
            DoublyLinkedList<string> testList = new DoublyLinkedList<string>();
            testList.AddToTail("1");

            testList.DeleteAtIndex(1);

            List<string> actual = CheckPreviousLinks(testList);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DeleteInEmptyList_IndexOutOfRangeExeption()
        {
            DoublyLinkedList<string> testList = new DoublyLinkedList<string>();             
            Assert.ThrowsException<IndexOutOfRangeException>(
                  () => testList.DeleteAtIndex(1));
           
        }
            
        

        [TestMethod]
        public void AddOneElementToTheMiddle_ElementsAreInRightOrder_FromHead()
        {
            List<string> expected = new List<string> { "1", "2", "3", "4", "5" };
            DoublyLinkedList<string> testList = new DoublyLinkedList<string>();
            testList.AddToTail("1");
            testList.AddToTail("2");
            testList.AddToTail("4");
            testList.AddToTail("5");

            testList.Insert( 3,"3");

            List<string> actual = CheckNextLinks(testList);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AddOneElementToTheMiddle_ElementsAreInRightOrder_FromTail()
        {
            List<string> expected = new List<string> { "5", "4", "3", "2", "1" };
            DoublyLinkedList<string> testList = new DoublyLinkedList<string>();
            testList.AddToTail("1");
            testList.AddToTail("2");
            testList.AddToTail("4");
            testList.AddToTail("5");

            testList.Insert( 3,"3");

            List<string> actual = CheckPreviousLinks(testList);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AddAtWrongIndex_IndexOutOfRangeException()
        {
            DoublyLinkedList<string> testList = new DoublyLinkedList<string>();
            testList.AddToTail("1");
            testList.AddToTail("2");
            testList.AddToTail("4");
            testList.AddToTail("5");

            Assert.ThrowsException<IndexOutOfRangeException>(
      () => testList.Insert( 7,"3"));          
        }

        [TestMethod]
        public void GetTheFirstElement_TheRightElementIsGotten()
        {
            string expected = "1";
            DoublyLinkedList<string> testList = new DoublyLinkedList<string>();
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
            DoublyLinkedList<string> testList = new DoublyLinkedList<string>();
            testList.AddToTail("1");

            string actual = testList.FirstElement();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetTheFirstElementInEmptyList_OutOfRangeException()
        {
            DoublyLinkedList<string> testList = new DoublyLinkedList<string>();
            Assert.ThrowsException<IndexOutOfRangeException>(
            () =>  testList.FirstElement());
          
        }

        [TestMethod]
        public void GetTheLastElement_TheRightElementIsGotten()
        {
            string expected = "5";
            DoublyLinkedList<string> testList = new DoublyLinkedList<string>();
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
            DoublyLinkedList<string> testList = new DoublyLinkedList<string>();
            Assert.ThrowsException<IndexOutOfRangeException>(
            () => testList.LastElement());
            
        }

    }
}
