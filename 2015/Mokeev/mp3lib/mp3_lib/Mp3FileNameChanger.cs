using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace mp3lib
{
	public class Mp3FileNameChanger
	{
		private IMp3File Mp3File { get; set; }

		private readonly DataExtracter _dataExtracter;

		private Dictionary<TagType, string> Id3Data { get; set; }
		private bool _fastChanges;

		public Mp3FileNameChanger(IMp3File file, string mask, bool fastChanges = false)
		{
			Mp3File = file;
			Id3Data = new Dictionary<TagType, string>();

			_dataExtracter = new DataExtracter(mask);

			_fastChanges = fastChanges;
		}


		public void AddTagReplacement(TagType type, string id3Data)
		{
			Id3Data.Add(type, id3Data);
		}

		//TODO: tests {READY}
		public string GetNewFileName()
		{
			var tags = _dataExtracter.GetTags();

			if (_fastChanges && Id3Data.Count == 0) return Path.GetFileNameWithoutExtension(Mp3File.FilePath);

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