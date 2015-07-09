using System;
using System.Text;

namespace RomanCalculator
{
    public static class RomanCalculator
    {
        private static readonly string[] RomanDigits = { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I"};
        private static readonly int[] Arabic = { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };

        public static int ToArabic(string romanNumber)
        {
            if (string.IsNullOrEmpty(romanNumber))
            {
                throw new ArgumentNullException("romanNumber");
            }

            int romanNumberLength = romanNumber.Length;
            int result = 0, offset = 0, i = 0;

            while (offset < romanNumberLength)
            {
                if (i == RomanDigits.Length)
                {
                    throw new ArgumentOutOfRangeException("romanNumber");
                }

                string romanDigit = RomanDigits[i];
                int repeatCount = 0;

                while (romanDigit == romanNumber.Substring(offset, Math.Min(romanDigit.Length, romanNumberLength - offset)).ToUpper())
                {
                    offset += romanDigit.Length;
                    result += Arabic[i];
                    ++repeatCount;
                }
                ++i;

                if ((repeatCount > 3 && (romanDigit == "I" || romanDigit == "X" || romanDigit == "C" || romanDigit == "M")) ||
                    repeatCount >= 2 && romanDigit.Length == 2)
                {
                    throw new ArgumentOutOfRangeException("romanNumber");
                }
            }

            return result;
        }

        public static string ToRoman(int arabicNumber)
        {
            var result = new StringBuilder();
            int i = 0;

            if (arabicNumber <= 0)
            {
                throw new ArgumentOutOfRangeException("arabicNumber");
            }

            while (arabicNumber > 0)
            {
                while (Arabic[i] <= arabicNumber)
                {
                    result.Append(RomanDigits[i]);
                    arabicNumber -= Arabic[i];
                }
                ++i;
            }

            return result.ToString();
        }

        public static string Add(string romanNumber1, string romanNumber2)
        {
            return ToRoman(ToArabic(romanNumber1) + ToArabic(romanNumber2));
        }

        public static string Multiply(string romanNumber1, string romanNumber2)
        {
            return ToRoman(ToArabic(romanNumber1) * ToArabic(romanNumber2));
        }

        public static string Substract(string romanNumber1, string romanNumber2)
        {
            return ToRoman(ToArabic(romanNumber1) - ToArabic(romanNumber2));
        }

        public static string CalculateExpression(string expression)
        {
            var expressionParts = expression.Split(new []{' '}, StringSplitOptions.RemoveEmptyEntries);

            if (expressionParts.Length != 3)
            {
                throw new ArgumentException("Invalid expression!");
            }

            string firstRomanNumber = expressionParts[0];
            string secondRomanNumber = expressionParts[2];
            string operation = expressionParts[1];

            switch (operation)
            {
                case "+":
                    return Add(firstRomanNumber, secondRomanNumber);
                case "-":
                    return Substract(firstRomanNumber, secondRomanNumber);
                case "*":
                    return Multiply(firstRomanNumber, secondRomanNumber);
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
