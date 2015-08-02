using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace mp3lib
{
	public class Mp3FileAnalyser
	{
		private string Mask { get; }
		private IEnumerable<IMp3File> Files { get; }

		public Mp3FileAnalyser(IEnumerable<IMp3File> files,string mask)
		{
			Mask = mask;
			Files = files;
		}

        //TODO: tests
        //TODO: return detailed information {READY}
		public IEnumerable<FileDifferences> FindDifferences()
		{
		    var list = new List<FileDifferences>();
		    foreach (var mp3 in Files)
		    {
		        var mp3Data = mp3.GetId3Data();
		        var extracter = new DataExtracter(Mask);
		        var tags = extracter.GetTags();
		        var prefixes = extracter.FindAllPrefixes(tags);
		        var diffs = new FileDifferences(mp3);
		        var data = extracter.GetFullDataFromString(prefixes, Path.GetFileNameWithoutExtension(mp3.FilePath), tags);

		        var differenceItems = data.Where(item => mp3Data[item.Key] != item.Value).ToArray();

                if (!differenceItems.Any()) continue;
		        
                foreach (var item in differenceItems)
		        {
		            diffs.Add(item.Key, new Diff {FileNameValue = item.Value, TagValue = mp3Data[item.Key]});
		        }

                list.Add(diffs);
            }

		    return list.ToArray();
		}
	}
}