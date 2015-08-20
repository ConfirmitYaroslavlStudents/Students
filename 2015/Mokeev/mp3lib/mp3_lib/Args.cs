using System;

namespace mp3lib
{
	public class Args
	{
		private readonly bool _onlyAction;
		private readonly string _path;
		private readonly string _mask;
		public ProgramAction Action { get; private set; }

		public string Path
		{
			get
			{
				if(!_onlyAction) return _path;
				throw new Exception("Can't access not set value");
			}
		}

		public string Mask
		{
			get
			{
				if (!_onlyAction) return _mask;
				throw new Exception("Can't access not set value");
			}
		}

		public Args(string path, string mask, ProgramAction action)
		{
			_path = path;
			_mask = mask;
			Action = action;
			_onlyAction = false;
		}

		public Args(ProgramAction action)
		{
			Action = action;
			_onlyAction = true;
		}

	}
}