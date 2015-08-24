using System;
using System.Collections.Generic;
using System.Linq;
using mp3lib.Rollback;

namespace mp3lib
{
	public class Mp3Syncing : IRecoverable
	{
		private readonly IMp3File[] _files;
		private readonly ICommunication _communication;
		private readonly string _mask;
		private readonly List<KeyValuePair<string, KeyValuePair<string, Dictionary<TagType, string>>>> _data;
		private IEnumerable<string> _filesChanged;
		private readonly ISaver _saver;

		public Mp3Syncing(IEnumerable<IMp3File> files, string mask, ICommunication communication, ISaver saver)
		{
			_files = files.ToArray();
			_communication = communication;
			_mask = mask;

			_data = new List<KeyValuePair<string, KeyValuePair<string, Dictionary<TagType, string>>>>();
			_saver = saver;
		}

		public void SyncFiles()
		{
			if (!_files.Any())
			{
				_communication.SendMessage("Nothing to sync.");
				return;
			}

			var analyser = new Mp3FileAnalyser(_files, _mask);
			var diffs = analyser.GetDifferences();

			if (!diffs.Any())
			{
				_communication.SendMessage("All files synced.");
				return;
			}

			var filesChanged = new List<string>();

			foreach (var fileDifferencese in diffs)
			{
				var fileNameChanger = new Mp3FileNameChanger(fileDifferencese.Mp3File, _mask, _saver, true);
				filesChanged.Add(fileDifferencese.Mp3File.FilePath);
				var fData = new KeyValuePair<string, Dictionary<TagType, string>>(fileDifferencese.Mp3File.FilePath, fileDifferencese.Mp3File.GetId3Data());
				foreach (var diff in fileDifferencese.Diffs)
				{
					if (string.IsNullOrWhiteSpace(diff.Value.FileNameValue) ^ string.IsNullOrWhiteSpace(diff.Value.TagValue))
					{
						fileDifferencese.Mp3File[diff.Key] = (diff.Value.FileNameValue != "")
							? diff.Value.FileNameValue
							: diff.Value.TagValue;
						fileNameChanger.AddTagReplacement(diff.Key, fileDifferencese.Mp3File[diff.Key]);
					}
					else
					{
						var data = new CommunicationWithUser().GetInfoFromUser(diff.Key, diff.Value, fileDifferencese.Mp3File, _communication);
						switch (data.DataFrom)
						{
							case SyncActions.FromFileName:
								fileDifferencese.Mp3File[diff.Key] = data.Data;
								break;
							case SyncActions.FromTags:
								break;
							case SyncActions.Manual:
								fileDifferencese.Mp3File[diff.Key] = data.Data;
								break;
						}
						fileNameChanger.AddTagReplacement(diff.Key, data.Data);
					}
				}

				var fn = fileNameChanger.GetNewFileName();
				_data.Add(new KeyValuePair<string, KeyValuePair<string, Dictionary<TagType, string>>>(fn, fData));
				fileDifferencese.Mp3File.ChangeFileName(fn);
			}
			_filesChanged = filesChanged;
		}

		public void Dispose()
		{
			new RollbackManager(_saver).AddAction(new RollbackInfo(ProgramAction.FileRename, _filesChanged, _mask,
				_data)).Dispose();
		}

		public void Rollback(RollbackInfo info)
		{
			var data = info.Data as List<KeyValuePair<string, KeyValuePair<string, Dictionary<TagType, string>>>>;

			if(data == null) return;

			foreach (var item in data)
			{
				var mp3 = new Mp3File(item.Key);
				foreach (var tag in item.Value.Value)
				{
					mp3[tag.Key] = tag.Value;
				}

				mp3.ChangeFileName(item.Value.Key);
			}
		}
	}

	internal enum ChangeType
	{
		File, Tags
	}
}