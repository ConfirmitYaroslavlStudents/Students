using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Heap
{
	class HeapIsEmptyException : Exception { }

	class Heap<T> : IEnumerable<T>
	{
		private List<T> _heap;
		private IComparer<T> _comparer;

		public Heap()
		{
			_heap = new List<T>();
			_comparer = Comparer<T>.Default;
		}
		public Heap(IComparer<T> userComparer) : this()
		{
			if (userComparer != null)
				_comparer = userComparer;
		}
		public Heap(IEnumerable<T> collection)
		{
			_heap = new List<T>(collection);
			_comparer = Comparer<T>.Default;
			_heap.Sort();
		}
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
		public Heap(int capacity, IComparer<T> userComparer) : this(capacity)
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

	class Program
	{
		class MyIntComparer : IComparer<int>
		{
			public int Compare(int a, int b)
			{
				return b - a;
			}
		}

		class MyStringComparar : IComparer<string>
		{
			public int Compare(string a, string b)
			{
				return -a.CompareTo(b);
			}
		}

		static void HeapSort<T>(T[] array)
		{
			Heap<T> sortingHeap = new Heap<T>(array.Length);

			foreach (T arrayElement in array)
				sortingHeap.Add(arrayElement);

			while (sortingHeap.Count > 0)
				array[array.Length - sortingHeap.Count] = sortingHeap.DeleteTop();
		}

		static void HeapSort<T>(T[] array, IComparer<T> comparar)
		{
			Heap<T> sortingHeap = new Heap<T>(array.Length, comparar);

			foreach (T arrayElement in array)
				sortingHeap.Add(arrayElement);

			while (sortingHeap.Count > 0)
				array[array.Length - sortingHeap.Count] = sortingHeap.DeleteTop();
		}

		static void HeapSortTimeTest()
		{
			StringWriter result = new StringWriter();

			for (int i = 100; i <= 100000000; i *= 10)
			{
				TimeSpan sortingTime = new TimeSpan();

				int[] testArray = SortingTimeTest.GenerateIntArray(i);
				int[] testArrayCopy = new int[i];
				testArray.CopyTo(testArrayCopy, 0);

				result.WriteLine("Sorting an array of {0} elements.", i);
				result.Write("HeapSort time: ");
				DateTime beforeSorting = DateTime.Now;
				HeapSort<int>(testArray);
				sortingTime = (DateTime.Now - beforeSorting);
				result.WriteLine("{0:0.######} seconds.", sortingTime.TotalSeconds);

				result.Write("QuickSort time: ");
				beforeSorting = DateTime.Now;
				Array.Sort<int>(testArrayCopy);
				sortingTime = (DateTime.Now - beforeSorting);
				result.WriteLine("{0:0.######} seconds.", sortingTime.TotalSeconds);
			}

			using (StreamWriter SW = new StreamWriter("test3.txt"))
				SW.WriteLine(result);
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

			testHeap = new Heap<int>(new MyIntComparer());
			testHeap.Add(7);
			testHeap.Add(3);
			testHeap.Add(5);
			testHeap.Add(1);
			Console.WriteLine("Using my int comparer. We have MaxHeap.");
			while (testHeap.Count > 0)
				Console.WriteLine(testHeap.DeleteTop());

			Queue<int> testQueue = new Queue<int>(new int[] { 10, -3, 5, 0, -25 });
			testHeap = new Heap<int>(testQueue);
			Console.WriteLine("We get heap from queue with default int comparer (MinHeap).");
			while (testHeap.Count > 0)
				Console.WriteLine(testHeap.DeleteTop());

			testHeap = new Heap<int>(testQueue, new MyIntComparer());
			Console.WriteLine("We get heap from queue with my int comparer (MaxHeap).");
			// Now Heap is IEnumerable<T>
			foreach (int a in testHeap)
				Console.WriteLine(a);

			string[] testArray = { "10", "2", "1", "1001" };
			HeapSort<string>(testArray);
			Console.WriteLine("We sorted array with default string comparer.");
			for (int i = 0; i < testArray.Length - 1; ++i)
				Console.Write(testArray[i].ToString() + " ");
			Console.WriteLine(testArray[testArray.Length - 1]);

			HeapSort<string>(testArray, new MyStringComparar());
			Console.WriteLine("We sorted array with my string comparer.");
			for (int i = 0; i < testArray.Length - 1; ++i)
				Console.Write(testArray[i].ToString() + " ");
			Console.WriteLine(testArray[testArray.Length - 1]);

			for (int i = 0; i < 5; ++i)
				HeapSortTimeTest();
		}
	}
}
