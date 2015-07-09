using System;
using System.Collections.Generic;
using System.Linq;

namespace RomanCalculatorLibrary
{
    public class ArithmeticExpressionParser
    {
        private readonly ConverterOfNumbers _converterOfNumbers = new ConverterOfNumbers();

        private class CharacterElement
        {
            public string CharValue { get; private set; }
            public TypeOfSign TypeOfSign { get; private set; }

            public CharacterElement(String charValue, TypeOfSign typeOfSign)
            {
                CharValue = charValue;
                TypeOfSign = typeOfSign;
            }
        }

        public enum TypeOfSign
        {
            OpeningBracket = 0,
            ClosingBracket = 1,
            AdditionSubtraction = 2,
            MultiplicationDivision = 3,
            Involution = 4,
            Letter = 5
        }

        public ConverterOfNumbers ConverterOfNumbers
        {
            get { return _converterOfNumbers; }
        }

        public String ConvertInputStringToReversePolishSignature(String expression)
        {
            if (!IsCorrectAmountOfBrackets(expression) || IsContainTerminalSign(expression))
                throw new ArgumentException("Uncorrect Format of expression");

            var listOfCharacterElements = GetAllCharacterElementsOfInputString(expression);

            var stack = new Stack<CharacterElement>();
            var resultingReversePolishSignature = String.Empty;
            var isPreviousCharIsLetter = false;
            foreach (var item in listOfCharacterElements)
            {
                resultingReversePolishSignature += ParsingCurrentCharacterElement(item, stack, ref isPreviousCharIsLetter);
            }

            AddRemainingCharacterElementsFromStack(stack, ref resultingReversePolishSignature);

            return resultingReversePolishSignature;
        }

        private Boolean IsCorrectAmountOfBrackets(String expression)
        {
            var isCorrectInputData = false;

            var amountOfClosedBrackets = expression.Count(x => x == ')');
            var amountOfOpenBrackets = expression.Count(x => x == '(');

            if (amountOfClosedBrackets == amountOfOpenBrackets)
                isCorrectInputData = true;

            return isCorrectInputData;
        }

        private Boolean IsContainTerminalSign(String expression)
        {
            return expression.Contains("$");
        }

        private IEnumerable<CharacterElement> GetAllCharacterElementsOfInputString(string inputArrayElements)
        {
            var listOfCharacterElements = new List<CharacterElement>();
            for (var i = 0; i < inputArrayElements.Length; i++)
            {
                var characterElement = new CharacterElement(inputArrayElements[i].ToString(), GetTypeOfSign(inputArrayElements[i].ToString()));
                listOfCharacterElements.Add(characterElement);
            }
            return listOfCharacterElements;
        }

        private TypeOfSign GetTypeOfSign(String currentChar)
        {
            switch (currentChar)
            {
                case "(":
                    return TypeOfSign.OpeningBracket;
                case ")":
                    return TypeOfSign.ClosingBracket;
                case "+":
                case "-":
                    return TypeOfSign.AdditionSubtraction;
                case "*":
                case "/":
                    return TypeOfSign.MultiplicationDivision;
                case "^":
                    return TypeOfSign.Involution;
                default:
                    return TypeOfSign.Letter;
            }
        }

        private void AddRemainingCharacterElementsFromStack(Stack<CharacterElement> stack,
            ref string resultingReversePolishSignature)
        {
            while (stack.Count != 0)
                resultingReversePolishSignature += stack.Pop().CharValue;
        }

        private string ParsingCurrentCharacterElement(CharacterElement item,
            Stack<CharacterElement> stack, ref bool isPreviousCharIsLetter)
        {
            var resultingReversePolishSignature = String.Empty;
            var termnalSign = String.Empty;

            if (item.TypeOfSign == TypeOfSign.Letter)
            {
                if (!isPreviousCharIsLetter)
                {
                    termnalSign = "$";
                    isPreviousCharIsLetter = true;
                }

                resultingReversePolishSignature += termnalSign + item.CharValue;
            }
            else
            {
                if (isPreviousCharIsLetter)
                {
                    resultingReversePolishSignature += "$";
                }

                isPreviousCharIsLetter = false;

                if (stack.Count == 0)
                {
                    stack.Push(item);
                }
                else if (item.CharValue == ")")
                {
                    while (true)
                    {
                        if (stack.Peek().CharValue == "(")
                        {
                            stack.Pop();
                            break;
                        }
                        if (PeekOfStackIsBracket(stack))
                        {
                            resultingReversePolishSignature += stack.Peek().CharValue;
                        }

                        stack.Pop();
                    }
                }
                else if (item.TypeOfSign == TypeOfSign.OpeningBracket || (item.TypeOfSign > stack.Peek().TypeOfSign))
                {
                    stack.Push(item);
                }
                else if (item.TypeOfSign <= stack.Peek().TypeOfSign)
                {
                    while (item.TypeOfSign <= stack.Peek().TypeOfSign)
                    {
                        resultingReversePolishSignature += stack.Pop().CharValue;
                    }
                    stack.Push(item);
                }
            }
            return resultingReversePolishSignature;
        }

        private bool PeekOfStackIsBracket(Stack<CharacterElement> stack)
        {
            return stack.Peek().CharValue != ")" && stack.Peek().CharValue != "(";
        }

        public List<string> InitializeListOfElements(string reversePolishSignature)
        {
            var listElementsOfExpression = new List<string>();
            const char terminalSign = '$';

            for (var i = 0; i < reversePolishSignature.Length; i++)
            {
                if (reversePolishSignature[i] == terminalSign && i != reversePolishSignature.Length - 1)
                {
                    var currentRomanNumber = ParseCurrentRomanNumber(ref i, reversePolishSignature, terminalSign);
                    var arabicNumber = _converterOfNumbers.ConvertRomanNumberToArabic(currentRomanNumber).ToString();
                    listElementsOfExpression.Add(arabicNumber);
                }
                else if (IsSignOfOperation(reversePolishSignature[i].ToString()))
                {
                    listElementsOfExpression.Add(reversePolishSignature[i].ToString());
                }
            }

            return listElementsOfExpression;
        }

        private string ParseCurrentRomanNumber(ref int i, string reversePolishSignature, char terminalSign)
        {
            var currentElementOfExpression = String.Empty;
            do
            {
                i++;
                if (reversePolishSignature[i] != terminalSign)
                    currentElementOfExpression += reversePolishSignature[i];
            } while (reversePolishSignature[i] != terminalSign);

            return currentElementOfExpression;
        }

        public static bool IsSignOfOperation(string sign)
        {
            switch (sign)
            {
                case "+": return true;
                case "-": return true;
                case "*": return true;
                case "/": return true;
                case "^": return true;
            }

            return false;
        }
    }
}
