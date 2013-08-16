using System;
using System.Collections.Generic;

namespace FizzBuzz
{
	public class FizzBuzzer
	{
		private Dictionary<int, string> _numberStringPairs;

		public FizzBuzzer()
		{
			_numberStringPairs = new Dictionary<int, string>();
		}

		public void AddNumberStringPair(int number, string numberString)
		{
			_numberStringPairs.Add(number, numberString);
		}

		public void DeleteNumberStringPair(int number)
		{
			_numberStringPairs.Remove(number);
		}

		public string ToString(int number)
		{
			var result = new List<string>();

			foreach (KeyValuePair<int, string> numberStringPair in _numberStringPairs)
				if (number % numberStringPair.Key == 0)
					result.Add(numberStringPair.Value);

			if (result.Count > 0)
				return String.Join(" ", result);
			else
				return number.ToString();
		}
	}
}
