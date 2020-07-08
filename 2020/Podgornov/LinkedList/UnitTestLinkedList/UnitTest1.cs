using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Linked_List;
using System.Collections.Generic;

namespace UnitTestLinkedList
{
    [TestClass]
    public class LinkedListTest
    {
        
        [TestMethod]
        public void LinkedListConstructorTest()
        {
            var nums = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Linked_List.LinkedList<int> list = new Linked_List.LinkedList<int>(nums);
            CollectionAssert.AreEqual(nums, new List<int>(list));           
        }


        [TestMethod]
        public void AddFirstTest()
        {
            Linked_List.LinkedList<int> list = new Linked_List.LinkedList<int>(
                new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }
                );
            list.AddFirst(12);
            Assert.AreEqual(12, list.First.Value);

        }
        [TestMethod]
        public void AddLastTest()
        {
            Linked_List.LinkedList<int> list = new Linked_List.LinkedList<int>(
                new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }
                );
            list.AddLast(12);
            Assert.AreEqual(12, list.Last.Value);
        }


        [TestMethod]
        public void AddBeforeTest()
        {
            Linked_List.LinkedList<int> list = new Linked_List.LinkedList<int>(
                new int[] { 1, 2, 3,}
                );
            list.AddBefore(list.Find(1), 0);
            list.AddBefore(list.Find(2), -1);
            CollectionAssert.AreEqual(new int[] { 0, 1, -1, 2, 3 }, new List<int>(list));

        }
        [TestMethod]
        public void AddAfterTest()
        {
            Linked_List.LinkedList<int> list = new Linked_List.LinkedList<int>(
                new int[] { 1, 2, 3, }
                );
            list.AddAfter(list.Find(3), 4);
            list.AddAfter(list.Find(2), -1);
            CollectionAssert.AreEqual(new int[] { 1, 2, -1, 3, 4 }, new List<int>(list));

        }

        [TestMethod]
        public void ContainsTest()
        {
            Linked_List.LinkedList<int> list = new Linked_List.LinkedList<int>(
                new int[] { 1, 2, 3, }
                );
            Assert.IsTrue(list.Contains(2));
            Assert.IsFalse(list.Contains(-23));

        }

        [TestMethod]
        public void FindTest()
        {
            Linked_List.LinkedList<int> list = new Linked_List.LinkedList<int>(
                new int[] { 1, 2, 3, 4, 2, 5 }
                );
            Assert.IsNull(list.Find(42));
            Assert.IsTrue(list.Find(2).Next.Value == 3);
            Assert.IsTrue(list.FindLast(2).Next.Value == 5);
        }

        [TestMethod]
        public void RemoveTest()
        {
            Linked_List.LinkedList<int> list = new Linked_List.LinkedList<int>(
                new int[] { 1, 2, 3, 4, 2, 5 }
                );
            list.Remove(2);
            CollectionAssert.AreEqual(new int[] { 1, 3, 4, 2, 5 }, new List<int>(list));

            list.Remove(1);
            CollectionAssert.AreEqual(new int[] { 3, 4, 2, 5 }, new List<int>(list));
            Assert.AreEqual(3, list.First.Value);

            list.Remove(5);
            CollectionAssert.AreEqual(new int[] { 3, 4, 2 }, new List<int>(list));
            Assert.AreEqual(2, list.Last.Value);
        }
        [TestMethod]
        public void RemoveFirstOrLast()
        {
            Linked_List.LinkedList<int> list = new Linked_List.LinkedList<int>(
                new int[] { 1, 3, 4, 2, 5 }
                );
            list.RemoveFirst();
            CollectionAssert.AreEqual(new int[] { 3, 4, 2, 5 }, new List<int>(list));
            Assert.AreEqual(3, list.First.Value, "Remove First");

            list.RemoveLast();
            CollectionAssert.AreEqual(new int[] { 3, 4, 2 }, new List<int>(list));
            Assert.AreEqual(2, list.Last.Value , "Remove Last");

        }


    }
}
