using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FizzBuzz;

namespace FizzBuzz.Tests
{
    [TestClass]

    public class FizzBuzzerTests
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
        public void Defult2Settings()
        {
            var fizzBuzz = new FizzBuzzer();

            Assert.AreEqual(fizzBuzz.Settings.Count, 2);
        }

        [TestMethod]
        public void PassNewSettingGet3Settings()
        {
            var fizzBuzz = new FizzBuzzer();
            fizzBuzz.AddSetting(2, "coconut");

            Assert.AreEqual(fizzBuzz.Settings.Count, 3);
        }

        [TestMethod]
        public void PassChangeSettingGet2Settings()
        {
            var fizzBuzz = new FizzBuzzer();
            fizzBuzz.AddSetting(3, "coconut");

            Assert.AreEqual(fizzBuzz.Settings.Count, 2);
        }

        [TestMethod]
        public void PassDivisibleBy3And5And2GetFizzBuzzCoconut()
        {
            var fizzBuzz = new FizzBuzzer();
            fizzBuzz.AddSetting(2, "Coconut");
            var result = fizzBuzz.ToString(30);

            Assert.AreEqual("Fizz Buzz Coconut", result);
        }

        [TestMethod]
        public void PassDivisibleBy3And5And2GetFizzBuzzBanana()
        {
            var fizzBuzz = new FizzBuzzer();
            fizzBuzz.AddSetting(2, "Coconut");
            fizzBuzz.AddSetting(2, "Banana");
            var result = fizzBuzz.ToString(30);

            Assert.AreEqual("Fizz Buzz Banana", result);
        }
    }
}
