using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace mp3lib
{
	public class Mp3Syncing
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
						var data = GetInfoFromUser(diff.Key, diff.Value, fileDifferencese.Mp3File);
						switch (data.DataFrom)
						{
							case SyncActions.FromFileName:
								fileDifferencese.Mp3File[diff.Key] = data.Data;
								break;
							case SyncActions.FromTags:
								fileNameChanger.AddTagReplacement(diff.Key, data.Data);
								break;
							case SyncActions.Manual:
								fileDifferencese.Mp3File[diff.Key] = data.Data;
								fileNameChanger.AddTagReplacement(diff.Key, data.Data);
								break;
						}
					}
				}
				var fn = fileNameChanger.GetNewFileName();
				fileDifferencese.Mp3File.ChangeFileName(fn);
			}


		}


		private class UserData
		{
			public UserData(SyncActions dataFrom, string data)
			{
				DataFrom = dataFrom;
				Data = data;
			}

			public SyncActions DataFrom { get; private set; }
			public string Data { get; private set; }
		}

        //[TODO] move to separate class
		private UserData GetInfoFromUser(TagType tag, Diff diff, IMp3File file)
		{
			_communication.SendMessage(string.Format("File: {0}", file.FilePath));
			_communication.SendMessage(string.Format("There is a problem with tag \"{0}\". ", tag));
			_communication.SendMessage(string.Format("You can enter tag from: \n\t1) File name (Data: \"{0}\"), \n\t2) Mp3 Tags (Data: \"{1}\"), \n\t3) Manual", diff.FileNameValue, diff.TagValue));

			while (true)
			{
				_communication.SendMessage("Your choise (number): ");
				SyncActions inputData;
				var choiseCorrect = SyncActions.TryParse(_communication.GetResponse(), out inputData);
				if (!choiseCorrect)
				{
					_communication.SendMessage("Wrong input!");
					_communication.SendMessage("You sholud enter number with action!");
					continue;
				}

				switch (inputData)
				{
					case SyncActions.FromFileName:
						return new UserData(inputData, diff.FileNameValue);
					case SyncActions.FromTags:
						return new UserData(inputData, diff.TagValue);
					case SyncActions.Manual:
						_communication.SendMessage("Enter text for tag \"" + tag + "\"");
						return new UserData(inputData, _communication.GetResponse());
				}
			}
		}

		private enum SyncActions
		{
			FromFileName = 1,
			FromTags = 2,
			Manual = 3,
		}
	}

}