using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RomaCalculator;
using RomaCalculator.KindsOfOperators;

namespace UnitTestsForRomaCalculator
{
    [TestClass]
    public class RomaCalculatorTests
    {
        [TestMethod]
        public void FivePlusThree()
        {
            var Roma = new Calculator();
            var result = Roma.Calculate("V", "III", new Addition());
            Assert.AreEqual("VIII", result);
        }

        [TestMethod]
        public void FivePlusSeven()
        {
            var Roma = new Calculator();
            var result = Roma.Calculate("V", "VII", new Addition());
           Assert.AreEqual("XII", result);
        }

        [TestMethod]
        public void FourteenMinusTwentyFive()
        {
            var Roma = new Calculator();
            var result = Roma.Calculate("XIV", "XXV", new Subtraction());
           Assert.AreEqual("-XI", result);
        }

        [TestMethod]
        public void ThreeMinusThree()
        {
            var Roma = new Calculator();
            var result = Roma.Calculate("III", "III", new Subtraction());
           Assert.AreEqual("zero", result);
        }

        [TestMethod]
        public void MultiplySevenAndFive()
        {
            var Roma = new Calculator();
            var result = Roma.Calculate("VII", "V",new Multiplication());
           Assert.AreEqual("XXXV", result);
        }

        [TestMethod]
        public void TwelveSquared()
        {
            var Roma = new Calculator();
            var result = Roma.Calculate("XII", "XII", new Multiplication());
            Assert.AreEqual("CXLIV", result);
        }
    }
}
