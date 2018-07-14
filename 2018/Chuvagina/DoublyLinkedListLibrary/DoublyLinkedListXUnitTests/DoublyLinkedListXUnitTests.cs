using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;
using DoublyLinkedListLibrary;
using System.Collections.Generic;
using Xunit.Sdk;

namespace DoublyLinkedListXUnitTests
{

   
    public class DoublyLinkedListXUnitTests
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
        
        [Theory]
        //First element
        [InlineData(1)]
        // Middle Element
        [InlineData(3)]
        // Last Element
        [InlineData(5)]       
        public void DeleteAtIndex_TheRightElementIsDeleted(int value1)
        {
            DoublyLinkedList.DoublyLinkedList testList = new DoublyLinkedList.DoublyLinkedList();
            testList.AddToTail("1");
            testList.AddToTail("2");
            testList.AddToTail("3");
            testList.AddToTail("4");
            testList.AddToTail("5");
            List<string> expected = new List<string> {"1","2","3","4","5"};

            testList.DeleteAtIndex(value1);
            expected.RemoveAt(value1-1);
            
            List<string> actualNextLinks = CheckNextLinks(testList);
            List<string> actualPreviousLinks = CheckPreviousLinks(testList);
            CollectionAssert.AreEqual(actualNextLinks,expected);
            expected.Reverse();
            CollectionAssert.AreEqual(actualPreviousLinks, expected);

        }


        [Theory]
        //Zero index
        [InlineData(0)]
        // More than count
        [InlineData(6)]
        // Negative index
        [InlineData(-1)]
        public void DeleteAtIndex_IndexOutOfRangeException(int index)
        {
            DoublyLinkedList.DoublyLinkedList testList = new DoublyLinkedList.DoublyLinkedList();
            testList.AddToTail("1");
            testList.AddToTail("2");
            testList.AddToTail("3");
            testList.AddToTail("4");
            testList.AddToTail("5");
            
            Xunit.Assert.Throws<IndexOutOfRangeException>(
                () => testList.DeleteAtIndex(index));
        }


        [Fact]
        public void DeleteTheOnlyExistedElement_TheListIsEmpty()
        {
            List<string> expected = new List<string> { };
            DoublyLinkedList.DoublyLinkedList testList = new DoublyLinkedList.DoublyLinkedList();
            testList.AddToTail("1");

            testList.DeleteAtIndex(1);

            List<string> actual = CheckNextLinks(testList);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Fact]
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

            Xunit.Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetTheOnlyFirstElement_TheElementIsGotten()
        {
            string expected = "1";
            DoublyLinkedList.DoublyLinkedList testList = new DoublyLinkedList.DoublyLinkedList();
            testList.AddToTail("1");

            string actual = testList.FirstElement();

            Xunit.Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetTheFirstElementInEmptyList_OutOfRangeException()
        {
            DoublyLinkedList.DoublyLinkedList testList = new DoublyLinkedList.DoublyLinkedList();
            Xunit.Assert.Throws<IndexOutOfRangeException>(
            () => testList.FirstElement());

        }

        [Fact]
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

            Xunit.Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetTheLastElementInEmptyList_OutOfRangeException()
        {
            DoublyLinkedList.DoublyLinkedList testList = new DoublyLinkedList.DoublyLinkedList();
            Xunit.Assert.Throws<IndexOutOfRangeException>(
            () => testList.LastElement());

        }


    }
}
