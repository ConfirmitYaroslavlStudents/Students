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
            //Why do we need variable "list" what is never used?
            var list = new List<int>(-9);
        }

        [TestMethod]
        public void Add_CorrectValueOfCount() //When I read this I see "Correct" as verb not as adverb
        //public void Add_CountIsProperlyChanged() - I think we should use something like this
        {
            var list = new List<int>();
            for (var i = 0; i < 5; i++)
                list.Add(i);

            Assert.AreEqual(5, list.Count);
        }

        [TestMethod]
        //The same comments as in previous
        public void Remove_CorrectValueOfCount()
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
        //And again this is the same
        public void Foreach_CorrectFunctioningOfEnumerator()
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
        //And again... and some other methods bellow
        public void IndexOf_CorrectGettingElementByIndex()
        {
            var list = new List<int>();
            for (var i = 0; i < 10; i++)
                list.Add(i);

            Assert.AreEqual(5, list.IndexOf(5));
        }

        [TestMethod]
        public void IndexOf_IndexOfElementNotIncludedInCollection()
        {
            var list = new List<int>();
            for (var i = 0; i < 10; i++)
                list.Add(i);

            Assert.AreEqual(-1, list.IndexOf(34));
        }

        [TestMethod]
        public void IndexerGetAccessor_CorrectValue()
        {
            var list = new List<int>();
            for (var i = 0; i < 10; i++)
                list.Add(i);

            Assert.AreEqual(6, list[6]);
        }

        [TestMethod]
        public void IndexerSetAccessor_CorrectValue()
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
        public void Clear_CorrectValueOfCount()
        {
            var list = new List<int>();
            for (var i = 0; i < 5; i++)
                list.Add(i);

            list.Clear();
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void CopyTo_CorrectCopyingOfArray()
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
        public void CopyTo_NegativeIndexWhenCopyingInArray_ExceptionThrown()
        {
            var list = new List<int>();
            for (var i = 0; i < 5; i++)
                list.Add(i);

            var intArray = new int[10];
            list.CopyTo(intArray, -2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CopyTo_IndexOutsideAllowableRangeWhenCopyingInArray_ExceptionThrown()
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
            
            //Check ReSharper
            list.CopyTo(destinationArray: intArray, indexForInsertingDestinationArray: 1);
        }
    }
}
