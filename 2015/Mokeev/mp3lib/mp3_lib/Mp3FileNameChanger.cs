using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mp3lib
{
	public class Mp3FileNameChanger
	{
		private IMp3File Mp3File { get; set; }

		private readonly DataExtracter _dataExtracter;

		private Dictionary<TagType, string> Id3Data { get; set; }

		public Mp3FileNameChanger(IMp3File file, string mask)
		{
			Mp3File = file;
			Id3Data = new Dictionary<TagType, string>();

			_dataExtracter = new DataExtracter(mask);
		}


		public void AddTagReplacement(TagType type, string id3Data)
		{
			Id3Data.Add(type, id3Data);
		}

		//TODO: tests {READY}
		public string GetNewFileName()
		{
			var tags = _dataExtracter.GetTags();

			var id3Data = Id3Data.Count > 0 ? Id3Data : Mp3File.GetId3Data();

			var emptyTags = id3Data.Where(tag => string.IsNullOrWhiteSpace(tag.Value)).Select(x=> x.Key).ToArray();

			var reallyImportantAndEmptyTags = from tag in tags
											  where emptyTags.Contains(tag)
											  select tag;


			foreach (var tag in reallyImportantAndEmptyTags)
			{
				throw new Exception("MP3 Mp3File hasn't tag " + tag);
			}

			var resultFileName = new StringBuilder(_dataExtracter.Mask);

			foreach (var tagInfo in id3Data)
			{
				resultFileName.Replace("{" + tagInfo.Key.ToString().ToLower() + "}", tagInfo.Value);
			}

			return resultFileName.ToString();
		}
	}
}