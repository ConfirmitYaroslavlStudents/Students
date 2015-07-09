using System;
using System.IO;
using Xunit;
using Xunit.Extensions;

namespace RomanCalculator.Tests
{
    public class RomanCalculatorTests
    {
        [Theory]
         [InlineData("I + I", "II", 2),
         InlineData("III + III", "VI", 6),
         InlineData("IV + VI", "X", 10),
         InlineData("CD + XLIII", "CDXLIII", 443),
         InlineData("XL + CM", "CMXL", 940),
         InlineData("DCLXVI + DCCLXXVII", "MCDXLIII", 1443)]

         [InlineData("II - I", "I", 1),
         InlineData("VI - III", "III", 3),
         InlineData("X - IV", "VI", 6),
         InlineData("X - I", "IX", 9),
         InlineData("MCCLIX - DXIV", "DCCXLV", 745),
         InlineData("MCDXLIII - DCCLXXVII", "DCLXVI", 666)]
        
         [InlineData("I * I", "I", 1),
         InlineData("III * III", "IX", 9),
         InlineData("II * DCCLXXVII", "MDLIV", 1554),
         InlineData("DCCLXXVII * II", "MDLIV", 1554),
         InlineData("LVII * XLIII", "MMCDLI", 2451)]
        public void RomanCalculator_SimpleExpressions_ShouldPass(string expression, string expectedRoman, int expectedArabic)
        {
            var actual = RomanCalculator.CalculateExpression(expression);

            Assert.Equal(expectedRoman, actual.Roman);
            Assert.Equal(expectedArabic, actual.Arabic);
        }

        [Theory,
         InlineData("I - II"),
         InlineData("DCXIV - CMXL"),
         InlineData("DXIV - MCCLIX"),
         InlineData("IV - IX"),
         InlineData("V - V")]
        public void RomanCalculator_FromExpression_NotNaturalResult_ShouldThrowInvalidOperation(string expression)
        {
            Assert.Throws(typeof (InvalidOperationException), () =>
            {
                RomanCalculator.CalculateExpression(expression);
            });
        }

        [Theory,
         InlineData("I ++"),
         InlineData("I+II"),
         InlineData("X IV"),
         InlineData("CXI"),
         InlineData("IVXL + IX"),
         InlineData("CMDCMD + CMD"),
         InlineData("IIIIII - I"),
         InlineData("XXXX + V"),
         InlineData("I + I + I + I")]
        public void RomanCalculator_IncorrectExpression_ShouldThrowInvalidData(string expression)
        {
            Assert.Throws(typeof (InvalidDataException), () =>
            {
                RomanCalculator.CalculateExpression(expression);
            });
        }

         [Theory,
         InlineData("II ^ IV"),
         InlineData("X / V"),
         InlineData("X % X"),
         InlineData("XI ! III")]
        public void RomanCalculator_IncorrectOperation_InvalidOperationExceptionThrown(string expression)
        {
            Assert.Throws(typeof(InvalidOperationException), () =>
            {
                RomanCalculator.CalculateExpression(expression);
            });
        }
    }
}
