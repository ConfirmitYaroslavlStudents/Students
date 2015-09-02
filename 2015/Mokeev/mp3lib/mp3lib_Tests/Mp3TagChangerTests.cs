using System;
using System.Collections.Generic;
using mp3lib;
using mp3lib.Core.Actions;
using mp3lib_Tests.Classes_for_tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mp3lib_Tests
{
	[TestClass]
	public class Mp3TagChangerTests
	{
		[TestMethod]
		public void Test_Changer()
		{
			var file = new TestMp3File("[1999] 1. art1 - ttl1.mp3") { TrackId = "5", Artist = "art5", Title = "ttl5", Year = "1995"};
			const string mask = "[{year}] {id}. {artist} - {title}";
            var changer = new Mp3TagChanger(file, mask, new TestRollbackSaver(new List<string>()));
			changer.ChangeTags();

			Assert.AreEqual(file.Artist, "art1");
			Assert.AreEqual(file.TrackId, "1");
			Assert.AreEqual(file.Year, "1999");
			Assert.AreEqual(file.Title, "ttl1");
		}
	}
}
