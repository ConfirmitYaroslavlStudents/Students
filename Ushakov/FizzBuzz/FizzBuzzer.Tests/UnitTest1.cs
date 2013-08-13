using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using FizzBuzz;

namespace FizzBuzzerTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Pass1GetString1()
        {
            var fizzBuzz = new FizzBuzzer();
            var result = fizzBuzz.ToString(1);

            Assert.AreEqual("1", result);
        }

        [TestMethod]
        public void PassDivzibleBy3GetStringFizz()
        {
            var fizzBuzz = new FizzBuzzer();
            var result = fizzBuzz.ToString(3);

            Assert.AreEqual("Fizz", result);
        }

        [TestMethod]
        public void PassDivzibleBy5GetStringFizz()
        {
            var fizzBuzz = new FizzBuzzer();
            var result = fizzBuzz.ToString(5);

            Assert.AreEqual("Buzz", result);
        }

        [TestMethod]
        public void PassDivzibleBy3And5GetStringFizzBuzz()
        {
            var fizzBuzz = new FizzBuzzer();
            var result = fizzBuzz.ToString(15);

            Assert.AreEqual("Fizz Buzz", result);
        }

        [TestMethod]
        public void PassDivzibleBy2GetStringCocount()
        {
            var fizzBuzz = new FizzBuzzer();
            fizzBuzz.AdditionalReplaceRules.Add(2, "coconut");

            var result = fizzBuzz.ToString(2);

            Assert.AreEqual("coconut", result);
        }

        [TestMethod]
        public void PassDivzibleBy7GetStringCocount()
        {
            var fizzBuzz = new FizzBuzzer();
            fizzBuzz.AdditionalReplaceRules.Add(7, "banana");

            var result = fizzBuzz.ToString(7);

            Assert.AreEqual("banana", result);
        }

        [TestMethod]
        public void PassDivzibleBy2And7GetStringCocountBanana()
        {
            var fizzBuzz = new FizzBuzzer();
            fizzBuzz.AdditionalReplaceRules.Add(7, "banana");
            fizzBuzz.AdditionalReplaceRules.Add(2, "coconut");

            var result = fizzBuzz.ToString(14);

            Assert.AreEqual("coconut banana", result);
        }

        [TestMethod]
        public void PassDivzibleBy32And7GetStringFizzCocountBanana()
        {
            var fizzBuzz = new FizzBuzzer();
            fizzBuzz.AdditionalReplaceRules.Add(7, "banana");
            fizzBuzz.AdditionalReplaceRules.Add(2, "coconut");

            var result = fizzBuzz.ToString(42);

            Assert.AreEqual("Fizz coconut banana", result);
        }
    }
}
