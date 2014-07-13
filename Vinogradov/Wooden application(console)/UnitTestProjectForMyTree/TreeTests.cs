using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Forest;

namespace UnitTestProjectForMyTree
{
    [TestClass]
    public class TreeTests
    {
        [TestMethod]
        public void CreationOfTheFirstType()
        {
            var birch = new Tree<int>();
            birch.Add(15);
            birch.Add(8);
            birch.Add(9);
            birch.Add(7);
            birch.Add(10);
        }

        [TestMethod]
        public void CreationOfTheSecondType()
        {
            var birch = new Tree<int>(new int[] { 10, 8, 7, 15, 9 });
        }

        [TestMethod]
        public void RemoveHead()
        {
            var birch = new Tree<int>(new int[] { 10,17,23 });
            birch.Remove(10);
        }

        [TestMethod]
        public void RemoveNodeWithoutChildren()
        {
            var birch = new Tree<int>(new int[] { 10, 8, 7, 15, 9 });
            birch.Remove(7);
        }

        [TestMethod]
        public void RemoveNodeInTheThickOfThings()
        {
            var birch = new Tree<int>(new int[] { 30, 40, 20, 25, 26, 10, 6, 3 });
            birch.Remove(20);
        }

        [TestMethod]
        [ExpectedException(typeof (InvalidOperationException))]
        public void RemoveWhenTreeIsEmpty()
        {
            var birch = new Tree<int>();
            birch.Remove(5);
        }

        [TestMethod]
        public void RemoveNon_existentNode()
        {
            var birch = new Tree<int>(new int[] { 10, 17, 23 });
            birch.Remove(33);
        }

        [TestMethod]
        public void CountAfterRemove()
        {
            var birch = new Tree<int>(new int[] { 10, 8, 7, 15, 9 });
            birch.Remove(7);
            var result = birch.Count;
            Assert.AreEqual(4, result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ContainsWhenTreeIsEmpty()
        {
            var birch = new Tree<int>();
            birch.Contains(5);
        }

        [TestMethod]
        public void ContainsFalse()
        {
            var birch = new Tree<int>(new int[] { 10, 8, 7, 15, 9 });
            var result = birch.Contains(5);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void ContainsTrue()
        {
            var birch = new Tree<int>(new int[] { 10, 8, 7, 15, 9 });
            var result = birch.Contains(9);
            Assert.AreEqual(true, result);
        }


    }
}
