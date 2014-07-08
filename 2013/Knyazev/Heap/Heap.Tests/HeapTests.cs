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
		public void Add2ReallyAdd2()
		{
			var heap = new Heap<int>();

			heap.Add(2);

			Assert.AreEqual(heap.Top, 2);
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

		[TestMethod, ExpectedException(typeof(HeapIsEmptyException))]
		public void DeleteElementFromEmptyHeapLeadsToException()
		{
			var heap = new Heap<int>();

			heap.DeleteTop();
		}

		[TestMethod]
		public void DeletedElementIsAnElementInTop()
		{
			var heap = new Heap<int>();

			heap.Add(3);
			heap.Add(2);
			var element = heap.DeleteTop();

			Assert.AreEqual(element, 2);
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

		[TestMethod, ExpectedException(typeof(HeapIsEmptyException))]
		public void TopElementOfEmptyHeapLeadsToException()
		{
			var heap = new Heap<int>();

			var top = heap.Top;
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
		public void HeapFromCollectionConstructorWorksRight()
		{
			var list = new List<int>(new[] { 2, 4, -2, 0, 1, 7 });
			var heap = new Heap<int>(list);

			list.Sort();
			var listFromHeap = new List<int>();
			foreach (var element in heap)
				listFromHeap.Add(element);

			CollectionAssert.AreEqual(list, listFromHeap);
		}

		[TestMethod]
		public void HeapFromCollectionConstructorWorksRightWhenMaxComparerUsing()
		{
			var comparer = new MyIntComparer();
			var list = new List<int>(new[] { 2, 4, -2, 0, 1, 7 });
			var heap = new Heap<int>(list, comparer);

			list.Sort(comparer);
			var listFromHeap = new List<int>();
			foreach (var element in heap)
				listFromHeap.Add(element);

			CollectionAssert.AreEqual(list, listFromHeap);
		}

		[TestMethod]
		public void GetEnumeratorReturnCorrectEnumerator()
		{
			var list = new List<int>(new[] { 2, 4, -2, 0, 1, 7 });
			var heap = new Heap<int>(list);

			list.Sort();
			var listEnumerator = list.GetEnumerator();
			var heapEnumerator = heap.GetEnumerator();

			while (listEnumerator.MoveNext() && heapEnumerator.MoveNext())
				Assert.AreEqual(listEnumerator.Current, heapEnumerator.Current);

			Assert.AreEqual(listEnumerator.MoveNext(), heapEnumerator.MoveNext());
		}
	}

	public class MyIntComparer : IComparer<int>
	{
		public int Compare(int a, int b)
		{
			return -a.CompareTo(b);
		}
	}
}
