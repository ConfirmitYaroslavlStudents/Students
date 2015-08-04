using System;
using System.IO;
using System.Linq;
using System.Text;

namespace mp3lib
{
	public class Mp3FileNameChanger
	{
		private IMp3File Mp3File { get; }

		private readonly DataExtracter _dataExtracter;

		public Mp3FileNameChanger(IMp3File file, string mask)
		{
			Mp3File = file;

			_dataExtracter = new DataExtracter(mask);
		}

		//TODO: tests
		public string GetNewFileName()
		{
			var tags = _dataExtracter.GetTags();

			var id3Data = Mp3File.GetId3Data();

			foreach (var tag in tags.Where(tag => string.IsNullOrWhiteSpace(id3Data[tag])))
			{
				throw new Exception("MP3 Mp3File hasn't tag " + tag);
			}

			var resultFileName = new StringBuilder(_dataExtracter.Mask);

			foreach (var tag in tags)
			{
				resultFileName.Replace("{" + tag + "}", id3Data[tag]);
			}

			return resultFileName.ToString();
		}
	}
}