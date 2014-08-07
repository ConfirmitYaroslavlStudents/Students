using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;


namespace RomeDigitLibrary
{
    public class RomeNumber : IEquatable<RomeNumber>
    {
        private readonly string _romeNumber;


        public RomeNumber(string number)
        {
            _romeNumber = number;
        }

        //можно использовать более тривиальный тип и название адекватнее
        //если придерживаться того, что входные данные корректны, то можно не делать проверки, коих много
        //переименовать i
        //сделать, чтобя сразу же переводилось в арабское число и хранилось внутри - это позволит избежать конвертации каждый раз при использовании
        public UInt32 ToUint32()
        {
            var firstRomeDigit = _romeNumber[0];
            var i = 1;
            uint result= 0;
            const char symbolEnd = '!';
            while ((firstRomeDigit != symbolEnd) && (i < _romeNumber.Length))
            {
                var secondRomeDigit = _romeNumber[i];
                RulesConvertRomeNumber.CheckExistRule(firstRomeDigit);
                RulesConvertRomeNumber.CheckExistRule(secondRomeDigit);
                if (RulesConvertRomeNumber.RulesConvertToTen[firstRomeDigit] >= RulesConvertRomeNumber.RulesConvertToTen[secondRomeDigit])
                {
                    result += RulesConvertRomeNumber.RulesConvertToTen[firstRomeDigit];
                    firstRomeDigit = secondRomeDigit;
                }
                else
                {
                    RulesConvertRomeNumber.CheckExistRule(firstRomeDigit + secondRomeDigit.ToString(CultureInfo.InvariantCulture));
                    result += RulesConvertRomeNumber.RulesConvertToTen[secondRomeDigit] - RulesConvertRomeNumber.RulesConvertToTen[firstRomeDigit];
                    firstRomeDigit = symbolEnd;
                    if (i < _romeNumber.Length - 1)
                    {
                        ++i;
                        firstRomeDigit = _romeNumber[i];
                    }
                }
                ++i;
            }
            if (firstRomeDigit != symbolEnd)
            {
                RulesConvertRomeNumber.CheckExistRule(firstRomeDigit);
                result += RulesConvertRomeNumber.RulesConvertToTen[firstRomeDigit];
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

       
        public static RomeNumber ConvertUIntToRomeNumber(uint number)
        {
            var result = "";
            while (number != 0)
            {
                var incompleteNumber = MaximumMathPowerTen(number);
                if (RulesConvertRomeNumber.RulesConvertToRome.ContainsKey(incompleteNumber))
                {
                    result += RulesConvertRomeNumber.RulesConvertToRome[incompleteNumber];
                    number -= incompleteNumber;
                    continue;
                }
                var approximateNumber = RulesConvertRomeNumber.RulesConvertToRome.FirstOrDefault(rule => IsAproximateEqualOne(incompleteNumber, rule)).Key;
                if (approximateNumber != 0)
                {
                    result += RulesConvertRomeNumber.RulesConvertToRome[approximateNumber];
                    number -= approximateNumber;
                }
                else
                {
                    approximateNumber = RulesConvertRomeNumber.RulesConvertToRome.Last(rule => IsAproximateBeforeTen(incompleteNumber, rule.Key)).Key;
                    for (var j = 0; j < incompleteNumber / approximateNumber; ++j)
                    {
                        result += RulesConvertRomeNumber.RulesConvertToRome[approximateNumber];
                        number -= approximateNumber;
                    }
                }
            }
            return new RomeNumber(result);
        }

        
        private static uint MaximumMathPowerTen(uint number)
        {
            var firstDigit = FirstSymbolToUInt(number);
            var tens = AddSymbolZero(number.ToString(CultureInfo.InvariantCulture).Length - 1);
            firstDigit *= uint.Parse(tens);
            return firstDigit;
        }

        private static uint FirstSymbolToUInt(uint number)
        {
            var firstSymbol = number.ToString(CultureInfo.InvariantCulture).First().ToString(CultureInfo.InvariantCulture);
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
            var tens = "1";
            for (var j = 0; j < number; ++j)
            {
                tens += "0";
            }
            return tens;
        }
        
    }
}
