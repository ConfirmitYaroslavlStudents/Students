using System;
using FizzBuzzLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FizzBuzzTests
{
    [TestClass]
    public class FizzBuzzTests
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
            var numberAsString = _fizzBuzz.Process(1);

            Assert.AreEqual("1", numberAsString);
        }

        [TestMethod]
        public void GetCorrectStringForNumberTwo()
        {
            var numberAsString = _fizzBuzz.Process(2);

            Assert.AreEqual("2", numberAsString);
        }

        [TestMethod]
        public void GetCorrectStringForNumberFive()
        {
            var numberAsString = _fizzBuzz.Process(5);

            Assert.AreEqual("Buzz", numberAsString);
        }

        [TestMethod]
        public void GetCorrectStringForNumberTen()
        {
            var numberAsString = _fizzBuzz.Process(10);

            Assert.AreEqual("Buzz", numberAsString);
        }

        [TestMethod]
        public void GetCorrectStringForNumberThree()
        {
            var numberAsString = _fizzBuzz.Process(3);

            Assert.AreEqual("Fizz", numberAsString);
        }

        [TestMethod]
        public void GetCorrectStringForNumberFifteen()
        {
            var numberAsString = _fizzBuzz.Process(15);

            Assert.AreEqual("FizzBuzz", numberAsString);
        }

        [TestMethod]
        public void GetCorrectStringForNewRuleAddedViaConstructor()
        {
            _fizzBuzz = new FizzBuzz(new Rule[] { new Rule(2, "Banana") });

            var numberAsString = _fizzBuzz.Process(2);

            Assert.AreEqual("Banana", numberAsString);
        }

        [TestMethod]
        public void AddNewRuleViaMethod()
        {
            _fizzBuzz.AddRule(2, "Banana");

            var numberAsString = _fizzBuzz.Process(2);

            Assert.AreEqual("Banana", numberAsString);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddExistingRuleViaMethod()
        {
            _fizzBuzz.AddRule(2, "Banana");
            _fizzBuzz.AddRule(2, "Banana");

            var numberAsString = _fizzBuzz.Process(2);

            Assert.AreEqual("Banana", numberAsString);
        }

        [TestMethod]
        public void RemoveRuleViaMethod()
        {
            _fizzBuzz.RemoveRule(3);

            var numberAsString = _fizzBuzz.Process(3);

            Assert.AreEqual("3", numberAsString);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RemoveUnexistingRule()
        {
            _fizzBuzz.RemoveRule(2);
        }

        [TestMethod]
        public void ChangeRule()
        {
            _fizzBuzz.ChangeRule(3, "Fidz");

            var numberAsString = _fizzBuzz.Process(3);

            Assert.AreEqual("Fidz", numberAsString);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ChangeUnexistingRule()
        {
            _fizzBuzz.ChangeRule(2, "Zzap");
        }

        [TestMethod]
        public void ProcessSeveralNumbers()
        {
            _fizzBuzz.AddRule(2, "Hello");
            _fizzBuzz.AddRule(7, "World");

            var expected = new string[] { "1", "Hello", "Fizz", "Hello", "Buzz", "HelloFizz", "World", "Hello", "Fizz", "HelloBuzz" };

            for (int i = 1; i <= 10; i++)
                Assert.AreEqual(expected[i - 1], _fizzBuzz.Process(i));
        }
    }
}
