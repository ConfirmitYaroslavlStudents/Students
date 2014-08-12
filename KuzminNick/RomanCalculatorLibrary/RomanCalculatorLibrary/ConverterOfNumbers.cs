using System;
using System.Collections.Generic;
using System.Linq;

namespace RomanCalculatorLibrary
{
    public class ConverterOfNumbers
    {
        public string ConvertArabicNumberToRoman(int arabicNumber)
        {
            var resultingRomanNumber = String.Empty;
            int[] digitsValues = { 1, 4, 5, 9, 10, 40, 50, 90, 100, 400, 500, 900, 1000 };
            string[] romanDigits = { "I", "IV", "V", "IX", "X", "XL", "L", "XC", "C", "CD", "D", "CM", "M" };
            while (arabicNumber > 0)
            {
                var indexMaxDivisorOfCurrentNumber = digitsValues.Length - 1;
                for (; indexMaxDivisorOfCurrentNumber >= 0; indexMaxDivisorOfCurrentNumber--)
                {
                    if (arabicNumber / digitsValues[indexMaxDivisorOfCurrentNumber] >= 1)
                    {
                        arabicNumber -= digitsValues[indexMaxDivisorOfCurrentNumber];
                        resultingRomanNumber += romanDigits[indexMaxDivisorOfCurrentNumber];
                        break;
                    }
                }
            }
            return resultingRomanNumber;
        }

        public int ConvertRomanNumberToArabic(string romanFormatNumber)
        {
            var result = 0;

            var stack = new Stack<int>();
            var listOfArabicDigits = new List<int>();
            foreach (var currentCharOfRomanNumber in romanFormatNumber)
            {
                var currentArabicDigit = ConvertRomanDigitToArabic(currentCharOfRomanNumber);
                listOfArabicDigits.Add(currentArabicDigit);
            }

            foreach (var currentArabicDigit in listOfArabicDigits)
            {
                if (stack.Count != 0)
                {
                    if (currentArabicDigit < stack.Peek())
                    {
                        result += stack.Sum();
                        stack.Clear();
                        stack.Push(currentArabicDigit);
                    }
                    else if (currentArabicDigit == stack.Peek())
                    {
                        stack.Push(currentArabicDigit);
                    }
                    else
                    {
                        var subtrahend = stack.Sum();
                        var minuend = currentArabicDigit;
                        var currentArabicDigitAfterSubtraction = minuend - subtrahend;
                        result += currentArabicDigitAfterSubtraction;
                        stack.Clear();
                    }
                }
                else
                {
                    stack.Push(currentArabicDigit);
                }
            }

            var remainingDigits = stack.Sum();
            result += remainingDigits;

            return result;
        }

        public int ConvertRomanDigitToArabic(char firstDigit)
        {
            if (firstDigit == 'M')
                return 1000;
            if (firstDigit == 'D')
                return 500;
            if (firstDigit == 'C')
                return 100;
            if (firstDigit == 'L')
                return 50;
            if (firstDigit == 'X')
                return 10;
            if (firstDigit == 'V')
                return 5;
            if (firstDigit == 'I')
                return 1;

            throw new ArgumentException("Uncorrect input data");
        }
    }
}
