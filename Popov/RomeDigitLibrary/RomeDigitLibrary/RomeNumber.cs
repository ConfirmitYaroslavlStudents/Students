using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;


namespace RomeDigitLibrary
{
    public class RomeNumber : IEquatable<RomeNumber>
    {
        private readonly string _romeNumber;
        public uint ArabNumber { get; private set; }


        public RomeNumber(string number)
        {
            _romeNumber = number;
            ArabNumber = ToUint32();
        }

       
        private UInt32 ToUint32()
        {
            char firstRomeDigit = _romeNumber[0];
            int i = 1;
            uint result= 0;
            const char symbolEnd = '!';
            bool noEnd = (firstRomeDigit != symbolEnd) && (i < _romeNumber.Length);

            while (noEnd)
            {
                char secondRomeDigit = _romeNumber[i];
                RulesConvertRomeNumber.CheckExistRule(firstRomeDigit);
                RulesConvertRomeNumber.CheckExistRule(secondRomeDigit);
                if (RulesConvertRomeNumber.RulesConvertToArab[firstRomeDigit] >= RulesConvertRomeNumber.RulesConvertToArab[secondRomeDigit])
                {
                    result += RulesConvertRomeNumber.RulesConvertToArab[firstRomeDigit];
                    firstRomeDigit = secondRomeDigit;
                }
                else
                {
                    RulesConvertRomeNumber.CheckExistRule(firstRomeDigit + secondRomeDigit.ToString(CultureInfo.InvariantCulture));
                    result += RulesConvertRomeNumber.RulesConvertToArab[secondRomeDigit] - RulesConvertRomeNumber.RulesConvertToArab[firstRomeDigit];
                    firstRomeDigit = symbolEnd;
                    if (i < _romeNumber.Length - 1)
                    {
                        ++i;
                        firstRomeDigit = _romeNumber[i];
                    }

                }
                
                ++i;
                noEnd = (firstRomeDigit != symbolEnd) && (i < _romeNumber.Length);
            }

            noEnd = firstRomeDigit != symbolEnd;
            if (noEnd)
            {
                RulesConvertRomeNumber.CheckExistRule(firstRomeDigit);
                result += RulesConvertRomeNumber.RulesConvertToArab[firstRomeDigit];
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
            uint result = number1.ToUint32() + number2.ToUint32();
            return ConvertUIntToRomeNumber(result);
        }

        public static RomeNumber operator -(RomeNumber number1, RomeNumber number2)
        {
            uint result = number1.ToUint32() - number2.ToUint32();
            if (result > 0)
            {
                return ConvertUIntToRomeNumber(result);
            }
            throw new InvalidOperationException("Converting in Roman numbers does not support negative values");
        }

        public static RomeNumber operator *(RomeNumber number1, RomeNumber number2)
        {
            uint result = number1.ToUint32() * number2.ToUint32();
            return ConvertUIntToRomeNumber(result);
        }

       
        public static RomeNumber ConvertUIntToRomeNumber(uint number)
        {
            var result = new StringBuilder();
            while (number != 0)
            {
                uint incompleteNumber = MaximumMathPowerTen(number);
                if (RulesConvertRomeNumber.RulesConvertToRome.ContainsKey(incompleteNumber))
                {
                    result.Append(RulesConvertRomeNumber.RulesConvertToRome[incompleteNumber]);
                    number -= incompleteNumber;
                    continue;
                }
                uint approximateNumber = RulesConvertRomeNumber.RulesConvertToRome.FirstOrDefault(rule => IsAproximateEqualOne(incompleteNumber, rule)).Key;
                if (approximateNumber != 0)
                {
                    result.Append(RulesConvertRomeNumber.RulesConvertToRome[approximateNumber]);
                    number -= approximateNumber;
                }
                else
                {
                    approximateNumber = RulesConvertRomeNumber.RulesConvertToRome.Last(rule => IsAproximateBeforeTen(incompleteNumber, rule.Key)).Key;
                    for (int j = 0; j < incompleteNumber / approximateNumber; ++j)
                    {
                        result.Append(RulesConvertRomeNumber.RulesConvertToRome[approximateNumber]);
                        number -= approximateNumber;
                    }
                }
            }
            return new RomeNumber(result.ToString());
        }

        


        private static uint MaximumMathPowerTen(uint number)
        {
            uint firstDigit = FirstSymbolToUInt(number);
            string tens = AddSymbolZero(number.ToString(CultureInfo.InvariantCulture).Length - 1);
            firstDigit *= uint.Parse(tens);
            return firstDigit;
        }

        private static uint FirstSymbolToUInt(uint number)
        {
            string firstSymbol = number.ToString(CultureInfo.InvariantCulture).First().ToString(CultureInfo.InvariantCulture);
            return uint.Parse(firstSymbol);
        }

        private static bool IsAproximateEqualOne(uint incompleteNumber, KeyValuePair<uint, string> rule)
        {
            return incompleteNumber / rule.Key == 1;
        }

        private static bool IsAproximateBeforeTen(uint nominator, uint denominator)
        {
            return ((nominator / denominator > 1) && (nominator / denominator < 10));
        }

        private static string AddSymbolZero(int number)
        {
            string tens = "1";
            for (int j = 0; j < number; ++j)
            {
                tens += "0";
            }
            return tens;
        }
        
    }
}
