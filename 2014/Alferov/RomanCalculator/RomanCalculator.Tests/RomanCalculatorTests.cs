using System;
using Xunit;
using Xunit.Extensions;

namespace RomanCalculator.Tests
{
    public class RomanCalculatorTests
    {
        [Theory]
        [InlineData("IV + VI", "X"),
         InlineData("XL + CM", "CMXL"),
         InlineData("MCCLIX + DXIV", "MDCCLXXIII"),
         InlineData("CD + XLIII", "CDXLIII"),
         InlineData("I + IV", "V")]

        [InlineData("XC - I", "LXXXIX"),
         InlineData("CMXL - DCXIV", "CCCXXVI"),
         InlineData("MCCLIX - DXIV", "DCCXLV"),
         InlineData("X - IV", "VI"),
         InlineData("V - III", "II")]

        [InlineData("I * IV", "IV"),
         InlineData("II * CMXLI", "MDCCCLXXXII"),
         InlineData("MCCCXXXIII * III", "MMMCMXCIX"),
         InlineData("LVII * XLIII", "MMCDLI"),
         InlineData("II * III", "VI")]
        public void CalculateExpression_SimpleExpressions_ShouldPass(string expression, string expected)
        {
            Assert.Equal(expected, RomanCalculator.CalculateExpression(expression));
        }

        [Theory]
        [InlineData(""),
         InlineData(null)]
        public void ToArabic_NullOrEmptyRomanNumber_ArgumentNullExceptionThrown(string romanNumber)
        {
            Assert.Throws(typeof(ArgumentNullException), () =>
            {
                RomanCalculator.ToArabic(romanNumber);
            });
        }

        [Theory]
        [InlineData("IIII + I"),
         InlineData("XHR - V"),
         InlineData("MVC * I"),
         InlineData("I + VM"),
         InlineData("V - V"),
         InlineData("V - VI"),
         InlineData("CMCM + I")]
        public void CalculateExpression_IncorrectArguments_ArgumentOutOfRangeExceptionThrown(string expression)
        {
            Assert.Throws(typeof(ArgumentOutOfRangeException), () =>
            {
                RomanCalculator.CalculateExpression(expression);
            });
        }

        [Theory]
        [InlineData("X ++"),
         InlineData("I+II"),
         InlineData("X + X + X")]
        public void CalculateExpression_IncorrectArgumentsCount_ArgumentExceptionThrown(string expression)
        {
            Assert.Throws(typeof(ArgumentException), () =>
            {
                RomanCalculator.CalculateExpression(expression);
            });
        }

        [Fact]
        public void CalculateExpression_IncorrectOperation_InvalidOperationExceptionThrown()
        {
            Assert.Throws(typeof(InvalidOperationException), () =>
            {
                RomanCalculator.CalculateExpression("I / V");
            });
        }
    }
}
