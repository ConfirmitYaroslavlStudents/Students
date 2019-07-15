using System;
using DaraStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTestsForBST
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AddTest()
        {
            BinarySearchTree<int,int> tree = new BinarySearchTree<int, int>(8, 1);
            tree.Insert(3, 1);
            tree.Insert(10, 1);
            tree.Insert(1, 1);
            tree.Insert(6, 1);
            tree.Insert(14, 1);
            tree.Insert(4, 1);
            tree.Insert(7, 1);
            tree.Insert(13, 1);
            Node<int, int> root = tree.Root;
            int[] ans = new int[] { 8, 3, 10, 1, 6, 14, 4, 7, 13 };
            int n = tree.Count;
            Queue<Node<int, int>> queue = new Queue<Node<int, int>>();
            queue.Enqueue(root); int i = 0;
            while (queue.Count != 0)
            {
                Node<int, int> curr = queue.Dequeue();
                Assert.AreEqual(curr.Key, ans[i]);
                i++;
                if (!(curr.Left == null))
                    queue.Enqueue(curr.Left);
                if (!(curr.Right == null))
                    queue.Enqueue(curr.Right);
            }
        }
        [TestMethod]
        public void InitTest()
        {
            BinarySearchTree<int, int> tree = new BinarySearchTree<int, int>(1, 1);
            Assert.AreEqual(tree.Count, 1);
        }
        [TestMethod]
        public void CountAfterInsertTest()
        {
            BinarySearchTree<int, int> tree = new BinarySearchTree<int, int>(8, 1);
            tree.Insert(3, 1);
            tree.Insert(10, 1);
            tree.Insert(1, 1);
            Assert.AreEqual(tree.Count, 4);
        }
        [TestMethod]
        public void CountAfterRemoveTest()
        {

            BinarySearchTree<int, int> tree = new BinarySearchTree<int, int>(8, 1);
            tree.Insert(3, 1);
            tree.Insert(10, 1);
            tree.Insert(1, 1);
            tree.Remove(3);
            Assert.AreEqual(tree.Count, 3);
        }
        [TestMethod]
        public void Remove1()
        {
            BinarySearchTree<int, int> tree = new BinarySearchTree<int, int>(8, 1);
            tree.Insert(3, 1);
            tree.Insert(10, 1);
            tree.Insert(1, 1);
            tree.Insert(6, 1);
            tree.Insert(14, 1);
            tree.Insert(4, 1);
            tree.Insert(7, 1);
            tree.Insert(13, 1);
            tree.Remove(3);
            Node<int, int> root = tree.Root;
            int[] ans = new int[] { 8, 4, 10, 1, 6, 14, 7, 13 };
            int n = tree.Count;
            Queue<Node<int, int>> queue = new Queue<Node<int, int>>();
            queue.Enqueue(root); int i = 0;
            while (queue.Count != 0)
            {
                Node<int,int> curr = queue.Dequeue();
                Assert.AreEqual(curr.Key, ans[i]);
                i++;
                if (!(curr.Left == null))
                    queue.Enqueue(curr.Left);
                if (!(curr.Right == null))
                    queue.Enqueue(curr.Right);
            }
        }
    }
}
