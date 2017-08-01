using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FizzBuzz.Tests
{
    [TestClass]
    public class FizzBuzzTest
    {
        private FizzBuzz _fizzBuzz;

        [TestInitialize]
        public void TestInitialize()
        {
            _fizzBuzz = new FizzBuzz();
        }

        [TestMethod]
        public void GetCorrectStringForNumberOne()
        {
            string numberAsString = _fizzBuzz.GetStringRepresentationFor(1);

            Assert.AreEqual("1", numberAsString);
        }

        [TestMethod]
        public void GetCorrectStringForNumberTwo()
        {
            string numberAsString = _fizzBuzz.GetStringRepresentationFor(2);

            Assert.AreEqual("2", numberAsString);
        }

        [TestMethod]
        public void GetCorrectStringForNumberFive()
        {
            string numberAsString = _fizzBuzz.GetStringRepresentationFor(5);

            Assert.AreEqual("Buzz", numberAsString);
        }

        [TestMethod]
        public void GetCorrectStringForNumberTen()
        {
            string numberAsString = _fizzBuzz.GetStringRepresentationFor(10);

            Assert.AreEqual("Buzz", numberAsString);
        }

        [TestMethod]
        public void GetCorrectStringForNumberThree()
        {
            string numberAsString = _fizzBuzz.GetStringRepresentationFor(3);

            Assert.AreEqual("Fizz", numberAsString);
        }

        [TestMethod]
        public void GetCorrectStringForNumberFifteen()
        {
            string numberAsString = _fizzBuzz.GetStringRepresentationFor(15);

            Assert.AreEqual("FizzBuzz", numberAsString);
        }

        [TestMethod]
        public void CheckRuleExists()
        {
            Assert.AreEqual(true, _fizzBuzz.IsRuleExistsFor(3));
            Assert.AreEqual(false, _fizzBuzz.IsRuleExistsFor(1));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddingPresentRule()
        {
            _fizzBuzz.AddRule(3, "Banana");
        }

        [TestMethod]
        public void AddingRule()
        {
            FizzBuzz fizzBuzz = new FizzBuzz();
            fizzBuzz.AddRule(18, "Banana");

            Assert.AreEqual(true, fizzBuzz.IsRuleExistsFor(18));
        }

        [TestMethod]
        public void GetCorrectStringForCustomRule()
        {
            FizzBuzz fizzBuzz = new FizzBuzz();
            fizzBuzz.AddRule(18, "Banana");

            var numberAsString = fizzBuzz.GetStringRepresentationFor(18);

            Assert.AreEqual("FizzBanana", numberAsString);
        }

        [TestMethod]
        public void RemovingRule()
        {
            FizzBuzz fizzBuzz = new FizzBuzz();
            fizzBuzz.AddRule(18, "Banana");
            fizzBuzz.RemoveRuleFor(18);

            Assert.AreEqual(false, fizzBuzz.IsRuleExistsFor(18));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ChangingNotExistingRule()
        {
            _fizzBuzz.ChangeRule(1, "NewValue");
        }

        [TestMethod]
        public void ChangingRule()
        {
            FizzBuzz fizzBuzz = new FizzBuzz();
            fizzBuzz.ChangeRule(3, "Buzz");
            fizzBuzz.ChangeRule(5, "Fizz");

            string numberAsString = fizzBuzz.GetStringRepresentationFor(15);

            Assert.AreEqual("BuzzFizz", numberAsString);
        }

        [TestMethod]
        public void AddingRuleRange()
        {
            FizzBuzz fizzBuzz = new FizzBuzz();
            fizzBuzz.AddRuleRange(new Dictionary<int, string>
            {
                {2, "Banana"},
                {17, "Orange"},
            });

            Assert.AreEqual(true, fizzBuzz.IsRuleExistsFor(2));
            Assert.AreEqual(true, fizzBuzz.IsRuleExistsFor(17));
        }

        [TestMethod]
        public void Processor()
        {
            string[] expectedValues =
            {
                "1", "Banana", "Fizz", "Banana", "Buzz",
                "BananaFizz", "7", "Banana", "FizzOrange", "BananaBuzz"
            };

            FizzBuzz fizzBuzz = new FizzBuzz(new Dictionary<int, string>
            {
                {2, "Banana"},
                {9, "Orange"},
            });

            FizzBuzzProcessor processor = new FizzBuzzProcessor(fizzBuzz);

            int i = 0;
            foreach (string item in processor.Process(1, 10))
            {
                Assert.AreEqual(expectedValues[i], item);
                i++;
            }
        }
    }
}