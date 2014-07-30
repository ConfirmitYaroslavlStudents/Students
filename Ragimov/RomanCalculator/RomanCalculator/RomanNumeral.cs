using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RomanCalculator
{
    public class RomanNumeral
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

        private static readonly Dictionary<int, string> DecimalToRoman = new Dictionary<int, string>
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

        public RomanNumeral(string roman)
        {
            Roman = roman;
            Value = Parse(roman);
        }

        public string Roman { get; set; }
        public int Value { get; set; }

        private static int Parse(string roman)
        {
            var val = 0;
            var l = roman.Length;
            var equalInARow = 0;
            for (var i = 0; i < l - 1; i++)
            {
                if (!DigitValues.ContainsKey(roman[i])) throw new InvalidDataException("Недопустимый символ");

                if (DigitValues[roman[i]] > DigitValues[roman[i + 1]])
                {
                    val += DigitValues[roman[i]];
                    equalInARow = 0;
                }
                else if (DigitValues[roman[i]] == DigitValues[roman[i + 1]])
                {
                    val += DigitValues[roman[i]];
                    equalInARow++;
                    if (equalInARow > 3) throw new InvalidDataException("Недопустимы 4 и более одинаковых символа");
                }
                else
                {
                    if (i > 0 && DigitValues[roman[i - 1]] < DigitValues[roman[i + 1]])
                        throw new InvalidDataException("Недопустимый формат числа");

                    val -= DigitValues[roman[i]];
                    equalInARow = 0;
                }
            }
            val += DigitValues[roman[l - 1]];
            return val;
        }

        public static RomanNumeral ToRoman(int value)
        {
            var roman = new StringBuilder();
            foreach (var item in DecimalToRoman)
            {
                while (value >= item.Key)
                {
                    roman.Append(item.Value);
                    value -= item.Key;
                }
            }
            return new RomanNumeral(roman.ToString());
        }

        public static RomanNumeral operator +(RomanNumeral a, RomanNumeral b)
        {
            return ToRoman(a.Value + b.Value);
        }

        public static RomanNumeral operator -(RomanNumeral a, RomanNumeral b)
        {
            return ToRoman(a.Value - b.Value);
        }

        public static RomanNumeral operator *(RomanNumeral a, RomanNumeral b)
        {
            return ToRoman(a.Value*b.Value);
        }

        //Division is not used
        public static RomanNumeral operator /(RomanNumeral a, RomanNumeral b)
        {
            return ToRoman(a.Value/b.Value);
        }

        public static RomanNumeral Operation(RomanNumeral a, string op, RomanNumeral b)
        {
            switch (op)
            {
                case "+":
                    return a + b;
                case "-":
                    if (a.Value - b.Value > 0)
                    {
                        return a - b;
                    }
                    throw new InvalidOperationException("Вычитание за пределами множества натуральных чисел");

                case "*":
                    return a*b;
                default:
                    throw new InvalidOperationException("Неверная операция");
            }
        }

        public static RomanNumeral CalculateExpression(string expression)
        {
            char[] sep = {' '};
            var parts = expression.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 3) throw new InvalidDataException("Неверный формат выражения");
            return Operation(new RomanNumeral(parts[0]), parts[1], new RomanNumeral(parts[2]));
        }
    }
}