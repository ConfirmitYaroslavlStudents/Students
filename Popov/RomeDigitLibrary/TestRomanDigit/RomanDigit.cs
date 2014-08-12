using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RomeDigitLibrary;

namespace TestRomanDigit
{
    static class InstanceRomeNumber
    {
        public static RomeNumber GetInstance(string Rome)
        {
            return new RomeNumber(Rome);
        }
    }

    [TestClass]
    public class RomanDigit
    {
        [TestMethod]
        public void TestConvertToUint()
        {
            var equalsDictionary = new Dictionary<uint, uint>
            {
                {InstanceRomeNumber.GetInstance("XV").ArabNumber, 15},
                {InstanceRomeNumber.GetInstance("XVIII").ArabNumber, 18},
                {InstanceRomeNumber.GetInstance("XIX").ArabNumber, 19},
                {InstanceRomeNumber.GetInstance("C").ArabNumber, 100},
                {InstanceRomeNumber.GetInstance("CCC").ArabNumber, 300},
                {InstanceRomeNumber.GetInstance("CMXCIX").ArabNumber, 999},
                {InstanceRomeNumber.GetInstance("MMMMMMIX").ArabNumber, 6009},
                {InstanceRomeNumber.GetInstance("MMMMMMMMMMMMMM").ArabNumber,14000},
                {InstanceRomeNumber.GetInstance("MCMLXXXVIII").ArabNumber,1988}
            };
            foreach (var pair in equalsDictionary)
            {
                Assert.AreEqual(pair.Key, pair.Value);
            }
        }

        [TestMethod]
        public void TestConvertUIntToRomeNumber()
        {
            var equalsDictionary = new Dictionary<RomeNumber, RomeNumber>
            {
                {InstanceRomeNumber.GetInstance("X"), RomeNumber.ConvertUIntToRomeNumber(10)},
                {InstanceRomeNumber.GetInstance("XI"), RomeNumber.ConvertUIntToRomeNumber(11)},
                {InstanceRomeNumber.GetInstance("CXI"), RomeNumber.ConvertUIntToRomeNumber(111)},
                {InstanceRomeNumber.GetInstance("IV"), RomeNumber.ConvertUIntToRomeNumber(4)},
                {InstanceRomeNumber.GetInstance("MCMXCIX"), RomeNumber.ConvertUIntToRomeNumber(1999)},
                {InstanceRomeNumber.GetInstance("MMM"), RomeNumber.ConvertUIntToRomeNumber(3000)},
                {InstanceRomeNumber.GetInstance("MMMCC"), RomeNumber.ConvertUIntToRomeNumber(3200)},
                {InstanceRomeNumber.GetInstance("MCMLXXXVIII"), RomeNumber.ConvertUIntToRomeNumber(1988)}
            };
            foreach (var pair in equalsDictionary)
            {
                Assert.AreEqual(pair.Key, pair.Value);
            }
        }

        [TestMethod]
        public void TestToString()
        {
            const string Rn = "XI";
            Assert.AreEqual(InstanceRomeNumber.GetInstance(Rn).ToString(), Rn);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestExeptionConverToUint1()
        {
            var a = InstanceRomeNumber.GetInstance("XXHaa");
            var temp = a.ArabNumber;
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestExeptionConverToUint2()
        {
            var a = InstanceRomeNumber.GetInstance("IC");
            var temp = a.ArabNumber;
        }

        [TestMethod]
        public void TestAddition()
        {
            var first = InstanceRomeNumber.GetInstance("X");
            var second = InstanceRomeNumber.GetInstance("V");

            Assert.AreEqual(first + second, RomeNumber.ConvertUIntToRomeNumber(15));
        }

        [TestMethod]
        public void TestSubtraction()
        {
            var first = InstanceRomeNumber.GetInstance("XX");
            var second = InstanceRomeNumber.GetInstance("VIII");

            Assert.AreEqual(first - second, RomeNumber.ConvertUIntToRomeNumber(12));
        }

        [TestMethod]
        public void TestMultiply()
        {
            var first = InstanceRomeNumber.GetInstance("IV");
            var second = InstanceRomeNumber.GetInstance("XIV");
            Assert.AreEqual(first * second, RomeNumber.ConvertUIntToRomeNumber(56));
        }
    }
}
