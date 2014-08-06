using System;
using System.IO;

namespace RomanCalculator
{
    public class RomanCalc
    {
        public RomanNumeral Operation(RomanNumeral a, string op, RomanNumeral b)
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
                    return a * b;
                default:
                    throw new InvalidOperationException("Неверная операция");
            }
        }

        public RomanNumeral CalculateExpression(string expression)
        {
            char[] sep = { ' ' };
            var parts = expression.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 3) throw new InvalidDataException("Неверный формат выражения");
            return Operation(new RomanNumeral(parts[0]), parts[1], new RomanNumeral(parts[2]));
        }
    }
}
