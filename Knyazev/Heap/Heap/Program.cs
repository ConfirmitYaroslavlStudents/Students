using System;
using System.Collections.Generic;

namespace Heap
{
	class MinHeap<T> where T : IComparable
	{
		private List<T> _heap;

		public MinHeap()
		{
			_heap = new List<T>();
		}

		public void Add(T elem)
		{
			int ind_elem = _heap.Count;
			_heap.Add(elem);

			while (ind_elem > 0 && elem.CompareTo(_heap[ind_elem / 2]) < 0)
			{
				Swap(ind_elem, ind_elem / 2);
				ind_elem /= 2;
			}
		}
		public T DeleteMinimum()
		{
			if (_heap.Count > 0)
			{
				T min = _heap[0];

				_heap[0] = _heap[_heap.Count - 1];
				_heap.RemoveRange(_heap.Count - 1, 1);
				int ind_new_root = 0;
				int ind_smallest = FindSmallestSonIndex(ind_new_root);

				while (ind_new_root < ind_smallest)
				{
					Swap(ind_new_root, ind_smallest);
					ind_new_root = ind_smallest;
					ind_smallest = FindSmallestSonIndex(ind_new_root);
				}

				return min;
			}
			else
				return default(T);
		}

		private void Swap(int ind_elem1, int ind_elem2)
		{
			T temp = _heap[ind_elem1];
			_heap[ind_elem1] = _heap[ind_elem2];
			_heap[ind_elem2] = temp;
		}
		private int FindSmallestSonIndex(int ind_parent)
		{
			int ind_smallest = ind_parent;
			int ind_left_son = (ind_parent + 1) * 2 - 1;
			int ind_right_son = (ind_parent + 1) * 2;

			if (ind_left_son < _heap.Count && _heap[ind_left_son].CompareTo(_heap[ind_parent]) < 0)
				ind_smallest = ind_left_son;
			if (ind_right_son < _heap.Count && _heap[ind_right_son].CompareTo(_heap[ind_smallest]) < 0)
				ind_smallest = ind_right_son;

			return ind_smallest;
		}

		public T Minimum
		{
			get
			{
				if (_heap.Count > 0)
					return _heap[0];
				else
					return default(T);
			}
		}
		public int Size
		{
			get { return _heap.Count; }
		}
	}

	class Program
	{
		static void Main()
		{
			MinHeap<int> H = new MinHeap<int>();
			H.Add(7);
			H.Add(3);
			H.Add(5);
			H.Add(1);

			while (H.Size > 0)
			Console.WriteLine(H.DeleteMinimum());
		}
	}
}
