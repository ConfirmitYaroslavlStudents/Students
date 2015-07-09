using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace RomanCalculator
{
    public class NaturalNumber
    {
        private static readonly Dictionary<char, int> DigitValues = new Dictionary<char, int>
        {
            {'I', 1},
            {'V', 5},
            {'X', 10},
            {'L', 50},
            {'C', 100},
            {'D', 500},
            {'M', 1000}
        };

        private static readonly Dictionary<int, string> MainRomanCombination = new Dictionary<int, string>
        {
            {1000, "M"},
            {900, "CM"},
            {500, "D"},
            {400, "CD"},
            {100, "C"},
            {90,"XC"},
            {50, "L"},
            {40, "XL"},
            {10, "X"},
            {9, "IX"},
            {5, "V"},
            {4, "IV"},
            {1, "I"},
        };

        public int Arabic { get; private set; }
        public string Roman { get; private set; }

        public NaturalNumber(string romanNumber)
        {
            if (!NumberIsValid(romanNumber))
            {
                throw new InvalidDataException("romanDigit");
            }

            Roman = romanNumber;
            Arabic = ToArabic(romanNumber);
        }

        public NaturalNumber(int arabicNumber)
        {
            if (arabicNumber <= 0)
            {
                throw new ArgumentOutOfRangeException("arabicNumber");
            }

            Roman = ToRoman(arabicNumber);
            Arabic = arabicNumber;
        }

        private static int GetArabicDigitFromRoman(char romanDigit)
        {
            return DigitValues[romanDigit];
        }
        // move after 'NaturalNumber(string romanNumber)' method
        private static bool NumberIsValid(string romanNumber)
        {
            //canonical roman number validation
            // Change comment to method that explains the logic of algorithm of its name
            const string pattern = "^(?i)M{0,3}(D?C{0,3}|C[DM])(L?X{0,3}|X[LC])(V?I{0,3}|I[VX])$";
            var rgx = new Regex(pattern, RegexOptions.IgnoreCase);

            return rgx.IsMatch(romanNumber);
        }

        //move after 'NaturalNumber(string romanNumber)' method
        private static int ToArabic(string romanNumber)
        {
            if (romanNumber.Length == 0)
                return 0;

            var arabicNumber = 0;
            var theBiggest = FindTheBiggestNumber(romanNumber);
            var indexOfSeparation = romanNumber.IndexOf(theBiggest);

            arabicNumber += GetArabicDigitFromRoman(romanNumber[indexOfSeparation]);
            arabicNumber += ToArabic(romanNumber.Substring(indexOfSeparation + 1, romanNumber.Length - indexOfSeparation - 1));
            arabicNumber -= ToArabic(romanNumber.Substring(0, indexOfSeparation));

            return arabicNumber;
        }

        private static char FindTheBiggestNumber(string romanNumber)
        {
            var theBiggest = 0;
            var result = '\0';

            for (var i = 0; i < romanNumber.Length; ++i)
            {
                if (GetArabicDigitFromRoman(romanNumber[i]) > theBiggest)
                {
                    theBiggest = GetArabicDigitFromRoman(romanNumber[i]);
                    result = romanNumber[i];
                }
            }

            return result;
        }

        //move after 'NaturalNumber(int arabicNumber)' method
        private static string ToRoman(int arabicNumber)
        {
            var result = new StringBuilder();

            foreach (var romanNumber in MainRomanCombination)
            {
                while (arabicNumber >= romanNumber.Key)
                {
                    result.Append(romanNumber.Value);
                    arabicNumber -= romanNumber.Key;
                }
            }

            return result.ToString();
        }

        public static NaturalNumber operator +(NaturalNumber a, NaturalNumber b)
        {
            var result = new NaturalNumber(a.Arabic + b.Arabic);

            return result;
        }

        public static NaturalNumber operator -(NaturalNumber a, NaturalNumber b)
        {
            if (a.Arabic - b.Arabic <= 0)
            {
                throw new InvalidOperationException("Result outside the set of natural numbers");
            }
            var result = new NaturalNumber(a.Arabic - b.Arabic);

            return result;
        }

        public static NaturalNumber operator *(NaturalNumber a, NaturalNumber b)
        {
            var result = new NaturalNumber(a.Arabic * b.Arabic);

            return result;
        }
    }
}
