using System;
using System.IO;

namespace RomanCalculator
{
    public static class RomanCalculator
    {
        public static string CalculateExpression(string expression)
        {
            var expressionParts = expression.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (expressionParts.Length != 3)
            {
                throw new InvalidDataException("Invalid expression!");
            }

            var firstNumber = Parser.ToArabic(expressionParts[0]);
            var secondNumber = Parser.ToArabic(expressionParts[2]);
            var operation = expressionParts[1];

            return GetResult(operation, firstNumber, secondNumber);
        }

        private static string GetResult(string operation, int firstNumber, int secondNumber)
        {
            int result;
            switch (operation)
            {
                case "+":
                    result = firstNumber + secondNumber;
                    break;
                case "-":
                    if (firstNumber - secondNumber <= 0)
                    {
                        throw new InvalidOperationException("Result outside the set of natural numbers");
                    }
                    result = firstNumber - secondNumber;
                    break;
                case "*":
                    result = firstNumber*secondNumber;
                    break;
                default:
                    throw new InvalidOperationException("Invalid operation");
            }

            return Parser.ToRoman(result);
        }
    }
}
