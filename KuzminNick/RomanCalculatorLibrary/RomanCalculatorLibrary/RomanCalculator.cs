using System;
using System.Collections.Generic;

namespace RomanCalculatorLibrary
{
    public class RomanCalculator
    {
        private readonly ArithmeticExpressionParser _arithmeticExpressionParser = new ArithmeticExpressionParser();

        public string CalculateExpression(string expression)
        {
            var reversePolishSignature = _arithmeticExpressionParser.ConvertInputStringToReversePolishSignature(expression);
            var resultInArarbicFormat = GetResultOfCalculationInArabicFormat(reversePolishSignature);

            if(resultInArarbicFormat <= 0)
                throw new ArgumentException("Not positive result of calculation");

            var resultInRomatFormat = _arithmeticExpressionParser.ConverterOfNumbers.ConvertArabicNumberToRoman(resultInArarbicFormat);

            return resultInRomatFormat;
        }

        private int GetResultOfCalculationInArabicFormat(string reversePolishSignature)
        {
            var stack = new Stack<string>();
            var resultOfCurrentOperation = 0;
            var listElementsOfExpression = _arithmeticExpressionParser.InitializeListOfElements(reversePolishSignature);

            foreach (var elementOfExpression in listElementsOfExpression)
            {
                if (!ArithmeticExpressionParser.IsSignOfOperation(elementOfExpression))
                {
                    stack.Push(elementOfExpression);
                }
                else
                {
                    resultOfCurrentOperation = CalculateCurrentOperation(stack, elementOfExpression, resultOfCurrentOperation);
                    stack.Push(resultOfCurrentOperation.ToString());
                }
            }
            return int.Parse(stack.Pop());
        }

        private static int CalculateCurrentOperation(Stack<string> stack, string symbolOfOperation, int resultOfCurrentOperation)
        {
            int secondNumber, firstNumber;
            var isCorrectParsedSecondNumber = int.TryParse(stack.Pop(), out secondNumber);
            var isCorrectParsedFirstNumber = int.TryParse(stack.Pop(), out firstNumber);
            if(!isCorrectParsedFirstNumber || !isCorrectParsedSecondNumber)
                throw new InvalidOperationException("Uncorrect Arithmetic Expression");

            switch (symbolOfOperation)
            {
                case "+":
                    resultOfCurrentOperation = firstNumber + secondNumber;
                    break;
                case "-":
                    resultOfCurrentOperation = firstNumber - secondNumber;
                    break;
                case "*":
                    resultOfCurrentOperation = firstNumber*secondNumber;
                    break;
                case "/":
                {
                    if (firstNumber%secondNumber != 0)
                        throw new ArgumentException("The result of division is not integer");

                    resultOfCurrentOperation = firstNumber/secondNumber;
                    break;
                }
                case "^":
                    resultOfCurrentOperation = (int) Math.Pow(firstNumber, secondNumber);
                    break;
            }
            return resultOfCurrentOperation;
        }
    }
}
