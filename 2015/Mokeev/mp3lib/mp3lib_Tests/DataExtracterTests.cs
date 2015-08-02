using System;
using System.Collections.Generic;
using System.Text;
using mp3lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mp3lib_Tests
{
	[TestClass]
	public class DataExtracterTests
	{
		[TestMethod]
		public void Test_GetTags()
		{
			var extracter = new DataExtracter("{artist}{id}");

			var expected = new Queue<TagType>();
			expected.Enqueue(TagType.Artist);
			expected.Enqueue(TagType.Id);
			var actual = extracter.GetTags();

			Assert.AreEqual(actual.Count, expected.Count);
			for (var i = 0; i < actual.Count; i++)
			{
				var act = actual.Dequeue();
				var exp = expected.Dequeue();
				Assert.AreEqual(act, exp);
			}
		}

		[TestMethod]
		public void Test_FindAllPrefixes()
		{
			var extracter = new DataExtracter("-this-is-{artist}-prefix-{title}");
			var prefixes = extracter.FindAllPrefixes(extracter.GetTags());

			var expected = new Queue<string>();
			expected.Enqueue("-this-is-");
			expected.Enqueue("-prefix-");

			CollectionAssert.AreEqual(expected, prefixes);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException), "Wrong type sended: {aaaa}")]
		public void Test_FindAllPrefixes_WrongArgumentPassed()
		{
			var extracter = new DataExtracter("-this-is-{aaaa}-prefix-{id}");
			var prefixes = extracter.FindAllPrefixes(extracter.GetTags());

			var expected = new Queue<string>();
			expected.Enqueue("-this-is-");
			expected.Enqueue("-prefix-");

			CollectionAssert.AreEqual(expected, prefixes);
		}

		[TestMethod]
		public void Test_GetFullDataFromString()
		{
			var extracter = new DataExtracter("{id}. {artist} - {title}");
			var tags = extracter.GetTags();
			var prefixes = extracter.FindAllPrefixes(tags);

			var data = extracter.GetFullDataFromString(prefixes, "10. test artist - test song name", tags);

			var expected = new Dictionary<TagType, string> { { TagType.Id, "10" }, { TagType.Artist, "test artist" }, { TagType.Title, "test song name" } };

			CollectionAssert.AreEqual(expected, data);
		}

		[TestMethod]
		[ExpectedException(typeof(Exception), "Too low prefixes count. Undefined state found.")]
		public void Test_GetFullDataFromString_DelimiterExpected()
		{
			var extracter = new DataExtracter("{id}{artist} - {title}");
			var tags = extracter.GetTags();
			var prefixes = extracter.FindAllPrefixes(tags);

			var data = extracter.GetFullDataFromString(prefixes, "10. test artist - test song name", tags);
		}
	}
}
