using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace mp3lib
{
	public class DataExtracter
	{
		public readonly string[] AllowedStrings = {
			"{title}",
			"{artist}",
			"{id}",
			"{album}",
			"{genre}",
			"{year}",
			"{comment}",
		};

		public string Mask { get; private set; }

		public DataExtracter(string mask)
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
				if (!AllowedStrings.Contains(tag.Value)) throw new ArgumentException("Wrong type sended: " + tag.Value);
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
			var prefixes = prefixesQueue.ToArray();
			for (var i = 1; i < prefixes.Length; i++)
			{
				var prefix = prefixes[i];
				if (prefix == "") throw new Exception("Too low prefixes count. Undefined state found.");
			}

			var data = new Dictionary<string, string>();
			while (prefixesQueue.Count > 0)
			{
				prefixesQueue.Dequeue();
				if (prefixesQueue.Count > 0)
				{
					var postfix = prefixesQueue.Peek();
					var resultStr = new StringBuilder();

					bool needContinue;

					do
					{
						var idx = 0;
						var currentStr = new StringBuilder();
						while (postfix[0] != mp3Name[idx])
						{
							resultStr.Append(mp3Name[idx]);
							currentStr.Append(mp3Name[idx]);
							idx++;
						}

						var tmpIdx = idx;
						var postfixIdx = 0;
						var tmpStr = new StringBuilder();

						while (tmpIdx < mp3Name.Length && postfixIdx < postfix.Length && postfix[postfixIdx] == mp3Name[tmpIdx])
						{
							tmpStr.Append(postfix[postfixIdx]);
							postfixIdx++;
							tmpIdx++;
						}

						mp3Name.Remove(0, tmpStr.Length);
						if (tmpStr.ToString() == postfix)
						{
							mp3Name.Remove(0, currentStr.Length);
							data.Add(tags.Dequeue().Value, resultStr.ToString());
							needContinue = false;
						}
						else
						{
							mp3Name.Remove(0, resultStr.Length);
							resultStr.Append(tmpStr);
							needContinue = true;
						}
					} 
					while (needContinue);
				}
				else
				{
					if (tags.Count > 0) data.Add(tags.Dequeue().Value, mp3Name.ToString());
					break;
				}
			}

			return data;
		}
	}
}