using System.Collections.Generic;
using System.Text;
using RomaCalculator.KindsOfOperators;

namespace RomaCalculator
{
    public class Calculator
    {
        public string Calculate(string operandA, string operandB, IOperator operatorForAAndB)
        {

            var A = ConvertFromRomanToArabNumber(operandA);
            var B = ConvertFromRomanToArabNumber(operandB);

            var answerForExpression = operatorForAAndB.CalculateIt(A, B);

            return ConvertFromArabToRomanNumber(answerForExpression);
        }

        public int ConvertFromRomanToArabDigit(char romanDigit)
        {
            switch (romanDigit)
            {
                case 'I':
                    return 1;
                case 'V':
                    return 5;
                case 'X':
                    return 10;
                case 'L':
                    return 50;
                case 'C':
                    return 100;
                case 'D':
                    return 500;
                case 'M':
                    return 1000;
                default:
                    return int.MaxValue;
            }
        }
        public int ConvertFromRomanToArabNumber(string romanNumber)
        {
            int result = 0;
            int previousCount = 0;
            var abacus = new List<int>();

            for (int i = 0; i < romanNumber.Length; i++)
            {
                abacus.Add(ConvertFromRomanToArabDigit(romanNumber[i]));
            }
            while (abacus.Count != 0)
            {
                previousCount = abacus.Count;
                for (int i = 0; i < abacus.Count - 1; i++)
                {
                    if (abacus[i] < abacus[i + 1])
                    {
                        abacus[i] = abacus[i + 1] - abacus[i];
                        abacus.RemoveAt(i + 1);
                        i = abacus.Count;
                    }
                }
                if (abacus.Count == previousCount)
                {
                    foreach (var item in abacus)
                    {
                        result += item;
                    }
                    abacus.Clear();
                }
            }
            return result;
        }

        public string ConvertFromArabToRomanNumber(int arabNumber)
        {
            var result = new StringBuilder();
            if (arabNumber == 0)
            {
                result.Append("zero");
            }
            else
            {
                if (arabNumber < 0)
                {
                    result.Append("-");
                    arabNumber = -arabNumber;
                }
                var arab = new int[] { 1, 4, 5, 9, 10, 40, 50, 90, 100, 400, 500, 900, 1000 };
                var roman = new string[] { "I", "IV", "V", "IX", "X", "XL", "L", "XC", "C", "CD", "D", "CM", "M" };
                var index = 0;
                while (arabNumber != 0)
                {
                    index = 0;
                    while ((index != 13) && (arabNumber >= arab[index]))
                    {
                        index++;
                    }
                    arabNumber -= arab[index - 1];
                    result.Append(roman[index - 1]);
                }
            }
            return result.ToString();
        }
    }
}
