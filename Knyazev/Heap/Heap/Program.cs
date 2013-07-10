using System;
using System.Collections.Generic;

namespace Heap
{
	class HeapIsEmptyException : Exception { }

	class Heap<T> where T : IComparable
	{
		private List<T> _heap;
		private IComparer<T> _comparer = Comparer<T>.Default;

		public Heap()
		{
			_heap = new List<T>();
		}
		public Heap(IComparer<T> userComparer) : this()
		{
			if (userComparer != null)
				_comparer = userComparer;
		}
		public Heap(IEnumerable<T> collection)
		{
			_heap = new List<T>(collection);
			_heap.Sort();
		}
		public Heap(IEnumerable<T> collection, IComparer<T> userComparer)
		{
			_heap = new List<T>(collection);
			if (userComparer != null)
				_comparer = userComparer;
			_heap.Sort(_comparer);
		}

		public void Add(T newElement)
		{
			int indexNewElement = _heap.Count;
			_heap.Add(newElement);

			while (indexNewElement > 0 && _comparer.Compare(newElement, _heap[indexNewElement / 2]) < 0)
			{
				Swap(indexNewElement, indexNewElement / 2);
				indexNewElement /= 2;
			}
		}
		public T DeleteTop()
		{
			if (_heap.Count > 0)
			{
				T topElement = _heap[0];

				_heap[0] = _heap[_heap.Count - 1];
				_heap.RemoveAt(_heap.Count - 1);

				int indexNewRoot = 0;
				int indexAimSon = FindAimSonIndex(indexNewRoot);
				while (indexNewRoot < indexAimSon)
				{
					Swap(indexNewRoot, indexAimSon);
					indexNewRoot = indexAimSon;
					indexAimSon = FindAimSonIndex(indexNewRoot);
				}

				return topElement;
			}
			else
				throw new HeapIsEmptyException();
		}

		private void Swap(int indexElement1, int indexElement2)
		{
			T temp = _heap[indexElement1];
			_heap[indexElement1] = _heap[indexElement2];
			_heap[indexElement2] = temp;
		}
		private int FindAimSonIndex(int indexParent)
		{
			int indexAim = indexParent;
			int indexLeftSon = (indexParent + 1) * 2 - 1;
			int indexRightSon = (indexParent + 1) * 2;
			if (indexLeftSon < _heap.Count && _comparer.Compare(_heap[indexLeftSon], _heap[indexParent]) < 0)
				indexAim = indexLeftSon;
			if (indexRightSon < _heap.Count && _comparer.Compare(_heap[indexRightSon], _heap[indexAim]) < 0)
				indexAim = indexRightSon;

			return indexAim;
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

	class Program
	{
		class MyComparer : IComparer<int>
		{
			public int Compare(int a, int b)
			{
				return b - a;
			}
		}

		static void Main()
		{
			Heap<int> testHeap = new Heap<int>();
			testHeap.Add(7);
			testHeap.Add(3);
			testHeap.Add(5);
			testHeap.Add(1);
			Console.WriteLine("Using default int comparer. We have MinHeap.");
			while (testHeap.Count > 0)
				Console.WriteLine(testHeap.DeleteTop());

			testHeap = new Heap<int>(new MyComparer());
			testHeap.Add(7);
			testHeap.Add(3);
			testHeap.Add(5);
			testHeap.Add(1);
			Console.WriteLine("Using my int comparer. We have MaxHeap.");
			while (testHeap.Count > 0)
				Console.WriteLine(testHeap.DeleteTop());

			Queue<int> testQueue = new Queue<int>(new int[] { 10, -3, 5, 0, -25 });
			testHeap = new Heap<int>(testQueue);
			Console.WriteLine("We get heap from queue with default comparer (MinHeap).");
			while (testHeap.Count > 0)
				Console.WriteLine(testHeap.DeleteTop());

			testHeap = new Heap<int>(testQueue, new MyComparer());
			Console.WriteLine("We get heap from queue with my comparer (MaxHeap).");
			while (testHeap.Count > 0)
				Console.WriteLine(testHeap.DeleteTop());
		}
	}
}
