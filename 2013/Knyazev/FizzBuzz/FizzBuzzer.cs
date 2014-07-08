using System;
using System.Collections.Generic;

namespace FizzBuzz
{
	public class ZeroCanNotBeKeyException : Exception { }
	public class FizzBuzzerHasThisKeyException : Exception { }

	public class FizzBuzzer
	{
		private Dictionary<int, string> _numberStringPairs;

		public FizzBuzzer()
		{
			_numberStringPairs = new Dictionary<int, string>();
			_numberStringPairs.Add(3, "Fizz");
			_numberStringPairs.Add(5, "Buzz");
		}

		public void AddNumberStringPair(int number, string numberString)
		{
			if (_numberStringPairs.ContainsKey(number))
				throw new FizzBuzzerHasThisKeyException();
			else if (number == 0)
				throw new ZeroCanNotBeKeyException();

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
