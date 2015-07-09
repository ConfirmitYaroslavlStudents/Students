using System.Collections.Generic;
using System.Text;
using System.IO;

namespace RomanCalculator
{
    public class RomanNumeral
    {
        private readonly Dictionary<char, int> _digitValues = new Dictionary<char, int>
        {
            {'I', 1},
            {'V', 5},
            {'X', 10},
            {'L', 50},
            {'C', 100},
            {'D', 500},
            {'M', 1000}
        };

        private readonly Dictionary<int, string> _decimalToRoman = new Dictionary<int, string>
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

        private int Parse(string roman)
        {
            var val = 0;
            //Change to 'lengthOfRomanNumber'
            var l = roman.Length;
            var equalInARow = 0;
            for (var i = 0; i < l - 1; i++)
            {
                if (!_digitValues.ContainsKey(roman[i])) throw new InvalidDataException("Недопустимый символ");

                if (_digitValues[roman[i]] > _digitValues[roman[i + 1]])
                {
                    val += _digitValues[roman[i]];
                    equalInARow = 0;
                }
                else if (_digitValues[roman[i]] == _digitValues[roman[i + 1]])
                {
                    val += _digitValues[roman[i]];
                    equalInARow++;
                    if (equalInARow > 3) throw new InvalidDataException("Недопустимы 4 и более одинаковых символа");
                }
                else
                {
                    if (i > 0 && _digitValues[roman[i - 1]] < _digitValues[roman[i + 1]])
                        throw new InvalidDataException("Недопустимый формат числа");

                    val -= _digitValues[roman[i]];
                    equalInARow = 0;
                }
            }
            val += _digitValues[roman[l - 1]];
            return val;
        }

        public RomanNumeral ToRoman(int value)
        {
            var roman = new StringBuilder();
            foreach (var item in _decimalToRoman)
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
            return a.ToRoman(a.Value + b.Value);
        }

        public static RomanNumeral operator -(RomanNumeral a, RomanNumeral b)
        {
            return a.ToRoman(a.Value - b.Value);
        }

        public static RomanNumeral operator *(RomanNumeral a, RomanNumeral b)
        {
            return a.ToRoman(a.Value*b.Value);
        }

        //Delete unused operator
        public static RomanNumeral operator /(RomanNumeral a, RomanNumeral b)
        {
            return a.ToRoman(a.Value/b.Value);
        }

    }
}