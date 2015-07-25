using System;
using mp3lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mp3lib_Tests
{
	[TestClass]
	public class ArgsClassTests
	{
		[TestMethod]
		public void Test_Creating()
		{
			var args = new Args("path", "mask");
			Assert.AreEqual(args.FilePath, "path");
			Assert.AreEqual(args.Mask, "mask");
		}
	}
}
