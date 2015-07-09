using System;
using System.IO;
using RomanCalculator;
using Xunit;
using Xunit.Extensions;

namespace RomanCalculatorTests
{
    public class RomanCalulatorTests
    {
        [Theory,
         InlineData("I + I", "II", 2),
         InlineData("XL + CM", "CMXL", 940),
         InlineData("MCCLIX + DXIV", "MDCCLXXIII", 1773),
         InlineData("CD + XLIII", "CDXLIII", 443),
         InlineData("II + III", "V", 5),

         InlineData("II - I", "I", 1),
         InlineData("CMXL - DCXIV", "CCCXXVI", 326),
         InlineData("MCCLIX - DXIV", "DCCXLV", 745),
         InlineData("X - I", "IX", 9),
         InlineData("V - III", "II", 2),

         InlineData("I * I", "I", 1),
         InlineData("II * CMXLI", "MDCCCLXXXII", 1882),
         InlineData("MCCLIX * II", "MMDXVIII", 2518),
         InlineData("LVII * XLIII", "MMCDLI", 2451),
         InlineData("II * III", "VI", 6)]
        public void Calculate_FromExpression_ShouldPass(string expression, string expected, int indecimal)
        {
            var actual = new RomanCalc().CalculateExpression(expression);
            Assert.Equal(expected, actual.Roman);
            Assert.Equal(indecimal, actual.Value);
        }

        [Theory,
         InlineData("IXX + I", "XX", 20),
         InlineData("CDXCIX + I", "D", 500),
         InlineData("LDVLIV + I", "D", 500),
         InlineData("XDIX + I", "D", 500),
         InlineData("VDIV + I", "D", 500),
         InlineData("ID + I", "D", 500),
         InlineData("MCMLXXXIX + III", "MCMXCII", 1992)]
        public void Calculate_UnusualExpression_ShouldPass(string expression, string expected, int indecimal)
        {
            var actual = new RomanCalc().CalculateExpression(expression);
            Assert.Equal(expected, actual.Roman);
            Assert.Equal(indecimal, actual.Value);
        }

        [Theory,
         InlineData("I - II"),
         InlineData("DCXIV - CMXL"),
         InlineData("DXIV - MCCLIX"),
         InlineData("IV - IX"),
         InlineData("V - V")]
        public void Calculate_FromExpression_NotNaturalResult_ShouldThrowInvalidOperation(string expression)
        {
            Assert.Throws(typeof (InvalidOperationException), () => { new RomanCalc().CalculateExpression(expression); });
        }

        [Theory,
         InlineData("I / II"),
         InlineData("DCXIV ^ CMXL"),
         InlineData("DXIV = MCCLIX"),
         InlineData("IV ! IX"),
         InlineData("V ? LIII")]
        public void Calculate_FromExpression_UndefinedOperation_ShouldThrowInvalidOperation(string expression)
        {
            Assert.Throws(typeof (InvalidOperationException), () => { new RomanCalc().CalculateExpression(expression); });
        }

        [Theory,
         InlineData("I+II"),
         InlineData("DCXIV CMXL"),
         InlineData("DXIVMCCLIX"),
         InlineData("IVXL + IX"),
         InlineData("CMDCMD + CMD"),
         InlineData("IIIIX - I"),
         InlineData("IIIII + V"),
         InlineData("X ++"),
         InlineData("X + X + X"),
         InlineData("Привет, как дела?")]
        public void Calculate_FromExpression_IncorrectInput_ShouldThrowInvalidData(string expression)
        {
            Assert.Throws(typeof (InvalidDataException), () => { new RomanCalc().CalculateExpression(expression); });
        }
    }
}