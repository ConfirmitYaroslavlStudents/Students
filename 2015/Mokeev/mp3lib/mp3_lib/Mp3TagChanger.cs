using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace mp3_lib
{
	public class Mp3TagChanger
	{
		private string Mp3RealName { get; set; }
		private Mp3File Mp3 { get; set; }

		private readonly string[] _allowedStrings = new[]
		{
			"{title}",
			"{artist}",
			"{id}",
			"{album}",
			"{genre}",
			"{year}",
			"{comment}",
		};

		public Mp3TagChanger(Mp3File mp3File)
		{
			Mp3 = mp3File;

			var mp3Info = new FileInfo(mp3File.FilePath);
			var ext = mp3Info.Extension;
			Mp3RealName = new StringBuilder(mp3Info.Name).Replace(ext, string.Empty).ToString();
		}

		public void Start(string maskInput)
		{
			var tagsCollection = Regex.Matches(maskInput, "({[a-z]+})+");
			var mask = new StringBuilder(maskInput);
			
			var prefixesQueue = new Queue<string>();
			foreach (Match tag in tagsCollection)
			{
				if (!_allowedStrings.Contains(tag.Value)) throw new ArgumentException("Wrong type sended: " + tag.Value);
				if (mask.Length == 0) continue;

				var index = mask.ToString().IndexOf(tag.Value, StringComparison.CurrentCulture);
				var str = mask.ToString().Substring(0, index);
				prefixesQueue.Enqueue(str);
				mask.Remove(0,
					(tag.Value.Length + str.Length > mask.Length) ? mask.Length : tag.Value.Length + str.Length);
			}

			var tags = new Queue<Match>();
			foreach (Match tag in tagsCollection)
			{
				tags.Enqueue(tag);
			}


			var data = new Dictionary<string, string>();
			var mp3Name = new StringBuilder(Mp3RealName);

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
					if(tags.Count > 0) data.Add(tags.Dequeue().Value, mp3Name.ToString());
					break;
				}
			}


			foreach (var item in data)
			{
				switch (item.Key)
				{
					case "{title}":
						Mp3.Title = item.Value;
						break;
					case "{artist}":
						Mp3.Artist = item.Value;
						break;
					case "{id}":
						Mp3.TrackId = Convert.ToUInt16(item.Value);
						break;
					case "{album}":
						Mp3.Album = item.Value;
						break;
					case "{genre}":
						Mp3.Genre = Convert.ToUInt16(item.Value);
						break;
					case "{year}":
						Mp3.Year = Convert.ToUInt16(item.Value);
						break;
					case "{comment}":
						Mp3.Comment = item.Value;
						break;
				}
			}
		}
	}
}