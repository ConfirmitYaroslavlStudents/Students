using System;
using mp3lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mp3lib_Tests
{
	[TestClass]
	public class DiffTests
	{
		[TestMethod]
		public void Test_Creation()
		{
			var diff = new Diff { FileNameValue = "1", TagValue = "2" };

			Assert.AreEqual(diff.FileNameValue, "1");
			Assert.AreEqual(diff.TagValue, "2");
		}
	}
}
