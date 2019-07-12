using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DllLIb;

namespace DllTest
{
    [TestClass]
    public class Test
    {
        
        [TestMethod]
        public void AddLastTest()
        {
            DoubleLinkedList<int> dll = new DoubleLinkedList<int>();
            dll.AddLast(1);
            dll.AddLast(2);
            dll.AddLast(3);
            int[] ans = new int[] { 1,2,3 };
            int i = 0;
            foreach (var item in dll)
            {
                Assert.AreEqual(item, ans[i]);
                i++;
            }
        }
        [TestMethod]
        public void AddFirstTest()
        {
            DoubleLinkedList<int> dll = new DoubleLinkedList<int>();
            dll.AddFirst(1);
            dll.AddFirst(2);
            dll.AddFirst(3);
            int[] ans = new int[] { 3, 2, 1 };
            int i = 0;
            foreach (var item in dll)
            {
                Assert.AreEqual(item, ans[i]);
                i++;
            }
        }
        [TestMethod]
        public void DeleteTest()
        {
            DoubleLinkedList<int> dll = new DoubleLinkedList<int>();
            dll.AddLast(3);
            dll.AddLast(1);
            dll.AddLast(2);
            dll.DeleteFirst();
            dll.DeleteLast();
            int[] ans = new int[] { 1 };
            int i = 0;
            foreach (var item in dll)
            {
                Assert.AreEqual(item, ans[i]);
                i++;
            }
        }
        public void AddTest()
        {
            DoubleLinkedList<int> dll = new DoubleLinkedList<int>();
            dll.AddLast(1);
            dll.AddLast(2);
            dll.AddFirst(3);
            dll.AddLast(4);
            dll.AddFirst(5);
            int[] ans = new int[] { 5,3,1,2,4 };
            int i = 0;
            foreach (var item in dll)
            {
                Assert.AreEqual(item, ans[i]);
                i++;
            }
        }
        [TestMethod]
        public void RemoveAtTest()
        {
            DoubleLinkedList<int> dll = new DoubleLinkedList<int>();
            dll.AddLast(1);
            dll.AddLast(2);
            dll.AddLast(3);
            dll.RemoveAt(0);
            int[] ans = new int[] { 2,3};
            int i = 0;
            foreach (var item in dll)
            {
                Assert.AreEqual(item, ans[i]);
                i++;
            }
        }
        [TestMethod]
        public void RemoveTest()
        {
            DoubleLinkedList<int> dll = new DoubleLinkedList<int>();
            dll.AddLast(1);
            dll.AddLast(2);
            dll.AddLast(3);
            dll.Remove(2);
            int[] ans = new int[] { 1, 3 };
            int i = 0;
            foreach (var item in dll)
            {
                Assert.AreEqual(item, ans[i]);
                i++;
            }
        }
        [TestMethod]
        public void InsertTest()
        {
            DoubleLinkedList<int> dll = new DoubleLinkedList<int>();
            dll.Insert(0, 1);
            dll.Insert(1, 2);
            dll.Insert(2, 4);
            dll.Insert(2, 3);
            int[] ans = new int[] { 1, 2,3,4};
            int i = 0;
            foreach (var item in dll)
            {
                Assert.AreEqual(item, ans[i]);
                i++;
            }
        }
    }
}
