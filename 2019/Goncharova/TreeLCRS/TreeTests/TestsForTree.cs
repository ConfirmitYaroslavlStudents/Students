using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TreeLCRS;

namespace TreeTests
{
    [TestClass]
    public class TestsForTree
    {
        [TestMethod]
        public void InsertInEmptyTree()
        {
            Tree<int> tree = new Tree<int>();
            tree.Insert(8);
            Assert.AreEqual(true, tree.Contains(8));
        }
        [TestMethod]
        public void InsertIncreasesCount()
        {
            Tree<int> tree = new Tree<int>(10);
            tree.Insert(5, 10);
            tree.Insert(7, 5);
            tree.Insert(3, 10);
            Assert.AreEqual(4, tree.Count);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertTheSameValue()
        {
            Tree<int> tree = new Tree<int>(10);
            tree.Insert(5, 10);
            tree.Insert(7, 5);
            tree.Insert(7, 10);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertWithNonPresentParent()
        {
            Tree<int> tree = new Tree<int>(10);
            tree.Insert(5, 10);
            tree.Insert(7, 9);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void InsertRootInNonEmptyTree()
        {
            Tree<int> tree = new Tree<int>(10);
            tree.Insert(5);
        }
    }
}
