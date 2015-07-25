using System;
using System.Collections.Generic;
using mp3lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mp3lib_Tests
{
	[TestClass]
	public class DataExtracterTests
	{
        //[TODO] need tests
		[TestMethod]
		public void Test_GetTags()
		{
			var testInput = "{aaaa}{bbb}";
			var extracter = new DataExtracterFromFileName(testInput);

			var expected = new Queue<string>();
			expected.Enqueue("{aaaa}");
			expected.Enqueue("{bbb}");
			var actual = extracter.GetTags();

			Assert.AreEqual(actual.Count, expected.Count);
			for (var i = 0; i < actual.Count; i++)
			{
				var act = actual.Dequeue();
				var exp = expected.Dequeue();
				Assert.AreEqual(act.Value, exp);
			}
		}
	}
}
