using System.Collections.Generic;
using System.Linq;

namespace mp3lib
{
	public class Mp3Syncing
	{
		private IMp3File[] _files;
		private ICommunication _communication;

		public Mp3Syncing(IEnumerable<IMp3File> files, ICommunication communication)
		{
			_files = files.ToArray();
			_communication = communication;
		}

		public void SyncFiles()
		{
			
		}


	}
}