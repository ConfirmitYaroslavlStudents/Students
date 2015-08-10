using System;
using System.Collections.Generic;
using System.Linq;

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

			foreach (var fileDifferencese in diffs.ToArray())
			{
				foreach (var diff in fileDifferencese.Diffs)
				{
					if (diff.Value.FileNameValue == "" ^ diff.Value.TagValue == "")
					{
						fileDifferencese.Mp3File[diff.Key] = GetInfoFromUser(diff.Key, diff.Value);
					}
					else
					{
						fileDifferencese.Mp3File[diff.Key] = (diff.Value.FileNameValue != "")
							? diff.Value.FileNameValue
							: diff.Value.TagValue;
					}

				}
			}


		}


		private string GetInfoFromUser(TagType tag, Diff diff)
		{
			_communication.SendMessage(string.Format("There is a problem with tag \"{0}\". ", tag));
			_communication.SendMessage("You can enter tag from: \n\t1) File name, \n\t1) File, \n\t1) Manual");

			while (true)
			{
				_communication.SendMessage("Your choise (number): ");
				SyncActions inputData;
				var choiseCorrect = SyncActions.TryParse(_communication.GetResponse(), out inputData);
				if(!choiseCorrect) continue;

				switch (inputData)
				{
					case SyncActions.FromFileName:
						return diff.FileNameValue;
					case SyncActions.FromData:
						return diff.TagValue;
					case SyncActions.Manual:
						_communication.SendMessage("Enter text for tag \"" + tag + "\"");
						return _communication.GetResponse();
				}
			}
		}

	}

	internal enum SyncActions
	{
		FromFileName	= 1,
		FromData		= 2,
		Manual			= 3,
	}

}