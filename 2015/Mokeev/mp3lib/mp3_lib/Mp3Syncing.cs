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
					//if (diff.Value.FileNameValue != "" && diff.Value.TagValue != "") 
                }
			}


		}




	}

	public enum SyncActions
	{
		FromFileName,
		FromData
	}

}