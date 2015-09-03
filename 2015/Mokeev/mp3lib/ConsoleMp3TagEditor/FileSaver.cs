using System.IO;

using mp3lib.Rollback;

namespace ConsoleMp3TagEditor
{
	public class FileSaver : ISaver
	{
		private readonly string _path;

		public FileSaver(string file)
		{
			_path = file;
		}

		public void SendMessage(string str)
		{
			File.WriteAllText(_path, str);
		}

		public string GetResponse()
		{
			return File.ReadAllText(_path);
		}
	}
}