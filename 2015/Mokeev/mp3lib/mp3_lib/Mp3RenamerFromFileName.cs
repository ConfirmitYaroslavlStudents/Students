using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace mp3_lib
{
	public class Mp3RenamerFromFileName
	{
		private StringBuilder Mp3RealName { get; set; }
		private Mp3File Mp3 { get; set; }

		public readonly string[] AllowedStrings = new[]
		{
			"{title}",
			"{artist}",
			"{id}",
			"{album}",
			"{genre}",
			"{year}",
			"{comment}",
		};

		public Mp3RenamerFromFileName(Mp3File mp3File)
		{
			Mp3 = mp3File;

			var mp3Info = new FileInfo(mp3File.FilePath);
			var ext = mp3Info.Extension;
			Mp3RealName = new StringBuilder(mp3Info.Name).Replace(ext, string.Empty);
		}

		public void Start(string maskInput)
		{
			var mask = new StringBuilder(maskInput);

			var delQueue = new Queue<string>();

			var i = 0;
			var regexp = new Regex(@"^\{[a-z]\}");
			while ()
			{
				mask.Replace(allowedString, "_" + i);
				i++;
			}


		}
	}
}