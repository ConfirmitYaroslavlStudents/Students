using System.Collections.Generic;
using System.Linq;

namespace mp3lib.Rollback
{
	public sealed class RollbackInfo
	{
		public ProgramAction Action { get; private set; }
		public List<string> FilesChanged { get; private set; }
		public string Mask { get; private set; }
		public dynamic Data { get; private set; }

		public RollbackInfo(ProgramAction action, IEnumerable<string> files, string mask, dynamic data = null)
		{
			Action = action;
			FilesChanged = files.ToList();
			Mask = mask;
			Data = data ?? new List<dynamic>();
		}
	}
}