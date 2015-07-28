namespace mp3lib
{
	public class Args
	{
		public ProgramAction Action { get; private set; }
		public string Path { get; private set; }
		public string Mask { get; private set; }

		public Args (string path, string mask, ProgramAction action)
		{
			Path = path;
			Mask = mask;
			Action = action;
		}
	}
}