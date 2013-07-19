using System;

namespace Heap
{
	class SortingTimeTest
	{
		const int MINNUM = -2000000000, MAXNUM = 2000000000;

		public static int[] GenerateIntArray(int size)
		{
			var randomNumberGenerator = new Random();
			var result = new int[size];
			for (int i = 0; i < size; ++i)
				result[i] = randomNumberGenerator.Next(MINNUM, MAXNUM);

			return result;
		}
	}
}
