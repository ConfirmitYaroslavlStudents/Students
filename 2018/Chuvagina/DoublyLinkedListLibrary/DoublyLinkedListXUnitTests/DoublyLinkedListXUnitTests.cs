using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;
using System.Collections.Generic;
using Xunit.Sdk;

namespace DoublyLinkedListLibrary
{

   
    public class DoublyLinkedListXUnitTests
    {
        public List<string> Check(DoublyLinkedList<string> testList)
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
        public void Insert_TheRightElementIsAdded(int value)
        {
            List<string> expected = new List<string> { "1", "2", "3", "4", "5" };
            DoublyLinkedList<string> testList = new DoublyLinkedList<string>("1", "2", "3", "4", "5");

            testList.Insert(value,"0");
            expected.Insert(value - 1, "0");

            List<string> actualNextLinks = Check(testList);
            CollectionAssert.AreEqual(actualNextLinks, expected);

        }

        [Theory]
        //First element
        [InlineData(1)]
        // Middle Element
        [InlineData(3)]
        // Last Element
        [InlineData(5)]       
        public void DeleteAtIndex_TheRightElementIsDeleted(int value)
        {
            List<string> expected = new List<string> { "1", "2", "3", "4", "5" };
            DoublyLinkedList<string> testList = new DoublyLinkedList<string>("1", "2", "3", "4", "5");

            testList.DeleteAtIndex(value);
            expected.RemoveAt(value-1);
            
            List<string> actualNextLinks = Check(testList);
            CollectionAssert.AreEqual(actualNextLinks,expected);

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
            DoublyLinkedList<string> testList = new DoublyLinkedList<string>("1", "2", "3", "4", "5");

            Xunit.Assert.Throws<IndexOutOfRangeException>(
                () => testList.DeleteAtIndex(index));
        }


        [Fact]
        public void DeleteTheOnlyExistedElement_TheListIsEmpty()
        {
            List<string> expected = new List<string> { };
            DoublyLinkedList<string> testList = new DoublyLinkedList<string>();
            testList.AddToTail("1");

            testList.DeleteAtIndex(1);

            List<string> actual = Check(testList);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Fact]
        public void GetTheFirstElement_TheRightElementIsGotten()
        {
            string expected = "1";
            DoublyLinkedList<string> testList = new DoublyLinkedList<string>("1", "2", "3", "4", "5");

            string actual = testList.FirstElement();

            Xunit.Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetTheOnlyFirstElement_TheElementIsGotten()
        {
            string expected = "1";
            DoublyLinkedList<string> testList = new DoublyLinkedList<string>("1");

            string actual = testList.FirstElement();

            Xunit.Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetTheFirstElementInEmptyList_OutOfRangeException()
        {
            DoublyLinkedList<string> testList = new DoublyLinkedList<string>();
            Xunit.Assert.Throws<IndexOutOfRangeException>(
            () => testList.FirstElement());

        }

        [Fact]
        public void GetTheLastElement_TheRightElementIsGotten()
        {
            string expected = "5";
            DoublyLinkedList<string> testList = new DoublyLinkedList<string>("1", "2", "3", "4", "5");

            string actual = testList.LastElement();

            Xunit.Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetTheLastElementInEmptyList_OutOfRangeException()
        {
            DoublyLinkedList<string> testList = new DoublyLinkedList<string>();
            Xunit.Assert.Throws<IndexOutOfRangeException>(
            () => testList.LastElement());

        }


    }
}
