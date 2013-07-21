using System.Collections.Generic;

namespace Heap
{
	static class HeapSortExtension
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
