using System.Collections.Generic;
using mp3lib;

namespace mp3lib_Tests.Classes_for_tests
{
	public class TestMp3File : IMp3File
	{
		public string Album { get; set; }
		public string Artist { get; set; }
		public string Comment { get; set; }
		public string FilePath { get; }
		public string Genre { get; set; }
		public string Title { get; set; }
		public string TrackId { get; set; }
		public string Year { get; set; }

		public string this[TagType type]
		{
			get { throw new System.NotImplementedException(); }
			set { throw new System.NotImplementedException(); }
		}

		public TestMp3File(string filePath)
		{
			FilePath = filePath;
		}

		public Dictionary<TagType, string> GetId3Data()
		{
			return new Dictionary<TagType, string>
			{
				{ TagType.Album, Album },
				{ TagType.Artist, Artist },
				{ TagType.Comment, Comment },
				{ TagType.Genre, Genre },
				{ TagType.Title, Title },
				{ TagType.Id, TrackId },
				{ TagType.Year, Year },
			};
		}

		public void ChangeFileName(string filePath)
		{
			
		}
	}
}