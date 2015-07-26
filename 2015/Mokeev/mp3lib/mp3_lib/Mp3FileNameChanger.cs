using System;
using System.Linq;
using System.Text;

namespace mp3lib
{
	public class Mp3FileNameChanger
	{
		private Mp3File Mp3File { get; set; }

		private readonly DataExtracter _dataExtracter;

		public Mp3FileNameChanger(Mp3File file, string mask)
		{
			Mp3File = file;

			_dataExtracter = new DataExtracter(mask);
		}

		public void ChangeFileName()
		{
			var tags = _dataExtracter.GetTags();

			//checks that all tags are valid
			foreach (var tag in tags.Where(tag => !_dataExtracter.AllowedStrings.Contains(tag.Value)))
			{
				throw new Exception("There is no tag like " + tag.Value);
			}

			var id3Data = Mp3File.GetId3Data();

			foreach (var tag in tags.Where(tag => string.IsNullOrWhiteSpace(id3Data[tag.Value])))
			{
				throw new Exception("MP3 File hasn't tag " + tag);
			}

			var resultFileName = new StringBuilder(_dataExtracter.Mask);

			foreach (var tag in tags)
			{
				resultFileName.Replace(tag.Value, id3Data[tag.Value]);
			}

		}
	}
}