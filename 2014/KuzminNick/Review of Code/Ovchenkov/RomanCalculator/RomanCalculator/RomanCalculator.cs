using System;
using System.IO;

namespace RomanCalculator
{
    public static class RomanCalculator
    {
        public static NaturalNumber CalculateExpression(string expression)
        {
            var expressionParts = expression.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (expressionParts.Length != 3)
            {
                throw new InvalidDataException("Invalid expression!");
            }

            var firstNumber = new NaturalNumber(expressionParts[0]);
            var secondNumber = new NaturalNumber(expressionParts[2]);
            var operation = expressionParts[1];

            return GetResult(operation, firstNumber, secondNumber);
        }

        private static NaturalNumber GetResult(string operation, NaturalNumber firstNaturalNumber, NaturalNumber secondNaturalNumber)
        {
            NaturalNumber result;
            switch (operation)
            {
                case "+":
                    result = firstNaturalNumber + secondNaturalNumber;
                    break;
                case "-":
                    result = firstNaturalNumber - secondNaturalNumber;
                    break;
                case "*":
                    result = firstNaturalNumber*secondNaturalNumber;
                    break;
                default:
                    throw new InvalidOperationException("Invalid operation");
            }

            return result;
        }
    }
}
