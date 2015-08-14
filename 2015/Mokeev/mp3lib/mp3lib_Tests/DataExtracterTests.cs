using System;
using System.Collections.Generic;
using System.Text;
using mp3lib;
using mp3lib.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mp3lib_Tests
{
	[TestClass]
	public class DataExtracterTests
	{
		[TestMethod]
		public void Test_GetTags()
		{
			var extracter = new DataExtracter("{artist}{id}{title}{year}");

			var expected = new Queue<TagType>();
			expected.Enqueue(TagType.Artist);
			expected.Enqueue(TagType.Id);
			expected.Enqueue(TagType.Title);
			expected.Enqueue(TagType.Year);
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
		[ExpectedException(typeof(DataExctracterException), "Too low prefixes count. Undefined state found.")]
		public void Test_GetFullDataFromString_DelimiterExpected()
		{
			var extracter = new DataExtracter("{id}{artist} - {title}");
			var tags = extracter.GetTags();
			var prefixes = extracter.FindAllPrefixes(tags);

			var data = extracter.GetFullDataFromString(prefixes, "10. test artist - test song name", tags);
		}

		[TestMethod]
		public void Test_FirstPrefix()
		{
			var extracter = new DataExtracter("[{id}] {artist}");
			var tags = extracter.GetTags();
			var prefixes = extracter.FindAllPrefixes(tags);
			var data = extracter.GetFullDataFromString(prefixes, "[10] test artist", tags);

			var expected = new Dictionary<TagType, string> {{TagType.Id, "10"}, {TagType.Artist, "test artist"}};

			CollectionAssert.AreEqual(expected, data);
		}

		[TestMethod]
		public void Test_NotEnoughData()
		{

			var extracter = new DataExtracter("{id} {artist} - {title}");
			var tags = extracter.GetTags();
			var prefixes = extracter.FindAllPrefixes(tags);
			var data = extracter.GetFullDataFromString(prefixes, "10 art", tags);

			var expected = new Dictionary<TagType, string> { { TagType.Id, "10" }, { TagType.Artist, "" }, {TagType.Title, ""} };

			CollectionAssert.AreEqual(expected, data);
		}
	}
}
