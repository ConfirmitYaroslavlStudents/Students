using System;
using System.Collections.Generic;
using mp3lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mp3lib_Tests
{
	[TestClass]
	public class ArgsManagerTests
	{
		[TestMethod]
		public void Test_CheckArgs_Valid()
		{
			var data = new[] {"-file", "filePath", "-mask", "{a}-{b}"};
			var argsManager = new ArgsManager(data);

			Assert.AreEqual(true, argsManager.CheckArgsValidity());
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException), "Expected usage: {0} -file \"[path to file]\" -mask \"[mask for changing title]\"")]
		public void Test_CheckArgs_InvalidArgsCount()
		{
			var data = new[] { "-file", "-mask", "{a}-{b}" };
			var argsManager = new ArgsManager(data);
			argsManager.CheckArgsValidity();
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException), "You don't append correct file path and mask for mp3.")]
		public void Test_CheckArgs_InvalidOrder_1()
		{
			var data = new[] {"-file", "filePath", "{a}-{b}", "-mask"};
			var argsManager = new ArgsManager(data);
			argsManager.CheckArgsValidity();
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException), "You don't append correct file path and mask for mp3.")]
		public void Test_CheckArgs_InvalidOrder_2()
		{
			var data = new[] {"-file", "-mask", "filePath", "{a}-{b}"};
			var argsManager = new ArgsManager(data);
			argsManager.CheckArgsValidity();
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException), "You don't append correct file path.")]
		public void Test_CheckArgs_InvalidMissFilePath()
		{
			var data = new[] { "-data", "data", "-mask", "{a}-{b}" };
			var argsManager = new ArgsManager(data);
			argsManager.CheckArgsValidity();
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException), "You don't append correct mask for mp3.")]
		public void Test_CheckArgs_InvalidMissMask()
		{
			var data = new[] { "-file", "filePath", "-data", "{a}-{b}" };
			var argsManager = new ArgsManager(data);
			argsManager.CheckArgsValidity();
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException), "You don't append correct file path and mask for mp3.")]
		public void Test_CheckArgs_InvalidMissFilePathAndMask()
		{
			var data = new[] { "-data1", "filePath", "-data", "{a}-{b}" };
			var argsManager = new ArgsManager(data);
			argsManager.CheckArgsValidity();
		}


		[TestMethod]
		public void Test_ExtractArgs()
		{
			var validator = new ArgsManager(new []{"-file", "something", "-mask", "{asd}{dsa}"});
			var args = validator.ExtactArgs();

			Assert.AreEqual(args.FilePath, "something");
			Assert.AreEqual(args.Mask, "{asd}{dsa}");
		}

	}
}
