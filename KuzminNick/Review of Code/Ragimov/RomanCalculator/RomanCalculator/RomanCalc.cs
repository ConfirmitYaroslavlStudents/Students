using System;
using System.IO;

namespace RomanCalculator
{
    // 'RomanCalculator' - unshortcut variant of class name
    public class RomanCalc
    {
        // romanNumberalOne, romanNumberalTwo/ romanNumberal1, romanNumberal2 - according to specification
        // of Resharper
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
            // Perhaps, it's better to use 'separatingSign', 'separatingSymbol', 'terminalSign', 'terminalSymbol'
            // Change char-array on constant char-value
            char[] sep = { ' ' };
            var parts = expression.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 3) throw new InvalidDataException("Неверный формат выражения");
            return Operation(new RomanNumeral(parts[0]), parts[1], new RomanNumeral(parts[2]));
        }
    }
}
