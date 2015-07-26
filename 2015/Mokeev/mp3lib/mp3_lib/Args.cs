namespace mp3lib
{
	public class Args
	{
		public string FilePath { get; private set; }
		public string Mask { get; private set; }

		public Args (string path, string mask)
		{
			FilePath = path;
			Mask = mask;
		}
	}
}