using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FizzBuzz.Tests
{
	[TestClass]
	public class FizzBuzzerTests
	{
		[TestMethod]
		public void Pass2GetString2()
		{
			var fizzBuzzer = new FizzBuzzer();

			fizzBuzzer.AddNumberStringPair(3, "Harder");
			fizzBuzzer.AddNumberStringPair(5, "Better");
			fizzBuzzer.AddNumberStringPair(7, "Faster");
			fizzBuzzer.AddNumberStringPair(11, "Stronger");

			Assert.AreEqual(fizzBuzzer.ToString(2), "2");
		}

		[TestMethod]
		public void Pass3GetStringHarder()
		{
			var fizzBuzzer = new FizzBuzzer();

			fizzBuzzer.AddNumberStringPair(3, "Harder");
			fizzBuzzer.AddNumberStringPair(5, "Better");
			fizzBuzzer.AddNumberStringPair(7, "Faster");
			fizzBuzzer.AddNumberStringPair(11, "Stronger");

			Assert.AreEqual(fizzBuzzer.ToString(3), "Harder");
		}

		[TestMethod]
		public void Pass5GetStringBetter()
		{
			var fizzBuzzer = new FizzBuzzer();

			fizzBuzzer.AddNumberStringPair(3, "Harder");
			fizzBuzzer.AddNumberStringPair(5, "Better");
			fizzBuzzer.AddNumberStringPair(7, "Faster");
			fizzBuzzer.AddNumberStringPair(11, "Stronger");

			Assert.AreEqual(fizzBuzzer.ToString(5), "Better");
		}

		[TestMethod]
		public void Pass7GetStringFaster()
		{
			var fizzBuzzer = new FizzBuzzer();

			fizzBuzzer.AddNumberStringPair(3, "Harder");
			fizzBuzzer.AddNumberStringPair(5, "Better");
			fizzBuzzer.AddNumberStringPair(7, "Faster");
			fizzBuzzer.AddNumberStringPair(11, "Stronger");

			Assert.AreEqual(fizzBuzzer.ToString(7), "Faster");
		}

		[TestMethod]
		public void Pass11GetStringStronger()
		{
			var fizzBuzzer = new FizzBuzzer();

			fizzBuzzer.AddNumberStringPair(3, "Harder");
			fizzBuzzer.AddNumberStringPair(5, "Better");
			fizzBuzzer.AddNumberStringPair(7, "Faster");
			fizzBuzzer.AddNumberStringPair(11, "Stronger");

			Assert.AreEqual(fizzBuzzer.ToString(11), "Stronger");
		}

		[TestMethod]
		public void Pass15GetStringHarderBetter()
		{
			var fizzBuzzer = new FizzBuzzer();

			fizzBuzzer.AddNumberStringPair(3, "Harder");
			fizzBuzzer.AddNumberStringPair(5, "Better");
			fizzBuzzer.AddNumberStringPair(7, "Faster");
			fizzBuzzer.AddNumberStringPair(11, "Stronger");

			Assert.AreEqual(fizzBuzzer.ToString(15), "Harder Better");
		}

		[TestMethod]
		public void Pass21GetStringHarderFaster()
		{
			var fizzBuzzer = new FizzBuzzer();

			fizzBuzzer.AddNumberStringPair(3, "Harder");
			fizzBuzzer.AddNumberStringPair(5, "Better");
			fizzBuzzer.AddNumberStringPair(7, "Faster");
			fizzBuzzer.AddNumberStringPair(11, "Stronger");

			Assert.AreEqual(fizzBuzzer.ToString(21), "Harder Faster");
		}

		[TestMethod]
		public void Pass33GetStringHarderStronger()
		{
			var fizzBuzzer = new FizzBuzzer();

			fizzBuzzer.AddNumberStringPair(3, "Harder");
			fizzBuzzer.AddNumberStringPair(5, "Better");
			fizzBuzzer.AddNumberStringPair(7, "Faster");
			fizzBuzzer.AddNumberStringPair(11, "Stronger");

			Assert.AreEqual(fizzBuzzer.ToString(33), "Harder Stronger");
		}

		[TestMethod]
		public void Pass35GetStringBetterFaster()
		{
			var fizzBuzzer = new FizzBuzzer();

			fizzBuzzer.AddNumberStringPair(3, "Harder");
			fizzBuzzer.AddNumberStringPair(5, "Better");
			fizzBuzzer.AddNumberStringPair(7, "Faster");
			fizzBuzzer.AddNumberStringPair(11, "Stronger");

			Assert.AreEqual(fizzBuzzer.ToString(35), "Better Faster");
		}

		[TestMethod]
		public void Pass55GetStringBetterStronger()
		{
			var fizzBuzzer = new FizzBuzzer();

			fizzBuzzer.AddNumberStringPair(3, "Harder");
			fizzBuzzer.AddNumberStringPair(5, "Better");
			fizzBuzzer.AddNumberStringPair(7, "Faster");
			fizzBuzzer.AddNumberStringPair(11, "Stronger");

			Assert.AreEqual(fizzBuzzer.ToString(55), "Better Stronger");
		}

		[TestMethod]
		public void Pass77GetStringFasterStronger()
		{
			var fizzBuzzer = new FizzBuzzer();

			fizzBuzzer.AddNumberStringPair(3, "Harder");
			fizzBuzzer.AddNumberStringPair(5, "Better");
			fizzBuzzer.AddNumberStringPair(7, "Faster");
			fizzBuzzer.AddNumberStringPair(11, "Stronger");

			Assert.AreEqual(fizzBuzzer.ToString(77), "Faster Stronger");
		}

		[TestMethod]
		public void Pass105GetStringHarderBetterFaster()
		{
			var fizzBuzzer = new FizzBuzzer();

			fizzBuzzer.AddNumberStringPair(3, "Harder");
			fizzBuzzer.AddNumberStringPair(5, "Better");
			fizzBuzzer.AddNumberStringPair(7, "Faster");
			fizzBuzzer.AddNumberStringPair(11, "Stronger");

			Assert.AreEqual(fizzBuzzer.ToString(105), "Harder Better Faster");
		}

		[TestMethod]
		public void Pass165GetStringHarderBetterStronger()
		{
			var fizzBuzzer = new FizzBuzzer();

			fizzBuzzer.AddNumberStringPair(3, "Harder");
			fizzBuzzer.AddNumberStringPair(5, "Better");
			fizzBuzzer.AddNumberStringPair(7, "Faster");
			fizzBuzzer.AddNumberStringPair(11, "Stronger");

			Assert.AreEqual(fizzBuzzer.ToString(165), "Harder Better Stronger");
		}

		[TestMethod]
		public void Pass231GetStringHarderFasterStronger()
		{
			var fizzBuzzer = new FizzBuzzer();

			fizzBuzzer.AddNumberStringPair(3, "Harder");
			fizzBuzzer.AddNumberStringPair(5, "Better");
			fizzBuzzer.AddNumberStringPair(7, "Faster");
			fizzBuzzer.AddNumberStringPair(11, "Stronger");

			Assert.AreEqual(fizzBuzzer.ToString(231), "Harder Faster Stronger");
		}

		[TestMethod]
		public void Pass385GetStringBetterFasterStronger()
		{
			var fizzBuzzer = new FizzBuzzer();

			fizzBuzzer.AddNumberStringPair(3, "Harder");
			fizzBuzzer.AddNumberStringPair(5, "Better");
			fizzBuzzer.AddNumberStringPair(7, "Faster");
			fizzBuzzer.AddNumberStringPair(11, "Stronger");

			Assert.AreEqual(fizzBuzzer.ToString(385), "Better Faster Stronger");
		}

		[TestMethod]
		public void Pass1155GetStringHarderBetterFasterStronger()
		{
			var fizzBuzzer = new FizzBuzzer();

			fizzBuzzer.AddNumberStringPair(3, "Harder");
			fizzBuzzer.AddNumberStringPair(5, "Better");
			fizzBuzzer.AddNumberStringPair(7, "Faster");
			fizzBuzzer.AddNumberStringPair(11, "Stronger");

			Assert.AreEqual(fizzBuzzer.ToString(1155), "Harder Better Faster Stronger");
		}
	}
}
