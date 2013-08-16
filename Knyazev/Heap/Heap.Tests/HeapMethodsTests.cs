using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Heap.Tests
{
	[TestClass]
	public class HeapMethodsTests
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
	}

	class MyIntComparer : IComparer<int>
	{
		public int Compare(int a, int b)
		{
			return -a.CompareTo(b);
		}
	}
}
