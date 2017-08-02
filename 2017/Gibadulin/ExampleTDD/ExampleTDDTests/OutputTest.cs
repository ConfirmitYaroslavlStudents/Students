using ExampleTDD;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExampleTDDTests
{
    [TestClass]
    public class OutputTest
    {
        [TestMethod]
        public void GetOutput()
        {
            var expected =
                "1 Banana Fizz Banana Buzz BananaFizz 7 Banana Fizz BananaBuzz 11 BananaFizz 13 Banana FizzBuzz Banana 17 BananaFizz 19 BananaBuzz Fizz Banana 23 BananaFizz Buzz Banana Fizz Banana 29 BananaFizzBuzz 31 Banana Fizz Banana Buzz BananaFizz 37 Banana Fizz BananaBuzz 41 BananaFizz 43 Banana FizzBuzz Banana 47 BananaFizz 49 BananaBuzz Fizz Banana 53 BananaFizz Buzz Banana Fizz Banana 59 BananaFizzBuzz 61 Banana Fizz Banana Buzz BananaFizz 67 Banana Fizz BananaBuzz 71 BananaFizz 73 Banana FizzBuzz Banana 77 BananaFizz 79 BananaBuzz Fizz Banana 83 BananaFizz Buzz Banana Fizz Banana 89 BananaFizzBuzz 91 Banana Fizz Banana Buzz BananaFizz 97 Banana Fizz BananaBuzz";

            var output = new Output();
            var actual = output.GetOutput();

            if (expected != actual)
                Assert.Fail();
        }
    }
}
