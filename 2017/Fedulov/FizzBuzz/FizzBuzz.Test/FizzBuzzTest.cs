using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FizzBuzz.Test
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
        public void SetNewRule()
        {
            _fizzBuzz.AddRule(2, "Banana");

            var numberAsString = _fizzBuzz.Process(4);

            Assert.AreEqual("Banana", numberAsString);
        }

        [TestMethod]
        public void SetNewRuleAndCheckAllRules()
        {
            _fizzBuzz.AddRule(2, "Banana");

            var numberAsString = _fizzBuzz.Process(30);

            Assert.AreEqual("BananaFizzBuzz", numberAsString);
        }

        [TestMethod]
        public void IsRuleExist()
        {
            _fizzBuzz.AddRule(2, "Banana");

            Assert.IsTrue(_fizzBuzz.isRuleExist(2));
        }

        [TestMethod]
        public void SetNewRuleForNumberTwice()
        {
            _fizzBuzz.AddRule(2, "Banana");

            var numberAsString = _fizzBuzz.Process(14);

            try
            {
                _fizzBuzz.AddRule(2, "Panama");
                Assert.Fail("ArgumentException expected, but test passed");
            }
            catch (ArgumentException e)
            {
                Assert.IsNotNull(e);
                numberAsString = _fizzBuzz.Process(14);
                Assert.AreEqual("Banana", numberAsString);
            }    
        }

        [TestMethod]
        public void SetNewRuleRange()
        {
            _fizzBuzz.AddRuleRange(new Dictionary<int, string> {
                    { 2, "Banana" },
                    { 7, "Montana"}
                });

            var numberAsString = _fizzBuzz.Process(14);

            Assert.AreEqual("BananaMontana", numberAsString);
        }

        [TestMethod]
        public void ChangeRule()
        {
            _fizzBuzz.AddRule(2, "Banana");

            var numberAsString = _fizzBuzz.Process(4);

            _fizzBuzz.ChangeRule(2, "Panama");

            var newNumberAsString = _fizzBuzz.Process(4);

            Assert.AreEqual("Banana", numberAsString);
            Assert.AreEqual("Panama", newNumberAsString);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ChangeRuleWhichDoesNotExist()
        {
            _fizzBuzz.ChangeRule(2, "Panama");
        }

        [TestMethod]
        public void DeleteRule()
        {
            _fizzBuzz.AddRule(2, "Banana");
            var numberAsString = _fizzBuzz.Process(4);

            _fizzBuzz.RemoveRule(2);
            var newNumberAsString = _fizzBuzz.Process(4);

            Assert.AreEqual("Banana", numberAsString);
            Assert.AreEqual("4", newNumberAsString);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteRuleWhichDoesNotExist()
        {
            _fizzBuzz.RemoveRule(2);
        }
    }
}
