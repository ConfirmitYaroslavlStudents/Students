using FizzBuzzLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace FizzBuzzTests
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
            var numberAsString = _fizzBuzz.GetReaction(1);

            Assert.AreEqual("1", numberAsString);
        }

        [TestMethod]
        public void GetCorrectStringForNumberTwo()
        {
            var numberAsString = _fizzBuzz.GetReaction(2);

            Assert.AreEqual("2", numberAsString);
        }

        [TestMethod]
        public void GetCorrectStringForNumberFive()
        {
            var numberAsString = _fizzBuzz.GetReaction(5);

            Assert.AreEqual("Buzz", numberAsString);
        }

        [TestMethod]
        public void GetCorrectStringForNumberTen()
        {
            var numberAsString = _fizzBuzz.GetReaction(10);

            Assert.AreEqual("Buzz", numberAsString);
        }

        [TestMethod]
        public void GetCorrectStringForNumberThree()
        {
            var numberAsString = _fizzBuzz.GetReaction(3);

            Assert.AreEqual("Fizz", numberAsString);
        }

        [TestMethod]
        public void GetCorrectStringForNumberFifteen()
        {
            var numberAsString = _fizzBuzz.GetReaction(15);

            Assert.AreEqual("FizzBuzz", numberAsString);
        }

        [TestMethod]
        public void AddNewDivisor()
        {
            _fizzBuzz.AddDivisor(2, "Vazz");

            var numberAsString = _fizzBuzz.GetReaction(20);

            Assert.AreEqual("VazzBuzz", numberAsString);
        }

        [TestMethod]
        public void RemoveExistentDivisor()
        {
            var actual = _fizzBuzz.RemoveDivisor(3);
            var expected = true;

            var numberAsString = _fizzBuzz.GetReaction(15);

            Assert.AreEqual("Buzz", numberAsString);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RemoveNonexistentDivisor()
        {
            var actual = _fizzBuzz.RemoveDivisor(2);
            var expected = false;

            var numberAsString = _fizzBuzz.GetReaction(15);

            Assert.AreEqual("FizzBuzz", numberAsString);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ChangeReactionForExistentDivisor()
        {
            var actual = _fizzBuzz.ChangeReactionForDivisor(5,"Vazz");
            var expected = true;

            var numberAsString = _fizzBuzz.GetReaction(15);

            Assert.AreEqual("FizzVazz", numberAsString);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ChangeReactionForNonexistentDivisor()
        {
            var actual = _fizzBuzz.ChangeReactionForDivisor(2,"Vazz");
            var expected = false;

            var numberAsString = _fizzBuzz.GetReaction(15);

            Assert.AreEqual("FizzBuzz", numberAsString);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConstructorTest()
        {
            var pairs = new KeyValuePair<int, string>[]
            {
                new KeyValuePair<int, string>(2, "Vash"),
                new KeyValuePair<int, string>(7, "Kak"),
                new KeyValuePair<int, string>(11, "Hard"),
                new KeyValuePair<int, string>(12, "Bass"),
            };
            _fizzBuzz = new FizzBuzz(pairs);

            var numberAsString = _fizzBuzz.GetReaction(13860);

            Assert.AreEqual("VashFizzBuzzKakHardBass", numberAsString);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddExistedDivisorException()
        {
            _fizzBuzz.AddDivisor(3, "Vazz");
        }
    }
}
