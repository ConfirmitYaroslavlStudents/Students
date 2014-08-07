using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RomaCalculator;
using RomaCalculator.KindsOfOperators;

namespace UnitTestsForRomaCalculator
{
    [TestClass]
    public class RomaCalculatorTests
    {
        //Need more tests
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

            var Roman = new Calculator();

            foreach (var item in testExpressions)
            {
                var result = Roman.Calculate(item.Key);
                Assert.AreEqual(item.Value, result);
            }
        }
    }
}
