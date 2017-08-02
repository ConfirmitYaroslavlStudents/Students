using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExampleTDD;

namespace ExampleTDDTests
{
    [TestClass]
    public class BananaFizzBuzzTest
    {
        private BananaFizzBuzz _fizzBuzz;

        [TestInitialize]
        public void TestInitialize()
        {
            _fizzBuzz = new BananaFizzBuzz();
        }

        [TestMethod]
        public void GetCorrectStringForNumberOne()
        {
            var numberAsString = _fizzBuzz.Process(1);

            Assert.AreEqual("1", numberAsString);
        }

        [TestMethod]
        public void GetCorrectStringForNumberFive()
        {
            var numberAsString = _fizzBuzz.Process(5);

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
        public void GetCorrectStringForNumberTwo()
        {
            var numberAsString = _fizzBuzz.Process(2);

            Assert.AreEqual("Banana", numberAsString);
        }

        [TestMethod]
        public void GetCorrectStringForNumberSix()
        {
            var numberAsString = _fizzBuzz.Process(6);

            Assert.AreEqual("BananaFizz", numberAsString);
        }

        [TestMethod]
        public void GetCorrectStringForNumberTen()
        {
            var numberAsString = _fizzBuzz.Process(10);

            Assert.AreEqual("BananaBuzz", numberAsString);
        }

        [TestMethod]
        public void GetCorrectStringForNumberThirty()
        {
            var numberAsString = _fizzBuzz.Process(30);

            Assert.AreEqual("BananaFizzBuzz", numberAsString);
        }
    }
}