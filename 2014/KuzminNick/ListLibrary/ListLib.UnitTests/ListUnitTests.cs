using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ListLibrary;

namespace ListLib.UnitTests
{
    [TestClass]
    public class ListLibraryUnitTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ListConstructor_NegativeValueOfSizeArray_ExceptionThrown()
        {
            new List<int>(-9);
        }

        [TestMethod]
        public void Add_CountIsProperlyChanged_AfterAddingElements()
        {
            var list = new List<int>();
            for (var i = 0; i < 5; i++)
                list.Add(i);

            Assert.AreEqual(5, list.Count);
        }

        [TestMethod]
        public void Remove_CountIsProperlyChanged_AfterAddingAndRemovingElements()
        {
            var list = new List<int>();
            for (var i = 0; i < 10; i++)
                list.Add(i);

            list.Remove(2);
            list.Remove(3);

            Assert.AreEqual(8, list.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RemoveAt_RemovingElementByNegativeIndex_ExceptionThrown()
        {
            var list = new List<int> {5};
            list.RemoveAt(-3);
        }

        [TestMethod]
        public void Foreach_VariableIsProperlyChangedByEnumerator()
        {
            var list = new List<int>();
            for (var i = 0; i < 10; i++)
                list.Add(i);

            var temp = 0;
            foreach (var i in list)
                temp = i;

            Assert.AreEqual(9, temp);
        }

        [TestMethod]
        public void IndexOf_ElementIsProperlyObtainedByIndex()
        {
            var list = new List<int>();
            for (var i = 0; i < 10; i++)
                list.Add(i);

            Assert.AreEqual(5, list.IndexOf(5));
        }

        [TestMethod]
        public void IndexOf_IndexOfElementNotIncludedInCollection_IsProperlyObtained()
        {
            var list = new List<int>();
            for (var i = 0; i < 10; i++)
                list.Add(i);

            Assert.AreEqual(-1, list.IndexOf(34));
        }

        [TestMethod]
        public void IndexerGetAccessor_ElementIsProperlyObtainedByIndexer()
        {
            var list = new List<int>();
            for (var i = 0; i < 10; i++)
                list.Add(i);

            Assert.AreEqual(6, list[6]);
        }

        [TestMethod]
        public void IndexerSetAccessor_ElementIsProperlyChangedByIndexer()
        {
            var list = new List<int>();
            for (var i = 0; i < 10; i++)
                list.Add(i);

            list[5] = 25;
            Assert.AreEqual(25, list[5]);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void IndexerSetAccessor_NegetiveValueOfIndexer_ExceptionThrown()
        {
            var list = new List<int>();
            for (var i = 0; i < 10; i++)
                list.Add(i);

            var temp = list[-2];
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void IndexerGetAccessor_NegetiveValueOfIndexer_ExceptionThrown()
        {
            var list = new List<int>();
            for (var i = 0; i < 10; i++)
                list.Add(i);

            list[-2] = 10;
        }

        [TestMethod]
        public void Clear_CountIsProperlyChangedAfterCleaningOfCollection()
        {
            var list = new List<int>();
            for (var i = 0; i < 5; i++)
                list.Add(i);

            list.Clear();
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void CopyTo_ArrayIsProperlyChanged_AfterCopyingFromList()
        {
            var list = new List<int>();
            for (var i = 0; i < 5; i++)
                list.Add(i);

            var intArray = new int[10];
            list.CopyTo(intArray, 2);

            Assert.AreEqual(1, intArray[3]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CopyTo_NegativeIndexOfInsertingCollectionInArray_ExceptionThrown()
        {
            var list = new List<int>();
            for (var i = 0; i < 5; i++)
                list.Add(i);

            var intArray = new int[10];
            list.CopyTo(intArray, -2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CopyTo_ListIsNotPlacedInLengthOfArray_ExceptionThrown()
        {
            var list = new List<int>();
            for (var i = 0; i < 5; i++)
                list.Add(i);

            var intArray = new int[10];

            list.CopyTo(intArray, 8);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CopyTo_CopyingInNullArray_ExceptionThrown()
        {
            var list = new List<int>();
            for (var i = 0; i < 5; i++)
                list.Add(i);

            int[] intArray = null;  
         
            list.CopyTo(destinationArray: intArray, indexForInsertingDestinationArray: 1);
        }
    }
}
