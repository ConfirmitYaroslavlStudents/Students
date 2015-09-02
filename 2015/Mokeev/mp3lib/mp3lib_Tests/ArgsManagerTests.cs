using System;
using mp3lib;
using mp3lib.Args_Managing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mp3lib_Tests
{
	[TestClass]
	public class ArgsManagerTests
	{
		[TestMethod]
		public void Test_CheckArgs_Valid()
		{
			var data = new[] { "-path", "filePath", "-mask", "{a}-{b}", "-action", "file-rename" };
			var argsManager = new ArgsManager(data);

			Assert.AreEqual(true, argsManager.CheckArgsValidity());
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException), "Expected usage: \n\t-action [analyse|file-rename|change-tags] -path \"[path to file]\" -mask \"[mask for changing title]\" \n\tor \n\t-")]
		public void Test_CheckArgs_InvalidArgsCount()
		{
			var data = new[] { "-path", "-mask", "{a}-{b}", "-action", "file-rename" };
			var argsManager = new ArgsManager(data);
			argsManager.CheckArgsValidity();
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException), "You don't append correct file path and mask for mp3.")]
		public void Test_CheckArgs_InvalidOrder_1()
		{
			var data = new[] { "-path", "filePath", "{a}-{b}", "-mask", "-action", "file-rename" };
			var argsManager = new ArgsManager(data);
			argsManager.CheckArgsValidity();
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException), "You don't append correct file path and mask for mp3.")]
		public void Test_CheckArgs_InvalidOrder_2()
		{
			var data = new[] { "-path", "-mask", "filePath", "{a}-{b}", "-action", "file-rename" };
			var argsManager = new ArgsManager(data);
			argsManager.CheckArgsValidity();
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException), "You don't append correct file path.")]
		public void Test_CheckArgs_InvalidMissFilePath()
		{
			var data = new[] { "-data", "data", "-mask", "{a}-{b}", "-action", "file-rename" };
			var argsManager = new ArgsManager(data);
			argsManager.CheckArgsValidity();
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException), "You don't append correct mask for mp3.")]
		public void Test_CheckArgs_InvalidMissMask()
		{
			var data = new[] { "-path", "filePath", "-data", "{a}-{b}", "-action", "file-rename" };
			var argsManager = new ArgsManager(data);
			argsManager.CheckArgsValidity();
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException), "You don't append correct file path and mask for mp3.")]
		public void Test_CheckArgs_InvalidMissFilePathAndMask()
		{
			var data = new[] { "-data1", "filePath", "-data", "{a}-{b}", "-action", "file-rename" };
			var argsManager = new ArgsManager(data);
			argsManager.CheckArgsValidity();
		}


		[TestMethod]
		public void Test_ExtractArgs()
		{
			var validator = new ArgsManager(new[] { "-path", "something", "-mask", "{asd}{dsa}", "-action", "file-rename" });
			validator.CheckArgsValidity();
			var args = validator.ExtactArgs();

			Assert.AreEqual(args.Path, "something");
			Assert.AreEqual(args.Mask, "{asd}{dsa}");
		}

	}
}
