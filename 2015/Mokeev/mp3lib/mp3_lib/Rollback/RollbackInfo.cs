using System;
using System.Collections.Generic;
using System.Linq;
using mp3lib.Args_Managing;

namespace mp3lib.Rollback
{
	[Serializable]
	public sealed class RollbackInfo
	{
		public IEnumerable<Info> Data { get; set; }

		public RollbackInfo(IEnumerable<Info> data = null)
		{
			Data = data ?? new List<Info>();
		}
	}
}