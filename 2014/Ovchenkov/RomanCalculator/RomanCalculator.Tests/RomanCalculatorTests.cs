using System;
using System.IO;
using Xunit;
using Xunit.Extensions;

namespace RomanCalculator.Tests
{
    public class RomanCalculatorTests
    {
        [Theory]
         [InlineData("I + I", "II"),
         InlineData("III + III", "VI"),
         InlineData("IV + VI", "X"),
         InlineData("CD + XLIII", "CDXLIII"),
         InlineData("XL + CM", "CMXL"),
         InlineData("DCLXVI + DCCLXXVII", "MCDXLIII")]

         [InlineData("II - I", "I"),
         InlineData("VI - III", "III"),
         InlineData("X - IV", "VI"),
         InlineData("X - I", "IX"),
         InlineData("MCCLIX - DXIV", "DCCXLV"),
         InlineData("MCDXLIII - DCCLXXVII", "DCLXVI")]
        
         [InlineData("I * I", "I"),
         InlineData("III * III", "IX"),
         InlineData("II * DCCLXXVII", "MDLIV"),
         InlineData("DCCLXXVII * II", "MDLIV"),
         InlineData("LVII * XLIII", "MMCDLI")]
        public void RomanCalculator_SimpleExpressions_ShouldPass(string expression, string expectedRoman)
        {
            var actual = RomanCalculator.CalculateExpression(expression);

            Assert.Equal(expectedRoman, actual);
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
