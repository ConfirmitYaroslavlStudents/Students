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

		public Mp3Syncing(IEnumerable<IMp3File> files, string mask, ICommunication communication)
		{
			_files = files.ToArray();
			_communication = communication;
			_mask = mask;
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

			foreach (var fileDifferencese in diffs)
			{
				var fileNameChanger = new Mp3FileNameChanger(fileDifferencese.Mp3File, _mask, true);
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
				fileDifferencese.Mp3File.ChangeFileName(fn);
			}


		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		public void Rollback(RollbackInfo info)
		{
			throw new NotImplementedException();
		}
	}
}