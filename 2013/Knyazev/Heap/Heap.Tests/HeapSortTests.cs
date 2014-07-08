using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Heap.Tests
{
	[TestClass]
	public class HeapSortTests
	{
		[TestMethod]
		public void SortingBadArray()
		{
			var array = new[] { 5, 4, 3, 2, 1 };
			var arrayCopy = (int[])array.Clone();

			array.HeapSort();
			Array.Sort(arrayCopy);

			CollectionAssert.AreEqual(arrayCopy, array);
		}

		[TestMethod]
		public void SortingSomeArray()
		{
			var array = new[] { -5, 5, -10, -4, 11, 0, 1, -9, 9, 7 };
			var arrayCopy = (int[])array.Clone();

			array.HeapSort();
			Array.Sort(arrayCopy);

			CollectionAssert.AreEqual(arrayCopy, array);
		}

		[TestMethod]
		public void SortingSomeArrayWhenMaxComparerUsing()
		{
			var array = new[] { -5, 5, -10, -4, 11, 0, 1, -9, 9, 7 };
			var arrayCopy = (int[])array.Clone();
			var comparer = new MyIntComparer();

			array.HeapSort(comparer);
			Array.Sort(arrayCopy, comparer);

			CollectionAssert.AreEqual(arrayCopy, array);
		}

		[TestMethod]
		public void SortingWithoutChanges()
		{
			var array = new[] { 1, 2, 3, 4, 5 };
			var arrayCopy = (int[])array.Clone();

			array.HeapSort();

			CollectionAssert.AreEqual(arrayCopy, array);
		}
	}
}
