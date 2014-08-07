using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RomeDigitLibrary;


namespace UnitTestRomeNumbers
{
    //переименовать фабрику и "UnitTest1"
    static class InstanceRomeNumber
    {
        public static RomeNumber GetInstance(string Rome)
        {
            return new RomeNumber(Rome);
        }
    }

    [TestClass]
    public class TestRomeNumber
    {

        [TestMethod]
        public void TestConvertToUint()
        {
            var equalsDictionary = new Dictionary<uint, uint>
            {
                {InstanceRomeNumber.GetInstance("XV").ToUint32(), 15},
                {InstanceRomeNumber.GetInstance("XVIII").ToUint32(), 18},
                {InstanceRomeNumber.GetInstance("XIX").ToUint32(), 19},
                {InstanceRomeNumber.GetInstance("C").ToUint32(), 100},
                {InstanceRomeNumber.GetInstance("CCC").ToUint32(), 300},
                {InstanceRomeNumber.GetInstance("CMXCIX").ToUint32(), 999},
                {InstanceRomeNumber.GetInstance("MMMMMMIX").ToUint32(), 6009},
                {InstanceRomeNumber.GetInstance("MMMMMMMMMMMMMM").ToUint32(),14000},
                {InstanceRomeNumber.GetInstance("MCMLXXXVIII").ToUint32(),1988}
            };
            foreach (var pair in equalsDictionary)
            {
                Assert.AreEqual(pair.Key,pair.Value);
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
            Assert.AreEqual(InstanceRomeNumber.GetInstance(Rn).ToString(),Rn);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestExeptionConverToUint1()
        {
            var a = InstanceRomeNumber.GetInstance("XXHaa");
            var temp = a.ToUint32();
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestExeptionConverToUint2()
        {
            var a = InstanceRomeNumber.GetInstance("IC");
            var temp = a.ToUint32();
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
