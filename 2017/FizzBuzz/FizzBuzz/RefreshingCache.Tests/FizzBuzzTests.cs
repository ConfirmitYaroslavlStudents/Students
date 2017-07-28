using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RefreshingCache.Tests
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
    }
}