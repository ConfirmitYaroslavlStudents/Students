using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RomanCalculator.UnitTests
{
    using RomanCalculatorLibrary;
    [TestClass]
    public class RomanCalculatorUnitTests
    {
        private readonly Dictionary<int, string> _dictionaryOfArabicAndRomanNumbers = new Dictionary<int, string>();
        private bool _isInitializedDictionary;

        private void InitializeDictionary()
        {
            using (var streamReader = new StreamReader("UnitTests.ListOfRomanNumbers.txt"))
            {
                string currentLine;
                while ((currentLine = streamReader.ReadLine()) != null)
                {
                    var numbersInStringFormatAfterSplitting = currentLine.Split('|');
                    var currentArabicNumberInStringFormat = numbersInStringFormatAfterSplitting[0];
                    var currentArabicNumber = int.Parse(currentArabicNumberInStringFormat);
                    var currentRomanNumberInStringFormat = numbersInStringFormatAfterSplitting[1];
                    _dictionaryOfArabicAndRomanNumbers.Add(currentArabicNumber, currentRomanNumberInStringFormat);
                }
                _isInitializedDictionary = true;
            }
        }

        [TestMethod]
        public void ConvertRomanNumberToArabic_RomanNumberIsProperlyConvertedToArabicNotation()
        {
            if (!_isInitializedDictionary)
                InitializeDictionary();

            var parser = new ArithmeticExpressionParser();
            foreach (var arabicNumber in _dictionaryOfArabicAndRomanNumbers.Keys)
            {
                var romanNumber = _dictionaryOfArabicAndRomanNumbers[arabicNumber];

                var expectedArabicNumber = arabicNumber;
                var actualArabicNumberAfterConverting = parser.ConverterOfNumbers.ConvertRomanNumberToArabic(romanNumber);
                Assert.AreEqual(expectedArabicNumber, actualArabicNumberAfterConverting);
            }
        }

        [TestMethod]
        public void ConvertArabicNumberToRoman_ArabicNumberIsProperlyConvertedToRomanNotation()
        {
            if (!_isInitializedDictionary)
                InitializeDictionary();

            var parser = new ArithmeticExpressionParser();
            foreach (var romanNumber in _dictionaryOfArabicAndRomanNumbers.Values)
            {
                var arabicNumber = _dictionaryOfArabicAndRomanNumbers.FirstOrDefault(x => x.Value == romanNumber).Key;
                var expectedRomanNumber = romanNumber;
                var actualRomanNumberAfterConverting = parser.ConverterOfNumbers.ConvertArabicNumberToRoman(arabicNumber);
                Assert.AreEqual(expectedRomanNumber, actualRomanNumberAfterConverting);
            }
        }

        [TestMethod]
        public void ConvertInputStringToReversePolishSignature_InputExpressionIsProperlyConvertedInReversePolishSignature()
        {
            var parser = new ArithmeticExpressionParser();
            const string inputArithmeticExpression = "(VII*IX)*(IX+(LVII*I))";

            const string expectedReversePolishSignature = "$VII$$IX$*$IX$$LVII$$I$*+*";
            var actualReversePolishSignature = parser.ConvertInputStringToReversePolishSignature(inputArithmeticExpression);
            Assert.AreEqual(expectedReversePolishSignature, actualReversePolishSignature);
        }

        [TestMethod]
        public void CalculateExpression_InpuExpressionIsProperlyCalculated()
        {
            var romanCalculator = new RomanCalculator();
            var inputArithmeticExpression = "(IX+(XVII*IV))-(VII*IX)";
            var actualResultOfCalculationInRomanFormat = romanCalculator.CalculateExpression(inputArithmeticExpression);
            var expectedResultInRomanFormat = "XIV";
            Assert.AreEqual(expectedResultInRomanFormat, actualResultOfCalculationInRomanFormat);

            inputArithmeticExpression = "(LXII*III)-(IV+(III*(XIV/VII)+III*II))";
            actualResultOfCalculationInRomanFormat = romanCalculator.CalculateExpression(inputArithmeticExpression);
            expectedResultInRomanFormat = "CLXX";
            Assert.AreEqual(expectedResultInRomanFormat, actualResultOfCalculationInRomanFormat);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CalculateExpression_UncorrectArithmeticExpression_ExceptionThrown()
        {
            var romanCalculator = new RomanCalculator();
            const string inputArithmeticExpression = "(IX+(XVII**IV))-(VII*IX)";
            romanCalculator.CalculateExpression(inputArithmeticExpression);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateExpression_UncorrectArithmeticExpression_ExceptionThrown2()
        {
            var romanCalculator = new RomanCalculator();
            const string inputArithmeticExpression = "(V++2)";
            romanCalculator.CalculateExpression(inputArithmeticExpression);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateExpression_UncorrectArithmeticExpressionWithTerminalSign_ExceptionThrown()
        {
            var romanCalculator = new RomanCalculator();
            const string inputArithmeticExpression = "(V$2)";
            romanCalculator.CalculateExpression(inputArithmeticExpression);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateExpression_UncorrectAmountOfBrackets_ExceptionThrown()
        {
            var romanCalculator = new RomanCalculator();
            const string inputArithmeticExpression = "(IX+(XVII*IV))-(VII*IX)(((";
            romanCalculator.CalculateExpression(inputArithmeticExpression);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateExpression_UncorrectRomanNumber_ExceptionThrown()
        {
            var romanCalculator = new RomanCalculator();
            const string inputArithmeticExpression = "(IX+F)";
            romanCalculator.CalculateExpression(inputArithmeticExpression);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateExpression_ResultOfDivisionIsNotInteger_ExceptionThrown()
        {
            var romanCalculator = new RomanCalculator();
            const string inputArithmeticExpression = "(IX/IV)";
            romanCalculator.CalculateExpression(inputArithmeticExpression);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void CalculateExpression_NotPositiveResultOfCalculation_ExceptionThrown()
        {
            var romanCalculator = new RomanCalculator();
            const string inputArithmeticExpression = "(IX-L)";
            romanCalculator.CalculateExpression(inputArithmeticExpression);
        }
    }
}
