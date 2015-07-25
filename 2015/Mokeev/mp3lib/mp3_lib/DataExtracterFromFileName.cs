using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace mp3lib
{
	public class DataExtracterFromFileName
	{
		private readonly string[] _allowedStrings = {
			"{title}",
			"{artist}",
			"{id}",
			"{album}",
			"{genre}",
			"{year}",
			"{comment}",
		};

		public string Mask { get; private set; }

		public DataExtracterFromFileName(string mask)
		{
			Mask = mask;
		}

		public Queue<Match> GetTags()
		{
			var tagsCollection = Regex.Matches(Mask, "({[a-z]*})");

			var tags = new Queue<Match>();
			foreach (Match tag in tagsCollection)
			{
				tags.Enqueue(tag);
			}
			return tags;
		}

		public Queue<string> FindAllPrefixes(Queue<Match> tagsCollection)
		{
			var mask = new StringBuilder(Mask);

			var prefixesQueue = new Queue<string>();
			foreach (var tag in tagsCollection)
			{
				if (!_allowedStrings.Contains(tag.Value)) throw new ArgumentException("Wrong type sended: " + tag.Value);
				if (mask.Length == 0) continue;

				var index = mask.ToString().IndexOf(tag.Value, StringComparison.CurrentCulture);
				var str = mask.ToString().Substring(0, index);
				prefixesQueue.Enqueue(str);
				mask.Remove(0,
					(tag.Value.Length + str.Length > mask.Length) ? mask.Length : tag.Value.Length + str.Length);
			}
			return prefixesQueue;
		}

		public Dictionary<string, string> GetFullDataFromString(Queue<string> prefixesQueue, StringBuilder mp3Name, Queue<Match> tags)
		{
			if (prefixesQueue.Count < tags.Count) throw new Exception("Too low prefixes count. Undefined state found.");

			var data = new Dictionary<string, string>();
			while (prefixesQueue.Count > 0)
			{
				var prefix = prefixesQueue.Dequeue();
				if (prefixesQueue.Count > 0)
				{
					var postfix = prefixesQueue.Peek();
					mp3Name.Remove(0, prefix.Length);
					var resultStr = new StringBuilder();

					var idx = 0;

					while (postfix[0] != mp3Name[idx])
					{
						resultStr.Append(mp3Name[idx]);
						idx++;
					}

					var tmpIdx = idx;
					var postfixIdx = 0;
					var tmpStr = new StringBuilder();

					while (postfix[postfixIdx] != mp3Name[tmpIdx])
					{
						tmpStr.Append(postfix[postfixIdx]);
						postfixIdx++;
						tmpIdx++;
					}


					mp3Name.Remove(0, resultStr.Length);
					data.Add(tags.Dequeue().Value, resultStr.ToString());
				}
				else
				{
					mp3Name.Remove(0, prefix.Length);
					if (tags.Count > 0) data.Add(tags.Dequeue().Value, mp3Name.ToString());
					break;
				}
			}

			return data;
		}
	}
}