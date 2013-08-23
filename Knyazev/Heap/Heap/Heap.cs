using System;
using System.Collections;
using System.Collections.Generic;

namespace Heap
{
	public class HeapIsEmptyException : Exception { }

	public class Heap<T> : IEnumerable<T>
	{
		private List<T> _heap;
		private IComparer<T> _comparer;

		public Heap()
		{
			_heap = new List<T>();
			_comparer = Comparer<T>.Default;
		}

		public Heap(IComparer<T> userComparer)
			: this()
		{
			if (userComparer != null)
				_comparer = userComparer;
		}

		public Heap(IEnumerable<T> collection)
			: this(collection, Comparer<T>.Default) { }

		public Heap(IEnumerable<T> collection, IComparer<T> userComparer)
		{
			_heap = new List<T>(collection);
			if (userComparer != null)
				_comparer = userComparer;
			else
				_comparer = Comparer<T>.Default;
			_heap.Sort(_comparer);
		}

		public Heap(int capacity)
		{
			_heap = new List<T>(capacity);
			_comparer = Comparer<T>.Default;
		}

		public Heap(int capacity, IComparer<T> userComparer)
			: this(capacity)
		{
			if (userComparer != null)
				_comparer = userComparer;
		}

		public void Add(T newElement)
		{
			int newElementIndex = _heap.Count;
			_heap.Add(newElement);

			while (newElementIndex > 0 && _comparer.Compare(newElement, _heap[newElementIndex / 2]) < 0)
			{
				Swap(newElementIndex, newElementIndex / 2);
				newElementIndex /= 2;
			}
		}

		public T DeleteTop()
		{
			if (_heap.Count > 0)
			{
				T topElement = _heap[0];

				_heap[0] = _heap[_heap.Count - 1];
				_heap.RemoveAt(_heap.Count - 1);

				int newRootIndex = 0;
				int aimSonIndex = FindAimSonIndex(newRootIndex);
				while (newRootIndex < aimSonIndex)
				{
					Swap(newRootIndex, aimSonIndex);
					newRootIndex = aimSonIndex;
					aimSonIndex = FindAimSonIndex(newRootIndex);
				}

				return topElement;
			}
			else
				throw new HeapIsEmptyException();
		}

		public IEnumerator<T> GetEnumerator()
		{
			return _heap.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return (IEnumerator)_heap.GetEnumerator();
		}

		private void Swap(int firstElementIndex, int secondElementIndex)
		{
			T temp = _heap[firstElementIndex];
			_heap[firstElementIndex] = _heap[secondElementIndex];
			_heap[secondElementIndex] = temp;
		}

		private int FindAimSonIndex(int parentIndex)
		{
			int aimIndex = parentIndex;
			int leftSonIndex = (parentIndex + 1) * 2 - 1;
			int rightSonIndex = (parentIndex + 1) * 2;
			if (leftSonIndex < _heap.Count && _comparer.Compare(_heap[leftSonIndex], _heap[parentIndex]) < 0)
				aimIndex = leftSonIndex;
			if (rightSonIndex < _heap.Count && _comparer.Compare(_heap[rightSonIndex], _heap[aimIndex]) < 0)
				aimIndex = rightSonIndex;

			return aimIndex;
		}

		public T Top
		{
			get
			{
				if (_heap.Count > 0)
					return _heap[0];
				else
					throw new HeapIsEmptyException();
			}
		}

		public int Count
		{
			get { return _heap.Count; }
		}
	}

	public static class HeapSortExtension
	{
		public static void HeapSort<T>(this T[] array)
		{
			var sortingHeap = new Heap<T>(array.Length);

			foreach (T arrayElement in array)
				sortingHeap.Add(arrayElement);

			while (sortingHeap.Count > 0)
				array[array.Length - sortingHeap.Count] = sortingHeap.DeleteTop();
		}

		public static void HeapSort<T>(this T[] array, IComparer<T> comparer)
		{
			var sortingHeap = new Heap<T>(array.Length, comparer);

			foreach (T arrayElement in array)
				sortingHeap.Add(arrayElement);

			while (sortingHeap.Count > 0)
				array[array.Length - sortingHeap.Count] = sortingHeap.DeleteTop();
		}
	}
}
