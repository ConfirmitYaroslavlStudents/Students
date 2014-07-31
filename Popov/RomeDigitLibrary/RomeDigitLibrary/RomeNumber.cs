using System;
using System.Collections.Generic;
using System.Linq;


namespace RomeDigitLibrary
{
    public class RomeNumber : IEquatable<RomeNumber>
    {
        private readonly string _romeNumber;
        private readonly static Dictionary<uint, string> _rulesConvertToRome;
        private readonly static Dictionary<char, uint> _rulesConvertToTen;


        static Dictionary<uint, string> RulesConvertToRome 
        {
            get { return _rulesConvertToRome; }
        }

        static Dictionary<char, uint> RulesConvertToTen 
        {
            get { return _rulesConvertToTen; }
        }
        

        static RomeNumber()
        {
            _rulesConvertToRome = new Dictionary<uint, string> 
            { 
            { 1, "I" },{ 4, "IV"},{ 5, "V" },
            { 9, "IX"},{10, "X"},{40, "XL"},
            {50, "L"},{90, "XC"},{100, "C"},
            {400, "CD"},{500, "D"},{900, "CM"},
            {1000, "M"}
            };

            _rulesConvertToTen = new Dictionary<char, uint>
            {
                {'I', 1},{'V', 5},{'X', 10},
                {'L', 50},{'C', 100},{'D', 500},
                {'M', 1000}
            };
        }

        public RomeNumber(string number)
        {
            _romeNumber = number;
        }


        public UInt32 ToUint32()
        {
            var firstRomeDigit = _romeNumber[0];
            var i = 1;
            uint result= 0;
            while ((firstRomeDigit != '!') && (i < _romeNumber.Length))
            {
                var secondRomeDigit = _romeNumber[i];
                CheckExistValueRulesConvertToTen(firstRomeDigit);
                CheckExistValueRulesConvertToTen(secondRomeDigit);
                if (RulesConvertToTen[firstRomeDigit] >= RulesConvertToTen[secondRomeDigit])
                {
                    result += RulesConvertToTen[firstRomeDigit];
                    firstRomeDigit = secondRomeDigit;
                }
                else
                {
                    CheckExistRule(firstRomeDigit + secondRomeDigit.ToString());
                    result += RulesConvertToTen[secondRomeDigit] - RulesConvertToTen[firstRomeDigit];
                    firstRomeDigit = '!';
                    if (i < _romeNumber.Length - 1)
                    {
                        ++i;
                        firstRomeDigit = _romeNumber[i];
                    }
                }
                ++i;
            }
            if (firstRomeDigit != '!')
            {
                CheckExistValueRulesConvertToTen(firstRomeDigit);
                result += RulesConvertToTen[firstRomeDigit];
            }
            return result;
        }


        public override string ToString()
        {
            return _romeNumber;
        }

        public override bool Equals(object obj)
        {
            var other = obj as RomeNumber;
            return other != null && Equals(other);
        }
        
        public bool Equals(RomeNumber other)
        {
            return _romeNumber.SequenceEqual(other._romeNumber);
        }


        public static RomeNumber operator +(RomeNumber number1, RomeNumber number2)
        {
            var result = number1.ToUint32() + number2.ToUint32();
            return ConvertUIntToRomeNumber(result);
        }

        public static RomeNumber operator -(RomeNumber number1, RomeNumber number2)
        {
            var result = number1.ToUint32() - number2.ToUint32();
            if (result > 0)
            {
                return ConvertUIntToRomeNumber(result);
            }
            throw new InvalidOperationException("Converting in Roman numbers does not support negative values");
        }

        public static RomeNumber operator *(RomeNumber number1, RomeNumber number2)
        {
            var result = number1.ToUint32() * number2.ToUint32();
            return ConvertUIntToRomeNumber(result);
        }

        public static RomeNumber operator /(RomeNumber number1, RomeNumber number2)
        {
            var result = number1.ToUint32() / number2.ToUint32();
            if (result != 0)
            {
                return ConvertUIntToRomeNumber(result);
            }
            throw new ContextMarshalException("The conversion of zero in Roman numbers are not supported");
        }

        
        private static void CheckExistValueRulesConvertToTen(char key)
        {
            if (!RulesConvertToTen.ContainsKey(key))
            {
                throw new FormatException("Incorrect entry of the Roman number");
            }
        }

        private static void CheckExistRule(string rule)
        {
            if (!RulesConvertToRome.ContainsValue(rule))
                throw new FormatException("Incorrect entry of the Roman number");
        }


        public static RomeNumber ConvertUIntToRomeNumber(uint number)
        {
            var result = "";
            while (number != 0)
            {
                var firstDigit = uint.Parse(number.ToString().First().ToString());
                var tens = AddSymbolZero(number.ToString().Length - 1);
                firstDigit *= uint.Parse(tens);
                if (RulesConvertToRome.ContainsKey(firstDigit))
                {
                    result += RulesConvertToRome[firstDigit];
                    number -= firstDigit;
                    continue;
                }
                var approximateNumber = RulesConvertToRome.FirstOrDefault(rule => firstDigit / rule.Key == 1).Key;
                if (approximateNumber != 0)
                {
                    result += RulesConvertToRome[approximateNumber];
                    number -= approximateNumber;
                }
                else
                {
                    approximateNumber = RulesConvertToRome.Last(rule => IsAproximate(firstDigit, rule.Key)).Key;
                    for (var j = 0; j < firstDigit / approximateNumber; ++j)
                    {
                        result += RulesConvertToRome[approximateNumber];
                        number -= approximateNumber;
                    }
                }
            }
            return new RomeNumber(result);
        }


        private static bool IsAproximate(uint nominator, uint denominator)
        {
            return ((nominator / denominator > 1) && (nominator / denominator < 10));
        }

        /// <param name="number">Number zero</param>
        /// <returns></returns>
        private static string AddSymbolZero(int number)
        {
            var tens = "1";
            for (var j = 0; j < number; ++j)
            {
                tens += "0";
            }
            return tens;
        }

        
    }
    
}
