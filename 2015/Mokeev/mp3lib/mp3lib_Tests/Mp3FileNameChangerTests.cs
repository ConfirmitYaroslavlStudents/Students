using System;
using System.IO;
using mp3lib;
using mp3lib_Tests.Classes_for_tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mp3lib_Tests
{
	[TestClass]
	public class Mp3FileNameChangerTests
	{
		[TestMethod]
		public void Test_NewFileName()
		{
			var mp3 = new TestMp3File("C:/test/mp3s/1. asd - qwe.mp3") {Artist = "art", TrackId = "1", Title = "ttl"};
			var renamer = new Mp3FileNameChanger(mp3, "{id}. {artist} - {title}", new TestRollbackSaver(new [] {""}));

			Assert.AreEqual("1. art - ttl", renamer.GetNewFileName());
		}

		[TestMethod]
		public void Test_ManualFileNameReplacements()
		{
			var mp3 = new TestMp3File("C:/test/mp3s/1. asd - qwe.mp3") { Artist = "art", TrackId = "1", Title = "ttl" };
			var renamer = new Mp3FileNameChanger(mp3, "{id}. {artist} - {title}", new TestRollbackSaver(new[] { "" }));
			renamer.AddTagReplacement(TagType.Id, mp3.TrackId);
			renamer.AddTagReplacement(TagType.Artist, mp3.Artist);
			renamer.AddTagReplacement(TagType.Title, mp3.Title);

			Assert.AreEqual("1. art - ttl", renamer.GetNewFileName());
		}


	}
}
