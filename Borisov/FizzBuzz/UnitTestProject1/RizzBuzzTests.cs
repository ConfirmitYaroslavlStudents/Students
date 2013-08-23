using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FizzBuzz;

namespace UnitTestProject1
{
    [TestClass]
    public class RizzBuzzTests
    {
        [TestMethod]
        public void Pass1GetString1()
        {
            var fizzBuzz = new FizzBuzzer();
            var result = fizzBuzz.ToString(1);

            Assert.AreEqual("1", result);
        }

        [TestMethod]
        public void Pass2GetString2()
        {
            var fizzBuzz = new FizzBuzzer();
            var result = fizzBuzz.ToString(2);

            Assert.AreEqual("2", result);
        }

        [TestMethod]
        public void PassDivisibleBy3GetFizz()
        {
            var fizzBuzz = new FizzBuzzer();
            var result = fizzBuzz.ToString(3);

            Assert.AreEqual("Fizz", result);
        }

        [TestMethod]
        public void PassDivisibleBy5GetBuzz()
        {
            var fizzBuzz = new FizzBuzzer();
            var result = fizzBuzz.ToString(5);

            Assert.AreEqual("Buzz", result);
        }

        [TestMethod]
        public void PassDivisibleBy3And5GetFizzBuzz()
        {
            var fizzBuzz = new FizzBuzzer();
            var result = fizzBuzz.ToString(15);

            Assert.AreEqual("Fizz Buzz", result);
        }


        [TestMethod]
        public void PassDivisibleBy2GetCoconut()
        {
            var fizzBuzz = new FizzBuzzer();
            fizzBuzz.AddRule(2, "Coconut");
            var result = fizzBuzz.ToString(2);

            Assert.AreEqual("Coconut", result);
        }

        [TestMethod]
        public void PassDivisibleBy7GetBanana()
        {
            var fizzBuzz = new FizzBuzzer();
            fizzBuzz.AddRule(7, "Banana");
            var result = fizzBuzz.ToString(7);

            Assert.AreEqual("Banana", result);
        }

        [TestMethod]
        public void PassDivisibleBy2And3And5And7GetCoconutFizzBuzzBanana()
        {
            var fizzBuzz = new FizzBuzzer();
            fizzBuzz.AddRule(2, "Coconut");
            fizzBuzz.AddRule(7, "Banana");
            var result = fizzBuzz.ToString(210);

            Assert.AreEqual("Coconut Fizz Buzz Banana", result);
        }
    }
}
