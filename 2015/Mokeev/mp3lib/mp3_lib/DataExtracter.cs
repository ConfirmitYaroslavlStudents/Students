using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using mp3lib.Exceptions;

namespace mp3lib
{
	public class DataExtracter
	{
		public string Mask { get; }

		public DataExtracter(string mask)
		{
			Mask = mask;
		}

		public Queue<TagType> GetTags()
		{
			var tagsCollection = Regex.Matches(Mask, "({[a-z]*})");

			var tags = new Queue<TagType>();
			foreach (Match tag in tagsCollection)
			{
				TagType tagType;
				switch (tag.Value)
				{
					case "{title}":
						tagType = TagType.Title;
						break;
					case "{id}":
						tagType = TagType.Id;
						break;
					case "{artist}":
						tagType = TagType.Artist;
						break;
					case "{album}":
						tagType = TagType.Album;
						break;
					case "{genre}":
						tagType = TagType.Genre;
						break;
					case "{comment}":
						tagType = TagType.Comment;
						break;
					case "{year}":
						tagType = TagType.Year;
						break;
					default:
						continue;
				}

				tags.Enqueue(tagType);
			}
			return tags;
		}

		public Queue<string> FindAllPrefixes(Queue<TagType> tagsCollection)
		{
			var mask = new StringBuilder(Mask);

			var prefixesQueue = new Queue<string>();
			foreach (var tag in tagsCollection)
			{
				if (mask.Length == 0) continue;

				var index = mask.ToString().IndexOf("{" + tag.ToString().ToLower() + "}", StringComparison.CurrentCulture);
				var str = mask.ToString().Substring(0, index);
				prefixesQueue.Enqueue(str);
				mask.Remove(0,
					((tag.ToString().Length + 2) + str.Length > mask.Length) ? mask.Length : (tag.ToString().Length + 2) + str.Length);
			}
			return prefixesQueue;
		}

		public Dictionary<TagType, string> GetFullDataFromString(Queue<string> prefixesQueue, string mp3NameString, Queue<TagType> tags)
		{
			var mp3Name = new StringBuilder(mp3NameString);

			var prefixes = prefixesQueue.ToArray();
			for (var i = 1; i < prefixes.Length; i++)
			{
				var prefix = prefixes[i];
				if (prefix == "") throw new DataExctracterException("Too low prefixes count. Undefined state found.");
			}


			Dictionary<TagType, string> data;
			try
			{
				data = FindAllTAgs(prefixesQueue, tags, mp3Name);
			}
			catch (NotEnoughDataException exception)
			{
				data = exception.Data;
			}


			return data;
		}

		private static Dictionary<TagType, string> FindAllTAgs(Queue<string> prefixesQueue, Queue<TagType> tags, StringBuilder mp3Name)
		{
			var data = new Dictionary<TagType, string>();
			mp3Name.Remove(0, prefixesQueue.Peek().Length);
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

						try
						{
							while (postfix[0] != mp3Name[idx])
							{
								resultStr.Append(mp3Name[idx]);
								currentStr.Append(mp3Name[idx]);
								idx++;
							}
						}
						catch (IndexOutOfRangeException)
						{
							throw new NotEnoughDataException("Not enough data", tags, data);
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
							data.Add(tags.Dequeue(), resultStr.ToString());
							needContinue = false;
						}
						else
						{
							mp3Name.Remove(0, resultStr.Length);
							resultStr.Append(tmpStr);
							needContinue = true;
						}
					} while (needContinue);
				}
				else
				{
					if (tags.Count > 0) data.Add(tags.Dequeue(), mp3Name.ToString());
					break;
				}
			}
			return data;
		}
	}
}