using System.Collections.Generic;

namespace mp3lib
{
	public interface IMp3File
	{
		string Album { get; set; }
		string Artist { get; set; }
		string Comment { get; set; }
		string FilePath { get; }
		string Genre { get; set; }
		string Title { get; set; }
		string TrackId { get; set; }
		string Year { get; set; }

		string this[TagType type] { get; set; }

		Dictionary<TagType, string> GetId3Data();
		void ChangeFileName(string fileName);
	}
}