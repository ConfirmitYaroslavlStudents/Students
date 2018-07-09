using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LinkedListLibrary;

namespace LinkedListLibraryTests
{
    [TestClass]
    public class LinkedListTest
    {
        [TestMethod]
        public void AddLast3()
        {
            string expected = "1/2/3/";
            DoublyLinkedList testList = new DoublyLinkedList();
            testList.AddLast("1");
            testList.AddLast("2");
            testList.AddLast("3");
            string actual = testList.Show();
            Assert.AreEqual(expected,actual);

        }
        [TestMethod]
        public void AddFirst3()
        {
            string expected = "3/2/1/";
            DoublyLinkedList testList = new DoublyLinkedList();
            testList.AddFirst("1");
            testList.AddFirst("2");
            testList.AddFirst("3");
            string actual = testList.Show();
            Assert.AreEqual(expected, actual);

        }
        [TestMethod]
        public void AddFirst3Reverse()
        {
            string expected = "1/2/3/";
            DoublyLinkedList testList = new DoublyLinkedList();
            testList.AddFirst("1");
            testList.AddFirst("2");
            testList.AddFirst("3");
            string actual = testList.ReverseShow();
            Assert.AreEqual(expected, actual);

        }
        [TestMethod]
        public void DeleteIn5()
        {
            string expected = "1/2/4/5/";
            DoublyLinkedList testList = new DoublyLinkedList();
            testList.AddLast("1");
            testList.AddLast("2");
            testList.AddLast("3");
            testList.AddLast("4");
            testList.AddLast("5");
            testList.DeleteAtIndex(3);
            string actual = testList.Show();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void DeleteIn5Reverse()
        {
            string expected = "5/4/2/1/";
            DoublyLinkedList testList = new DoublyLinkedList();
            testList.AddLast("1");
            testList.AddLast("2");
            testList.AddLast("3");
            testList.AddLast("4");
            testList.AddLast("5");
            testList.DeleteAtIndex(3);
            string actual = testList.ReverseShow();
            Assert.AreEqual(expected, actual);

        }
        [TestMethod]
        public void DeleteIn1()
        {
            string expected = "The list is empty.";
            DoublyLinkedList testList = new DoublyLinkedList();
            testList.AddLast("1");
            testList.DeleteAtIndex(1);
            string actual = testList.Show();
            Assert.AreEqual(expected, actual);

        }
        [TestMethod]
        public void DeleteIn0()
        {
            string expected = "The list is empty.";
            DoublyLinkedList testList = new DoublyLinkedList();
            testList.DeleteAtIndex(1);
            string actual = testList.Show();
            Assert.AreEqual(expected, actual);

        }
        [TestMethod]
        public void AddAtIndex()
        {
            string expected = "1/2/3/4/5/";
            DoublyLinkedList testList = new DoublyLinkedList();
            testList.AddLast("1");
            testList.AddLast("2");
            testList.AddLast("4");
            testList.AddLast("5");
            testList.AddAtIndex("3",3);
            string actual = testList.Show();
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void AddAtWrongIndex()
        {
            string expected = "1/2/4/5/";
            DoublyLinkedList testList = new DoublyLinkedList();
            testList.AddLast("1");
            testList.AddLast("2");
            testList.AddLast("4");
            testList.AddLast("5");
            testList.AddAtIndex("3", 7);
            string actual = testList.Show();
            Assert.AreEqual(expected, actual);

        }
        [TestMethod]
        public void PopFirst()
        {
            string expected1 = "1";
            string expected2 = "2/3/4/5/";
            DoublyLinkedList testList = new DoublyLinkedList();
            testList.AddLast("1");
            testList.AddLast("2");
            testList.AddLast("3");
            testList.AddLast("4");
            testList.AddLast("5");
            object actual1 = testList.PopFirst();
            string actual2 = testList.Show();
            Assert.AreEqual(expected1, actual1.ToString());
            Assert.AreEqual(expected2, actual2);
        }
        [TestMethod]
        public void PopFirstReverse()
        {
            string expected1 = "1";
            string expected2 = "5/4/3/2/";
            DoublyLinkedList testList = new DoublyLinkedList();
            testList.AddLast("1");
            testList.AddLast("2");
            testList.AddLast("3");
            testList.AddLast("4");
            testList.AddLast("5");
            object actual1 = testList.PopFirst();
            string actual2 = testList.ReverseShow();
            Assert.AreEqual(expected1, actual1.ToString());
            Assert.AreEqual(expected2, actual2);
        }

        [TestMethod]
        public void PopFirstIn1()
        {
            string expected1 = "1";
            string expected2 = "The list is empty.";
            DoublyLinkedList testList = new DoublyLinkedList();
            testList.AddLast("1");
            object actual1=testList.PopFirst();         
            string actual2 = testList.Show();
            Assert.AreEqual(expected1, actual1.ToString());
            Assert.AreEqual(expected2, actual2);
        }

        [TestMethod]
        public void PopFirstIn0()
        {
            string expected1 = "There's nothing to pop.";
            string expected2 = "The list is empty.";
            DoublyLinkedList testList = new DoublyLinkedList();
            object actual1=testList.PopFirst();
            string actual2 = testList.Show();
            Assert.AreEqual(expected1, actual1.ToString());
            Assert.AreEqual(expected2, actual2);
        }
        [TestMethod]
        public void PopLast()
        {
            string expected1 = "5";
            string expected2 = "1/2/3/4/";
            DoublyLinkedList testList = new DoublyLinkedList();
            testList.AddLast("1");
            testList.AddLast("2");
            testList.AddLast("3");
            testList.AddLast("4");
            testList.AddLast("5");           
            object actual1 = testList.PopLast();
            string actual2 = testList.Show();
            Assert.AreEqual(expected1, actual1.ToString());
            Assert.AreEqual(expected2, actual2);
        }
        [TestMethod]
        public void PopLastReverse()
        {
            string expected1 = "5";
            string expected2 = "4/3/2/1/";
            DoublyLinkedList testList = new DoublyLinkedList();
            testList.AddLast("1");
            testList.AddLast("2");
            testList.AddLast("3");
            testList.AddLast("4");
            testList.AddLast("5");
            object actual1 = testList.PopLast();
            string actual2 = testList.ReverseShow();
            Assert.AreEqual(expected1, actual1.ToString());
            Assert.AreEqual(expected2, actual2);
        }

        [TestMethod]
        public void PopLastIn1()
        {
            string expected1 = "1";
            string expected2 = "The list is empty.";
            DoublyLinkedList testList = new DoublyLinkedList();
            testList.AddLast("1");
            object actual1 = testList.PopLast();
            string actual2 = testList.Show();
            Assert.AreEqual(expected1, actual1.ToString());
            Assert.AreEqual(expected2, actual2);
        }

        [TestMethod]
        public void PopLastIn0()
        {
            string expected1 = "There's nothing to pop.";
            string expected2 = "The list is empty.";
            DoublyLinkedList testList = new DoublyLinkedList();
            object actual1 = testList.PopLast();
            string actual2 = testList.Show();
            Assert.AreEqual(expected1, actual1.ToString());
            Assert.AreEqual(expected2, actual2);
        }
    }
}
