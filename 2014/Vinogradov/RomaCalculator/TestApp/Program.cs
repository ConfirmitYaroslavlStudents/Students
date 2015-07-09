using System;
using System.Collections.Generic;
using RomaCalculator;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
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
                //Assert.AreEqual(item.Value, result);
            }
            Console.ReadLine();
        }
    }
}
