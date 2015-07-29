using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mp3lib
{
	public class Mp3FileAnalyser
	{
		private string Mask { get; set; }
		private IEnumerable<string> Files { get; set; }

		public Mp3FileAnalyser(IEnumerable<string> files,string mask)
		{
			Mask = mask;
			Files = files;
		}

		public IEnumerable<string> FindDifferences()
		{
			return (
						from file in Files
						let mp3 = new Mp3File(file)
						let mp3Data = mp3.GetId3Data()
						let extracter = new DataExtracter(Mask)
						let tags = extracter.GetTags()
						let prefixes = extracter.FindAllPrefixes(tags)
						let data = extracter.GetFullDataFromString(prefixes, new StringBuilder(mp3.FilePath), tags)
						where data.Any(key => mp3Data[key.Key] != key.Value)
						select file

					).ToArray();
		}
	}
}