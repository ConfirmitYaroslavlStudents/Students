using System.Collections.Generic;

namespace mp3lib.Core
{
	public class FileDifferences
	{
		public IMp3File Mp3File { get; private set; }
		public Dictionary<TagType, Diff> Diffs { get; set; }

		public FileDifferences(IMp3File mp3File)
		{
			Mp3File = mp3File;
			Diffs = new Dictionary<TagType, Diff>();
		}

		public void Add(TagType type, Diff differeceTags)
		{
			Diffs.Add(type, differeceTags);
		}
	}
}