using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Heap.Tests
{
	[TestClass]
	public class HeapTests
	{
		[TestMethod]
		public void AddElementIncreaseHeapSize()
		{
			var heap = new Heap<int>();

			heap.Add(1);

			Assert.AreEqual(heap.Count, 1);
		}

		[TestMethod]
		public void MinElementInTop1()
		{
			var heap = new Heap<int>();

			heap.Add(0);
			heap.Add(2);

			Assert.AreEqual(heap.Top, 0);
		}

		[TestMethod]
		public void MinElementInTop2()
		{
			var heap = new Heap<int>();

			heap.Add(2);
			heap.Add(0);

			Assert.AreEqual(heap.Top, 0);
		}

		[TestMethod]
		public void MaxElementInTopWhenMaxComparerUsing()
		{
			var heap = new Heap<int>(new MyIntComparer());

			heap.Add(2);
			heap.Add(0);

			Assert.AreEqual(heap.Top, 2);
		}

		[TestMethod]
		public void DeleteElementDecreaseHeapSize()
		{
			var heap = new Heap<int>();

			heap.Add(2);
			heap.DeleteTop();

			Assert.AreEqual(heap.Count, 0);
		}

		[TestMethod]
		public void MinInTopAfterDelete()
		{
			var heap = new Heap<int>();

			heap.Add(3);
			heap.Add(2);
			heap.Add(1);
			heap.Add(0);
			heap.DeleteTop();

			Assert.AreEqual(heap.Top, 1);
		}

		[TestMethod]
		public void MaxInTopAfterDeleteWhenMaxComparerUsing()
		{
			var heap = new Heap<int>(new MyIntComparer());

			heap.Add(3);
			heap.Add(2);
			heap.Add(1);
			heap.Add(0);
			heap.DeleteTop();

			Assert.AreEqual(heap.Top, 2);
		}

		[TestMethod]
		public void SortingBadArray()
		{
			var array = new[] { 5, 4, 3, 2, 1 };

			array.HeapSort();

			for (int i = 1; i <= 5; ++i)
				Assert.AreEqual(array[i - 1], i);
		}

		[TestMethod]
		public void SortingSomeArray()
		{
			var array = new[] { -5, 5, -10, -4, 11, 0, 1, -9, 9, 7 };
			var arrayCopy = new[] { -5, 5, -10, -4, 11, 0, 1, -9, 9, 7 };

			array.HeapSort();
			Array.Sort(arrayCopy);

			for (int i = 0; i < array.Length; ++i)
				Assert.AreEqual(array[i], arrayCopy[i]);
		}

		[TestMethod]
		public void SortingSomeArrayWhenMaxComparerUsing()
		{
			var array = new[] { -5, 5, -10, -4, 11, 0, 1, -9, 9, 7 };
			var arrayCopy = new[] { -5, 5, -10, -4, 11, 0, 1, -9, 9, 7 };

			var comparer = new MyIntComparer();
			array.HeapSort(comparer);
			Array.Sort(arrayCopy, comparer);

			for (int i = 0; i < array.Length; ++i)
				Assert.AreEqual(array[i], arrayCopy[i]);
		}

		[TestMethod]
		public void SortingWithoutChanges()
		{
			var array = new[] { 1, 2, 3, 4, 5 };

			array.HeapSort();

			for (int i = 1; i <= 5; ++i)
				Assert.AreEqual(array[i - 1], i);
		}
	}

	class MyIntComparer : IComparer<int>
	{
		public int Compare(int a, int b)
		{
			return -a.CompareTo(b);
		}
	}
}
