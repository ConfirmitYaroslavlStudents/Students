using System;
using System.Collections.Generic;
using mp3lib.Core;
using TagLib;

namespace mp3lib.Rollback
{
	[Serializable]
	public sealed class Info
	{
		public IEnumerable<KeyValuePair<TagType, string>> Tags { get; } 
		public string NewFileName { get; }
		public string OldFileName { get; }

		public Info(IEnumerable<KeyValuePair<TagType, string>> tags, string newFileName, string oldFileName)
		{
			Tags = tags;
			NewFileName = newFileName;
			OldFileName = oldFileName;
		}
	}
}