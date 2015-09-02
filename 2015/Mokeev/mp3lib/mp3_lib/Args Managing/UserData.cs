using mp3lib.Core;

namespace mp3lib.Args_Managing
{
	public class UserData
	{
		public UserData(SyncActions dataFrom, string data)
		{
			DataFrom = dataFrom;
			Data = data;
		}

		public SyncActions DataFrom { get; private set; }
		public string Data { get; private set; }
	}
}