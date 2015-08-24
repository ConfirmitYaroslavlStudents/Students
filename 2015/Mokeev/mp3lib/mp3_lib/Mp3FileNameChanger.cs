using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using mp3lib.Rollback;

namespace mp3lib
{
	public class Mp3FileNameChanger : IRecoverable
	{
		private readonly IMp3File _mp3File;
		private readonly DataExtracter _dataExtracter;

		private readonly ISaver _saver;
		private readonly string _mask;
		private StringBuilder _data;

		private Dictionary<TagType, string> Id3Data { get; }
		private readonly bool _fastChanges;

		public Mp3FileNameChanger(IMp3File file, string mask, ISaver saver, bool fastChanges = false)
		{
			_mp3File = file;
			Id3Data = new Dictionary<TagType, string>();

			_dataExtracter = new DataExtracter(mask);

			_fastChanges = fastChanges;

			_saver = saver;
			_mask = mask;
		}


		public void AddTagReplacement(TagType type, string id3Data)
		{
			Id3Data.Add(type, id3Data);
		}

		public string GetNewFileName()
		{
			var tags = _dataExtracter.GetTags();

			if (_fastChanges && Id3Data.Count == 0) return Path.GetFileNameWithoutExtension(_mp3File.FilePath);

			Dictionary<TagType, string> id3Data;
			if (Id3Data.Count > 0)
			{
				id3Data = Id3Data;
				var keys = _mp3File.GetId3Data().Where(tag => !Id3Data.ContainsKey(tag.Key) && tags.Contains(tag.Key)).ToDictionary(tag => tag.Key, tag => tag.Value);

				foreach (var item in  keys)
				{
					id3Data.Add(item.Key, item.Value);
				}
			}
			else
			{
				id3Data = _mp3File.GetId3Data();
			}

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

			_data = resultFileName;

			return resultFileName.ToString();
		}

		public void Dispose()
		{
			new RollbackManager(_saver).AddAction(new RollbackInfo(ProgramAction.FileRename, new[] {_mp3File.FilePath}, _mask,
				_data.ToString())).Dispose();
		}

		public void Rollback(RollbackInfo info)
		{
			var data = info.Data as string;
			if(data == null) return;

            _mp3File.ChangeFileName(data);
		}
    }
}