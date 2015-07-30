using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace mp3lib
{
    //[TODO] extract classes
	public class Mp3TagChanger
	{
		private string Mp3RealName { get; set; }
		private Mp3File Mp3 { get; set; }

		private readonly DataExtracter _dataExtracter;

		public Mp3TagChanger(Mp3File mp3File, string mask)
		{
			Mp3 = mp3File;

			Mp3RealName = Path.GetFileNameWithoutExtension(mp3File.FilePath);
			_dataExtracter = new DataExtracter(mask);
		}

        //[TODO] tests
		public void ChangeTags()
		{
			var tags = _dataExtracter.GetTags();
			var prefixesQueue = _dataExtracter.FindAllPrefixes(tags);

			var mp3Name = new StringBuilder(Mp3RealName);

			var data = _dataExtracter.GetFullDataFromString(prefixesQueue, mp3Name, tags);

			ChangeMp3Tags(data);
		}

		private void ChangeMp3Tags(Dictionary<TagType, string> data)
		{
			foreach (var item in data)
			{
				switch (item.Key)
				{
					case TagType.Artist:
						Mp3.Artist = item.Value;
						break;
					case TagType.Id:
						Mp3.TrackId = Convert.ToUInt16(item.Value);
						break;
					case TagType.Title:
						Mp3.Title = item.Value;
						break;
					case TagType.Album:
						Mp3.Album = item.Value;
						break;
					case TagType.Genre:
						Mp3.Genre = Convert.ToUInt16(item.Value);
						break;
					case TagType.Year:
						Mp3.Year = Convert.ToUInt16(item.Value);
						break;
					case TagType.Comment:
						Mp3.Comment = item.Value;
						break;
				}
			}
		}
	}
}