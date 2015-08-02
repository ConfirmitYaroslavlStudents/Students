using System;
using System.Collections.Generic;
using mp3lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mp3lib_Tests
{
	[TestClass]
	public class FileDifferencesTests
	{
		private FileDifferences _fileDiff;
		private Diff _d1;
		private Diff _d2;
		private TestMp3File _mp3File;

		[TestInitialize]
		public void TestInit()
		{
			_mp3File = new TestMp3File("/test");
			_fileDiff = new FileDifferences(_mp3File);
			_d1 = new Diff { FileNameValue = "1", TagValue = "2" };
			_d2 = new Diff { FileNameValue = "art1", TagValue = "art2" };
			_fileDiff.Add(TagType.Id, _d1);
			_fileDiff.Add(TagType.Artist, _d2);
		}

		[TestMethod]
		public void Test_Add()
		{
			var expected = new Dictionary<TagType, Diff>
			{
				{ TagType.Id, _d1 },
				{ TagType.Artist, _d2 },
			};

			CollectionAssert.AreEqual(expected, _fileDiff.Diffs);
		}

		[TestMethod]
		public void Test_Path()
		{
			Assert.AreEqual(_mp3File, _fileDiff.Mp3File);
		}
	}
}
