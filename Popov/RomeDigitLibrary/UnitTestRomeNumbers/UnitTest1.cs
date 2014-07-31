using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RomeDigitLibrary;


namespace UnitTestRomeNumbers
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var a = new RomeNumber("XV");
            Assert.AreEqual((uint)15, a.ToUint32());
        }

        [TestMethod]
        public void TestMethod2()
        {
            var a = new RomeNumber("XVIII");
            Assert.AreEqual((uint)18, a.ToUint32());
        }

        [TestMethod]
        public void TestMethod3()
        {
            var a = new RomeNumber("XIX");
            Assert.AreEqual((uint)19, a.ToUint32());
        }

        [TestMethod]
        public void TestMethod4()
        {
            var a = new RomeNumber("C");
            Assert.AreEqual((uint)100, a.ToUint32());
        }

        [TestMethod]
        public void TestMethod5()
        {
            var a = new RomeNumber("CCC");
            Assert.AreEqual((uint)300, a.ToUint32());
        }

        [TestMethod]
        public void TestMethod6()
        {
            var a = new RomeNumber("CMXCIX");
            Assert.AreEqual((uint)999, a.ToUint32());
        }

        [TestMethod]
        public void TestMethod7()
        {
            var a = new RomeNumber("MMMMMMIX");
            Assert.AreEqual((uint)6009, a.ToUint32());
        }


        [TestMethod]
        public void TestRomeNumber1()
        {
            var a = new RomeNumber("X");
            Assert.AreEqual(a, RomeNumber.ConvertUIntToRomeNumber(10));
        }

        [TestMethod]
        public void TestRomeNumber2()
        {
            var a = new RomeNumber("XI");
            Assert.AreEqual(a, RomeNumber.ConvertUIntToRomeNumber(11));
        }

        [TestMethod]
        public void TestRomeNumber3()
        {
            var a = new RomeNumber("CXI");
            Assert.AreEqual(a, RomeNumber.ConvertUIntToRomeNumber(111));
        }

        [TestMethod]
        public void TestRomeNumber4()
        {
            var a = new RomeNumber("IV");
            Assert.AreEqual(a, RomeNumber.ConvertUIntToRomeNumber(4));
        }

        [TestMethod]
        public void TestRomeNumber5()
        {
            var a = new RomeNumber("MCMXCIX");
            Assert.AreEqual(a, RomeNumber.ConvertUIntToRomeNumber(1999));
        }

        [TestMethod]
        public void TestRomeNumber6()
        {
            var a = new RomeNumber("MMM");
            Assert.AreEqual(a, RomeNumber.ConvertUIntToRomeNumber(3000));
        }

        [TestMethod]
        public void TestRomeNumber7()
        {
            var a = new RomeNumber("MMMCC");
            Assert.AreEqual(a, RomeNumber.ConvertUIntToRomeNumber(3200));
        }

        [TestMethod]
        public void TestRomeNumber8()
        {
            var a = new RomeNumber("MCMLXXXVIII");
            Assert.AreEqual(a, RomeNumber.ConvertUIntToRomeNumber(1988));
        }

        [TestMethod]
        public void TestConverToUint1()
        {
            var a = new RomeNumber("MMMMMMMMMMMMMM");
            Assert.AreEqual(a.ToUint32(), (uint)14000);
        }

        [TestMethod]
        public void TestConverToUint2()
        {
            var a = new RomeNumber("MCMLXXXVIII");
            Assert.AreEqual(a.ToUint32(), (uint)1988);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestExeptionConverToUint1()
        {
            var a = new RomeNumber("XXHaa");
            var temp = a.ToUint32();
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestExeptionConverToUint2()
        {
            var a = new RomeNumber("IC");
            var temp = a.ToUint32();
        }

        [TestMethod]
        public void TestOperator1()
        {
            var first = new RomeNumber("X");
            var second = new RomeNumber("V");
            
            Assert.AreEqual(first + second, RomeNumber.ConvertUIntToRomeNumber(15));
        }

        [TestMethod]
        public void TestOperator2()
        {
            var first = new RomeNumber("XX");
            var second = new RomeNumber("VIII");

            Assert.AreEqual(first - second, RomeNumber.ConvertUIntToRomeNumber(12));
        }

        [TestMethod]
        public void TestOperator3()
        {
            var first = new RomeNumber("IV");
            var second = new RomeNumber("XIV");
            Assert.AreEqual(first * second, RomeNumber.ConvertUIntToRomeNumber(56));
        }

        [TestMethod]
        public void TestOperator4()
        {
            var first = new RomeNumber("LXIV");
            var second = new RomeNumber("VIII");
            Assert.AreEqual(first / second, RomeNumber.ConvertUIntToRomeNumber(8));
        }

        [TestMethod]
        [ExpectedException(typeof(ContextMarshalException))]
        public void TestZeroExeption()
        {
            var first = new RomeNumber("V");
            var second = new RomeNumber("X");
            var temp = first/second;
        }
    }
}
