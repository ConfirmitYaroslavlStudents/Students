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
                while (arabNumber != 0)
                {
                    if (arabNumber / 1000 != 0)
                    {
                        result.Append("M");
                        arabNumber -= 1000;
                    }
                    else
                    {
                        if (arabNumber / 900 != 0)
                        {
                            result.Append("CM");
                            arabNumber -= 900;
                        }
                        else
                        {
                            if (arabNumber / 500 != 0)
                            {
                                result.Append("D");
                                arabNumber -= 500;
                            }
                            else
                            {
                                if (arabNumber / 400 != 0)
                                {
                                    result.Append("CD");
                                    arabNumber -= 400;
                                }
                                else
                                {
                                    if (arabNumber / 100 != 0)
                                    {
                                        result.Append("C");
                                        arabNumber -= 100;
                                    }
                                    else
                                    {
                                        if (arabNumber / 90 != 0)
                                        {
                                            result.Append("XC");
                                            arabNumber -= 90;
                                        }
                                        else
                                        {
                                            if (arabNumber / 50 != 0)
                                            {
                                                result.Append("L");
                                                arabNumber -= 50;
                                            }
                                            else
                                            {
                                                if (arabNumber / 40 != 0)
                                                {
                                                    result.Append("XL");
                                                    arabNumber -= 40;
                                                }
                                                else
                                                {
                                                    if (arabNumber / 10 != 0)
                                                    {
                                                        result.Append("X");
                                                        arabNumber -= 10;
                                                    }
                                                    else
                                                    {
                                                        if (arabNumber / 9 != 0)
                                                        {
                                                            result.Append("IX");
                                                            arabNumber -= 9;
                                                        }
                                                        else
                                                        {
                                                            if (arabNumber / 5 != 0)
                                                            {
                                                                result.Append("V");
                                                                arabNumber -= 5;
                                                            }
                                                            else
                                                            {
                                                                if (arabNumber / 4 != 0)
                                                                {
                                                                    result.Append("IV");
                                                                    arabNumber -= 4;
                                                                }
                                                                else
                                                                {
                                                                    if (arabNumber / 1 != 0)
                                                                    {
                                                                        result.Append("I");
                                                                        arabNumber -= 1;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return result.ToString();
        }
    }
}
