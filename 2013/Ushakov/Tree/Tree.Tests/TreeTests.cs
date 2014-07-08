using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tree;
using System.Collections.Generic;

namespace TreeTests
{
    [TestClass]
    public class TreeTests
    {
        [TestMethod]
        public void Add1ElementGetListCount1()
        {
            var intTree = new Tree<int>();
            intTree.Add(0);

            Assert.AreEqual(1, intTree.Traverse().Count);
        }

        [TestMethod]
        public void Add2ElementsAndRemove1ElementGetListCount1()
        {
            var intTree = new Tree<int>();
            intTree.Add(0);
            intTree.Add(0);
            intTree.Remove(0);

            Assert.AreEqual(1, intTree.Traverse().Count);
        }

        [TestMethod]
        public void Add1ElementAndRemoveNotExistElementGetListCount1()
        {
            var intTree = new Tree<int>();
            intTree.Add(0);
            intTree.Remove(1);

            Assert.AreEqual(1, intTree.Traverse().Count);
        }

        [TestMethod]
        public void DirectTraversingFor3ElementsGet213List()
        {
            var intTree = new Tree<int>();
            intTree.Add(1);
            intTree.Add(2);
            intTree.Add(3);

            var resultList = new List<int>(new[] { 2, 1, 3 });
            CollectionAssert.AreEqual(resultList, intTree.Traverse());
        }

        [TestMethod]
        public void ReverseTraversingFor3ElementsGet132List()
        {
            var intTree = new Tree<int>();
            intTree.Add(1);
            intTree.Add(2);
            intTree.Add(3);

            intTree.Traversing = new ReverseTraversing<int>();

            var resultList = new List<int>(new[] { 1, 3, 2 });
            CollectionAssert.AreEqual(resultList, intTree.Traverse());
        }

        [TestMethod]
        public void SymmetrycTraversingFor3ElementsGet123List()
        {
            var intTree = new Tree<int>();
            intTree.Add(2);
            intTree.Add(1);
            intTree.Add(3);

            intTree.Traversing = new SymmetricTraversing<int>();

            var resultList = new List<int>(new[] { 1, 2, 3 });
            CollectionAssert.AreEqual(resultList, intTree.Traverse());
        }

        [TestMethod]
        public void GetEnumeratorFor3ElementsGetEnumerator213()
        {
            var intTree = new Tree<int>();
            intTree.Add(1);
            intTree.Add(2);
            intTree.Add(3);

            var resultEnumerator = intTree.GetEnumerator();
            var expectedEnumerator = (new List<int>(new[] { 2, 1, 3 })).GetEnumerator();
            for (int i = 0; i < 3; i++)
            {
                resultEnumerator.MoveNext();
                expectedEnumerator.MoveNext();
                Assert.AreEqual(expectedEnumerator.Current, resultEnumerator.Current);
            }
        }

        [TestMethod]
        public void SymmetrycTraversingFor10lementsGetCorrectList()
        {
            var intTree = new Tree<int>();
            var resultList = new List<int>();

            for (int i = 0; i < 10; i++)
            {
                intTree.Add(i);
                resultList.Add(i);
            }

            intTree.Traversing = new SymmetricTraversing<int>();

            CollectionAssert.AreEqual(resultList, intTree.Traverse());
        }

        [TestMethod]
        public void SymmetrycTraversingForReverce10ElementsGetCorrectList()
        {
            var intTree = new Tree<int>();
            var resultList = new List<int>();

            for (int i = 10; i > 0; i--)
            {
                intTree.Add(i);
                resultList.Add(10 - i + 1);
            }

            intTree.Traversing = new SymmetricTraversing<int>();

            CollectionAssert.AreEqual(resultList, intTree.Traverse());
        }

        [TestMethod]
        public void SymmetrycTraversingFor10ElementsAndDelete1ElementGetCorrectList()
        {
            var intTree = new Tree<int>();
            var resultList = new List<int>();

            for (int i = 0; i < 10; i++)
            {
                intTree.Add(i);
                resultList.Add(i);
            }

            intTree.Remove(5);
            resultList.Remove(5);

            intTree.Traversing = new SymmetricTraversing<int>();

            CollectionAssert.AreEqual(resultList, intTree.Traverse());
        }

        [TestMethod]
        public void BalancedTreeFor6ElementsGetCorrectList()
        {
            var intTree = new Tree<int>();
            var resultList = new List<int>(new[] {3, 1, 0, 2, 4, 5});

            for (int i = 0; i < 6; i++)
                intTree.Add(i);

            CollectionAssert.AreEqual(resultList, intTree.Traverse());
        }
    }
}
