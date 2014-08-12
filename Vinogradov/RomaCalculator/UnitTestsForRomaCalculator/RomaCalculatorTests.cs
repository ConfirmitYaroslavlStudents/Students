using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RomaCalculator;

namespace UnitTestsForRomaCalculator
{
    [TestClass]
    public class RomaCalculatorTests
    {
        [TestMethod]
        public void TestFactory()
        {
            var testExpressions = new Dictionary<string, string>();
            testExpressions["V + III"] = "VIII";
            testExpressions["V + VII"] = "XII";
            testExpressions["XIV - XXV"] = "-XI";
            testExpressions["III - III"] = "zero";
            testExpressions["VII * V"] = "XXXV";
            testExpressions["XII * XII"] = "CXLIV";

            testExpressions["I + I"] = "II";
            testExpressions["XL + CM"] = "CMXL";
            testExpressions["MCCLIX + DXIV"] = "MDCCLXXIII";
            testExpressions["CD + XLIII"] = "CDXLIII";
            testExpressions["II + III"] = "V";
            testExpressions["II - I"] = "I";
            testExpressions["CMXL - DCXIV"] = "CCCXXVI";
            testExpressions["MCCLIX - DXIV"] = "DCCXLV";
            testExpressions["X - I"] = "IX";
            testExpressions["V - III"] = "II";
            testExpressions["I * I"] = "I";
            testExpressions["II * CMXLI"] = "MDCCCLXXXII";
            testExpressions["MCCLIX * II"] = "MMDXVIII";
            testExpressions["LVII * XLIII"] = "MMCDLI";
            testExpressions["II * III"] = "VI";

            var Roman = new Calculator();

            foreach (var item in testExpressions)
            {
                var result = Roman.Calculate(item.Key);
                Assert.AreEqual(item.Value, result);
            }
        }
    }
}
