using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace mp3lib
{
	public class Mp3TagChanger
	{
		private string Mp3RealName { get; }
		private IMp3File Mp3 { get; }

		private readonly DataExtracter _dataExtracter;

		public Mp3TagChanger(IMp3File mp3File, string mask)
		{
			Mp3 = mp3File;

			Mp3RealName = Path.GetFileNameWithoutExtension(mp3File.FilePath);
			_dataExtracter = new DataExtracter(mask);
		}

		//TODO: tests {READY}
		public void ChangeTags()
		{
			var tags = _dataExtracter.GetTags();
			var prefixesQueue = _dataExtracter.FindAllPrefixes(tags);

			var data = _dataExtracter.GetFullDataFromString(prefixesQueue, Mp3RealName, tags);

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
						Mp3.TrackId = item.Value;
						break;
					case TagType.Title:
						Mp3.Title = item.Value;
						break;
					case TagType.Album:
						Mp3.Album = item.Value;
						break;
					case TagType.Genre:
						Mp3.Genre = item.Value;
						break;
					case TagType.Year:
						Mp3.Year = item.Value;
						break;
					case TagType.Comment:
						Mp3.Comment = item.Value;
						break;
				}
			}
		}
	}
}