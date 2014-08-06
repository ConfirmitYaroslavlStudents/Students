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
        private readonly Dictionary<int, string> dictionaryOfArabicAndRomanNumbers = new Dictionary<int, string>();
        private bool IsInitializedDictionary = false;

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
                    dictionaryOfArabicAndRomanNumbers.Add(currentArabicNumber, currentRomanNumberInStringFormat);
                }
                IsInitializedDictionary = true;
            }
        }

        [TestMethod]
        public void RomanCalculatorUnitTest_CorrectnessConvertingRomanNumbersToArabic()
        {
            if (!IsInitializedDictionary)
                InitializeDictionary();

            var parser = new ParserArithmeticExpression();
            foreach (var arabicNumber in dictionaryOfArabicAndRomanNumbers.Keys)
            {
                var romanNumber = dictionaryOfArabicAndRomanNumbers[arabicNumber];

                var expectedArabicNumber = arabicNumber;
                var actualArabicNumberAfterConverting = parser.ConvertRomanNumberToArabic(romanNumber);
                Assert.AreEqual(expectedArabicNumber, actualArabicNumberAfterConverting);
            }
        }

        [TestMethod]
        public void ConvertRomanNumberToArabic_CorrectValue()
        {
            if (!IsInitializedDictionary)
                InitializeDictionary();

            var parser = new ParserArithmeticExpression();
            foreach (var romanNumber in dictionaryOfArabicAndRomanNumbers.Values)
            {
                var arabicNumber = dictionaryOfArabicAndRomanNumbers.FirstOrDefault(x => x.Value == romanNumber).Key; ;

                var expectedArabicNumber = arabicNumber;
                var actualArabicNumberAfterConverting =  parser.ConvertRomanNumberToArabic(romanNumber);
                Assert.AreEqual(expectedArabicNumber, actualArabicNumberAfterConverting);
            }
        }

        [TestMethod]
        public void ConvertInputStringToReversePolishSignature_CorrectValue()
        {
            var parser = new ParserArithmeticExpression();
            const string inputArithmeticExpression = "(VII*IX)*(IX+(LVII*I))";

            const string expectedReversePolishSignature = "$VII$$IX$*$IX$$LVII$$I$*+*";
            var actualReversePolishSignature = parser.ConvertInputStringToReversePolishSignature(inputArithmeticExpression);
            Assert.AreEqual(expectedReversePolishSignature, actualReversePolishSignature);
        }

        [TestMethod]
        public void CalculateExpression_CorrectValueOfCalculation()
        {
            var romanCalculator = new RomanCalculatorLibrary.RomanCalculator();
            const string inputArithmeticExpression = "(IX+(XVII*IV))-(VII*IX)";
            var actualResultOfCalculationInRomanFormat = romanCalculator.CalculateExpression(inputArithmeticExpression);
            const string expectedResultInRomanFormat = "XIV";

            Assert.AreEqual(expectedResultInRomanFormat, actualResultOfCalculationInRomanFormat);
        }

        [TestMethod]
        public void CalculateExpression_CorrectValueOfCalculation2()
        {
            var romanCalculator = new RomanCalculator();
            const string inputArithmeticExpression = "(LXII*III)-(IV+(III*(XIV/VII)+III*II))";
            var actualResultOfCalculationInRomanFormat = romanCalculator.CalculateExpression(inputArithmeticExpression);
            const string expectedResultInRomanFormat = "CLXX";

            Assert.AreEqual(expectedResultInRomanFormat, actualResultOfCalculationInRomanFormat);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CalculateExpression_UncorrectArithmeticExpression_ExceptionThrown()
        {
            var romanCalculator = new RomanCalculator();
            const string inputArithmeticExpression = "(IX+(XVII**IV))-(VII*IX)";
            var actualResultOfCalculationInRomanFormat = romanCalculator.CalculateExpression(inputArithmeticExpression);
            const string expectedResultInRomanFormat = "XIV";

            Assert.AreEqual(expectedResultInRomanFormat, actualResultOfCalculationInRomanFormat);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateExpression_UncorrectArithmeticExpression_ExceptionThrown2()
        {
            var romanCalculator = new RomanCalculator();
            const string inputArithmeticExpression = "(V++2)";
            var actualResultOfCalculationInRomanFormat = romanCalculator.CalculateExpression(inputArithmeticExpression);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateExpression_UncorrectArithmeticExpressionWithTerminalSign_ExceptionThrown()
        {
            var romanCalculator = new RomanCalculator();
            const string inputArithmeticExpression = "(V$2)";
            var actualResultOfCalculationInRomanFormat = romanCalculator.CalculateExpression(inputArithmeticExpression);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateExpression_UncorrectAmountOfBrackets_ExceptionThrown()
        {
            var romanCalculator = new RomanCalculator();
            const string inputArithmeticExpression = "(IX+(XVII*IV))-(VII*IX)(((";
            var actualResultOfCalculationInRomanFormat = romanCalculator.CalculateExpression(inputArithmeticExpression);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateExpression_UncorrectRomanNumber_ExceptionThrown()
        {
            var romanCalculator = new RomanCalculator();
            const string inputArithmeticExpression = "(IX+F)";
            var actualResultOfCalculationInRomanFormat = romanCalculator.CalculateExpression(inputArithmeticExpression);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateExpression_ResultOfDivisionIsNotInteger_ExceptionThrown()
        {
            var romanCalculator = new RomanCalculator();
            const string inputArithmeticExpression = "(IX/IV)";
            var actualResultOfCalculationInRomanFormat = romanCalculator.CalculateExpression(inputArithmeticExpression);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateExpression_NotPositiveResultOfCalculation_ExceptionThrown()
        {
            var romanCalculator = new RomanCalculator();
            const string inputArithmeticExpression = "(IX-L)";
            var expectedResultOfCalculationInRomanFormat = "-41";
            var actualResultOfCalculationInRomanFormat = romanCalculator.CalculateExpression(inputArithmeticExpression);

            Assert.AreEqual(expectedResultOfCalculationInRomanFormat, actualResultOfCalculationInRomanFormat);
        }
    }
}
