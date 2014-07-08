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
            for (int i = 0; i < 3; i++)
                Assert.AreEqual(resultList[i], intTree.Traverse()[i]);
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
            for (int i = 0; i < 3; i++)
                Assert.AreEqual(resultList[i], intTree.Traverse()[i]);
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
            for (int i = 0; i < 3; i++)
                Assert.AreEqual(resultList[i], intTree.Traverse()[i]);
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
    }
}
