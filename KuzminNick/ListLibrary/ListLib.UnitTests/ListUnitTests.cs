using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ListLibrary;

namespace ListLib.UnitTests
{
    [TestClass]
    public class ListUnitTests
    {
        [TestMethod]
        public void List_CorrectnessPropertyCountAfterRemoving()
        {
            var list = new List<int>();
            for (var i = 0; i < 10; i++)
                list.Add(i);

            list.RemoveAt(1); 
            Assert.AreEqual(9, list.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void List_NegativeIndexRemovingElement()
        {
            var list = new List<int> {5};
            list.RemoveAt(-3);
        }

        [TestMethod]
        public void List_CorrectnessRemoveMethod()
        {
            var list = new List<int>();
            for (var i = 0; i < 10; i++)
                list.Add(i);

            list.Remove(2);
            list.Remove(3);

            Assert.AreEqual(8, list.Count);
        }

        [TestMethod]
        public void List_CorrectnessEnumerator()
        {
            var list = new List<int>();
            for (var i = 0; i < 10; i++)
                list.Add(i);

            int temp = 0;
            foreach (int i in list)
                temp = i;

            Assert.AreEqual(9, temp);
        }

        [TestMethod]
        public void List_CorrectnessIndexOfMethod()
        {
            var list = new List<int>();
            for (var i = 0; i < 10; i++)
                list.Add(i);

            Assert.AreEqual(5, list.IndexOf(5));
        }

        [TestMethod]
        public void List_IndexOfNotIncludedElementInCollection()
        {
            var list = new List<int>();
            for (var i = 0; i < 10; i++)
                list.Add(i);

            Assert.AreEqual(-1, list.IndexOf(34));
        }

        [TestMethod]
        public void List_CorrectnessOfIndexer()
        {
            var list = new List<int>();
            for (var i = 0; i < 10; i++)
                list.Add(i);

            Assert.AreEqual(6, list[6]);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void List_NegetiveValueOfIndexerGetMethod()
        {
            var list = new List<int>();
            for (var i = 0; i < 10; i++)
                list.Add(i);

            var temp = list[-2];
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void List_NegetiveValueOfIndexerSetMethod()
        {
            var list = new List<int>();
            for (var i = 0; i < 10; i++)
                list.Add(i);

            list[-2] = 10;
        }

        [TestMethod]
        public void List_CorrectnessClearMethod()
        {
            var list = new List<int>();
            for (var i = 0; i < 5; i++)
                list.Add(i);

            list.Clear();
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void List_CorrectnessContainsMethodForNullElement()
        {
            var list = new List<string>();
            for (var i = 0; i < 5; i++)
                list.Add(i.ToString());

            list.Add(null);

            Assert.AreEqual(true, list.Contains(null));
        }

        [TestMethod]
        public void List_CorrectnessOfCopyingInArray()
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
        public void List_NegativeIndexCopyingInArray()
        {
            var list = new List<int>();
            for (var i = 0; i < 5; i++)
                list.Add(i);

            var intArray = new int[10];
            list.CopyTo(intArray, -2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void List_ExceptionNullArray()
        {
            var list = new List<int>();
            for (var i = 0; i < 5; i++)
                list.Add(i);

            int[] intArray = null;  
         
            list.CopyTo(destinationArray: intArray, indexDestinationArray: 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void List_UncorrectIndexCopyingArray()
        {
            var list = new List<int>();
            for (var i = 0; i < 5; i++)
                list.Add(i);

            var intArray = new int[10];

            list.CopyTo(intArray, 8);
            Assert.AreEqual(1, intArray[9]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void List_ConstructorNegativeSize()
        {
            var list = new List<int>(-9);
        }

        [TestMethod]
        public void List_CorrectnessOfCountProperty()
        {
            var list = new List<int>();
            for (var i = 0; i < 5; i++)
                list.Add(i);

            Assert.AreEqual(5, list.Count);
        }

    }
}
