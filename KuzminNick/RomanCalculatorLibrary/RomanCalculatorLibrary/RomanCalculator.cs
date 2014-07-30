using System;
using System.Collections.Generic;
using ParsingInputDataOfCalculator;

namespace RomanCalculatorLibrary
{
    public class RomanCalculator
    {
        private readonly ParserArithmeticExpression _parser = new ParserArithmeticExpression();

        public string CalculateExpression(string expression)
        {
            var reversePolishSignature = _parser.ConvertInputStringToReversePolishSignature(expression);

            var stack = new Stack<string>();
            var resultOfCurrentOperation = 0;
            var resultInArarbicFormat = 0;

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
                    stack.Push(resultOfCurrentOperation.ToString());
                }
            }

            resultInArarbicFormat = int.Parse(stack.Pop());
            if(resultInArarbicFormat <= 0)
                throw new ArgumentException("Not positive result of calculation");

            var resultInRomatFormat = _parser.ConvertArabicNumberToRoman(resultInArarbicFormat);

            return resultInRomatFormat;
        }

        private static int CalculateCurrentOperation(Stack<string> stack, string elementOfExpression, int resultOfCurrentOperation)
        {
            int secondNumber, firstNumber = 0;
            try
            {
                secondNumber = int.Parse(stack.Pop());
                firstNumber = int.Parse(stack.Pop());
            }
            catch
            {
                throw new InvalidOperationException("Uncorrect Arithmetic Expression");
            }
            switch (elementOfExpression)
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
                        throw new ArgumentException();

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
