using System;
using System.Collections.Generic;

namespace RomanCalculatorLibrary
{
    public class RomanCalculator
    {
        private readonly ParserArithmeticExpression _parser = new ParserArithmeticExpression();

        public string CalculateExpression(string expression)
        {
            var reversePolishSignature = _parser.ConvertInputStringToReversePolishSignature(expression);
            var resultInArarbicFormat = GetResultOfCalculationInArabicFormat(reversePolishSignature);

            if(resultInArarbicFormat <= 0)
                throw new ArgumentException("Not positive result of calculation");

            var resultInRomatFormat = _parser.ConvertArabicNumberToRoman(resultInArarbicFormat);

            return resultInRomatFormat;
        }

        private int GetResultOfCalculationInArabicFormat(string reversePolishSignature)
        {
            var stack = new Stack<string>();
            var resultOfCurrentOperation = 0;
            var listElementsOfExpression = _parser.InitializeListOfElements(reversePolishSignature);

            foreach (var elementOfExpression in listElementsOfExpression)
            {
                if (!ParserArithmeticExpression.IsSignOfOperation(elementOfExpression))
                {
                    stack.Push(elementOfExpression);
                }
                else
                {
                    resultOfCurrentOperation = CalculateCurrentOperation(stack, elementOfExpression, resultOfCurrentOperation);
                    stack.Push(resultOfCurrentOperation.ToString()); //Need invariant culture
                }
            }
            return int.Parse(stack.Pop());
        }

        private static int CalculateCurrentOperation(Stack<string> stack, string symbolOfOperation, int resultOfCurrentOperation)
        {
            int secondNumber, firstNumber;
           //Try/catch is slow, it is better to handle error using if/etc and then throw exception
            try
            {
                secondNumber = int.Parse(stack.Pop());
                firstNumber = int.Parse(stack.Pop());
            }
            catch
            {
                throw new InvalidOperationException("Uncorrect Arithmetic Expression");
            }

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
