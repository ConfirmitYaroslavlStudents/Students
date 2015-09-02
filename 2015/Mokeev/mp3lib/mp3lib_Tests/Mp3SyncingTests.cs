using mp3lib;
using mp3lib.Core.Actions;
using mp3lib_Tests.Classes_for_tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mp3lib_Tests
{
	[TestClass]
	public class Mp3SyncingTests
	{
		[TestMethod]
		public void Test_GetDataFromFileName()
		{
			var communicator = new TestCommunicator(userInput: new[]{
				"1",
				"1",
			});

			var item = new TestMp3File("2. name2 - title2.mp3") { TrackId = "1", Title = "title2", Artist = "name1" };

			var files = new[] {item};

			var syncer = new Mp3Syncing(files, "{id}. {artist} - {title}", communicator, new TestRollbackSaver(new[] { "" }));
			syncer.SyncFiles();

			Assert.AreEqual(item.TrackId, "2");
			Assert.AreEqual(item.Artist, "name2");
			Assert.AreEqual(item.Title, "title2");

		}

		[TestMethod]
		public void Test_GetDataFromTags()
		{
			var communicator = new TestCommunicator(userInput: new[]
			{
				"2",
			});

			var item = new TestMp3File("1. name3 - title3.mp3") { TrackId = "3", Title = "title3", Artist = "name3" };

			var files = new[] {item};

			var syncer = new Mp3Syncing(files, "{id}. {artist} - {title}", communicator, new TestRollbackSaver(new[] { "" }));
			syncer.SyncFiles();

			Assert.AreEqual(item.TrackId, "3");
			Assert.AreEqual(item.Artist, "name3");
			Assert.AreEqual(item.Title, "title3");
		}

		[TestMethod]
		public void Test_GetDataFromUser()
		{
			var communicator = new TestCommunicator(userInput: new[]{
				"3",
				"3", //it's entering a value
			});

			var item = new TestMp3File("1. name3 - title3.mp3") { TrackId = "3", Title = "title3", Artist = "name3" };

			var files = new[] {item};

			var syncer = new Mp3Syncing(files, "{id}. {artist} - {title}", communicator, new TestRollbackSaver(new[] { "" }));
			syncer.SyncFiles();

			Assert.AreEqual(item.TrackId, "3");
			Assert.AreEqual(item.Artist, "name3");
			Assert.AreEqual(item.Title, "title3");
		}
	}
}
