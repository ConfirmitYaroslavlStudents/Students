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

		private readonly DataExtracterFromFileName _dataExtracterFromFileName;

		public Mp3TagChanger(Mp3File mp3File, string mask)
		{
			Mp3 = mp3File;

			Mp3RealName = Path.GetFileNameWithoutExtension(mp3File.FilePath);
			_dataExtracterFromFileName = new DataExtracterFromFileName(mask);
		}

		public void ChangeTags()
		{
			var tags = _dataExtracterFromFileName.GetTags();
			var prefixesQueue = _dataExtracterFromFileName.FindAllPrefixes(tags);

			var mp3Name = new StringBuilder(Mp3RealName);

			var data = _dataExtracterFromFileName.GetFullDataFromString(prefixesQueue, mp3Name, tags);

			ChangeMp3Tags(data);
		}

		private void ChangeMp3Tags(Dictionary<string, string> data)
		{
			foreach (var item in data)
			{
				switch (item.Key)
				{
					case "{title}":
						Mp3.Title = item.Value;
						break;
					case "{artist}":
						Mp3.Artist = item.Value;
						break;
					case "{id}":
						Mp3.TrackId = Convert.ToUInt16(item.Value);
						break;
					case "{album}":
						Mp3.Album = item.Value;
						break;
					case "{genre}":
						Mp3.Genre = Convert.ToUInt16(item.Value);
						break;
					case "{year}":
						Mp3.Year = Convert.ToUInt16(item.Value);
						break;
					case "{comment}":
						Mp3.Comment = item.Value;
						break;
				}
			}
		}
	}
}