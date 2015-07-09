using System;
using System.Collections.Generic;
using System.Linq;

namespace RomanCalculatorLibrary
{
    //I recommend your to divide this "superclass" into several small classes with different rensposibilities
    public class ParserArithmeticExpression //I would prefer name like ArithmeticExpressionParser
    {
        private class CharacterElement
        {
            public string CharValue { get; private set; }
            public TypeOfSign TypeOfSign { get; private set; }

            public CharacterElement(String charValue, TypeOfSign typeOfSign) //Here we have "String" and property is "string" (low case)
            //We are refering to an object here and should use "string" - low case
            //See usage recomendations for String and string below:

            //string is an alias for System.String. So technically, there is no difference. It's like int vs. System.Int32.
            //As far as guidelines, I think it's generally recommended to use string any time you're referring to an object.
            //e.g. string place = "world";
            //Likewise, I think it's generally recommended to use String if you need to refer specifically to the class.
            //e.g.  string greet = String.Format("Hello {0}!", place);
            //This is the style that Microsoft tends to use in their examples.
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
            //See Re#
            if (expression.Contains("$"))
                return true;

            return false;
        }

        //Due to our last lection we can use IEnumerable as Re# suggests (Not critical)
        private List<CharacterElement> GetAllCharacterElementsOfInputString(string inputArrayElements)
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
                resultingReversePolishSignature += stack.Pop().CharValue; //So many polish signature methods here, I think polish signature parser should be placed in class library
        }

        //Bellow we have so many low-quality code from lab made in first year of university. I don't think I can take it anymore.
        //Code bellow has not been inspected due to this reasons

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
                    var arabicNumber = ConvertRomanNumberToArabic(currentRomanNumber).ToString();
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

        public int ConvertRomanNumberToArabic(string romanFormatNumber)
        {
            var result = 0;

            var stack = new Stack<int>();
            var listOfArabicDigits = new List<int>();
            foreach (var currentCharOfRomanNumber in romanFormatNumber)
                listOfArabicDigits.Add(ConvertRomanDigitToArabic(currentCharOfRomanNumber));

            foreach (var currentArabicDigit in listOfArabicDigits)
            {
                if (stack.Count != 0)
                {
                    if (currentArabicDigit < stack.Peek())
                    {
                        result += stack.Sum();
                        stack.Clear();
                        stack.Push(currentArabicDigit);
                    }
                    else if (currentArabicDigit == stack.Peek())
                    {
                        stack.Push(currentArabicDigit);
                    }
                    else
                    {
                        var subtrahend = stack.Sum();
                        var minuend = currentArabicDigit;
                        var currentArabicDigitAfterSubtraction = minuend - subtrahend;
                        result += currentArabicDigitAfterSubtraction;
                        stack.Clear();
                    }
                }
                else
                {
                    stack.Push(currentArabicDigit);
                }
            }

            var remainingDigits = stack.Sum();
            result += remainingDigits;

            return result;
        }

        private int ConvertRomanDigitToArabic(char firstDigit)
        {
            if (firstDigit == 'M')
                return 1000;
            if (firstDigit == 'D')
                return 500;
            if (firstDigit == 'C')
                return 100;
            if (firstDigit == 'L')
                return 50;
            if (firstDigit == 'X')
                return 10;
            if (firstDigit == 'V')
                return 5;
            if (firstDigit == 'I')
                return 1;

            throw new ArgumentException("Uncorrect input data");
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

        public string ConvertArabicNumberToRoman(int number)
        {
            var result = String.Empty;
            int[] digitsValues = { 1, 4, 5, 9, 10, 40, 50, 90, 100, 400, 500, 900, 1000 };
            string[] romanDigits = { "I", "IV", "V", "IX", "X", "XL", "L", "XC", "C", "CD", "D", "CM", "M" };
            while (number > 0)
            {
                var indexMaxDivisorOfCurrentNumber = digitsValues.Count() - 1;
                for (; indexMaxDivisorOfCurrentNumber >= 0; indexMaxDivisorOfCurrentNumber--)
                {
                    if (number / digitsValues[indexMaxDivisorOfCurrentNumber] >= 1)
                    {
                        number -= digitsValues[indexMaxDivisorOfCurrentNumber];
                        result += romanDigits[indexMaxDivisorOfCurrentNumber];
                        break;
                    }
                }
            }
            return result;
        }
    }
}
