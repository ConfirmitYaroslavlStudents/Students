using System;
using System.Collections.Generic;
using System.Linq;

namespace mp3lib.Rollback
{
	[Serializable]
	public sealed class RollbackInfo
	{
		public ProgramAction Action { get; set; }
		public List<string> FilesChanged { get; set; }
		public string Mask { get; set; }
		public object Data { get; set; }

		public RollbackInfo(ProgramAction action, IEnumerable<string> files, string mask, dynamic data = null)
		{
			Action = action;
			FilesChanged = files?.ToList() ?? new List<string>();
			Mask = mask;
			Data = data ?? new List<dynamic>();
		}
	}
}