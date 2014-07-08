using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FizzBuzz.Tests
{
	[TestClass]
	public class FizzBuzzerTests
	{
		[TestMethod]
		public void Pass7GetString7()
		{
			var fizzBuzzer = new FizzBuzzer();

			var result = fizzBuzzer.ToString(7);

			Assert.AreEqual(result, "7");
		}

		[TestMethod]
		public void Pass3GetStringFizz()
		{
			var fizzBuzzer = new FizzBuzzer();

			var result = fizzBuzzer.ToString(3);

			Assert.AreEqual(result, "Fizz");
		}

		[TestMethod]
		public void Pass5GetStringBuzz()
		{
			var fizzBuzzer = new FizzBuzzer();

			var result = fizzBuzzer.ToString(5);

			Assert.AreEqual(result, "Buzz");
		}

		[TestMethod]
		public void Pass7GetStringFasterWhenPair7FasterAdded()
		{
			var fizzBuzzer = new FizzBuzzer();

			fizzBuzzer.AddNumberStringPair(7, "Faster");
			var result = fizzBuzzer.ToString(7);

			Assert.AreEqual(result, "Faster");
		}

		[TestMethod]
		public void Pass15GetStringFizzBuzz()
		{
			var fizzBuzzer = new FizzBuzzer();

			var result = fizzBuzzer.ToString(15);

			Assert.AreEqual(result, "Fizz Buzz");
		}

		[TestMethod]
		public void Pass21GetStringFizzFasterWhenPair7FasterAdded()
		{
			var fizzBuzzer = new FizzBuzzer();

			fizzBuzzer.AddNumberStringPair(7, "Faster");
			var result = fizzBuzzer.ToString(21);

			Assert.AreEqual(result, "Fizz Faster");
		}

		[TestMethod]
		public void Pass105GetStringFizzBuzzFasterWhenPair7FasterAdded()
		{
			var fizzBuzzer = new FizzBuzzer();

			fizzBuzzer.AddNumberStringPair(7, "Faster");
			var result = fizzBuzzer.ToString(105);

			Assert.AreEqual(result, "Fizz Buzz Faster");
		}

		[TestMethod]
		public void Pass3GetString3WhenKey3Deleted()
		{
			var fizzBuzzer = new FizzBuzzer();

			fizzBuzzer.DeleteNumberStringPair(3);
			var result = fizzBuzzer.ToString(3);

			Assert.AreEqual(result, "3");
		}

		[TestMethod]
		public void Pass3GetStringHarderWhenKey3DeletedAndPair3HarderAdded()
		{
			var fizzBuzzer = new FizzBuzzer();

			fizzBuzzer.DeleteNumberStringPair(3);
			fizzBuzzer.AddNumberStringPair(3, "Harder");
			var result = fizzBuzzer.ToString(3);

			Assert.AreEqual(result, "Harder");
		}

		[TestMethod, ExpectedException(typeof(ZeroCanNotBeKeyException))]
		public void ZeroCanNotBeKey()
		{
			var fizzBuzzer = new FizzBuzzer();

			fizzBuzzer.AddNumberStringPair(0, "Zero");
		}

		[TestMethod, ExpectedException(typeof(FizzBuzzerHasThisKeyException))]
		public void FizzBuzzerCanNotContain2SameKeys()
		{
			var fizzBuzzer = new FizzBuzzer();

			fizzBuzzer.AddNumberStringPair(3, "Tree");
		}
	}
}
