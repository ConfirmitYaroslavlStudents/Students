using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// ReSharper disable InconsistentNaming

namespace mp3_lib
{
	public class Mp3File
	{
		private byte[] _id3v1Data;
		private byte[] _id3v1ExtendedData;

		private string _filePath;

		private string _title;
		private string _artist;
		private string _album;
		private ushort _year;
		private string _comment;
		private ushort _trackId;
		private ushort _genre;

		public string Title { get { return _title; }  }
		public string Artist { get { return _artist; } }
		public string Album { get { return _album; }  }
		public ushort Year { get { return _year; } }
		public string Comment { get { return _comment; }  }
		private byte ZeroByte { get; set; }
		public ushort TrackId { get { return _trackId; } }
		public ushort Genre { get { return _genre; } }

		public Mp3File(string file)
		{
			if (!File.Exists(file)) throw new FileNotFoundException("File not found", file);
			_filePath = file;

			var fileData = new MemoryStream();

			using (var fs = new FileStream(file, FileMode.Open))
			{
				fs.CopyTo(fileData);
			}

			var fileDataArray = fileData.ToArray();


			var data = fileDataArray.Reverse().Take(128).Reverse().ToArray();

			var tagCode = data.Take(3).ToArray();

			if (Encoding.UTF8.GetString(tagCode) == "TAG")
			{
				_id3v1Data = data;
				var tmp = fileDataArray.Reverse().Take(355).Reverse().ToArray();
				var extendedData = tmp.Take(227).ToArray();

				var tmpData = data.Reverse().Take(125).Reverse().ToArray();
				_title = Encoding.UTF8.GetString(tmpData.Take(30).ToArray());

				tmpData = tmpData.Reverse().Take(95).Reverse().ToArray();
				_artist = Encoding.UTF8.GetString(tmpData.Take(30).ToArray());

				tmpData = tmpData.Reverse().Take(65).Reverse().ToArray();
				_album = Encoding.UTF8.GetString(tmpData.Take(30).ToArray());

				tmpData = tmpData.Reverse().Take(35).Reverse().ToArray();
				ushort.TryParse(Encoding.UTF8.GetString(tmpData.Take(4).ToArray()), out _year);


				if (Encoding.UTF8.GetString(extendedData.Take(4).ToArray()) == "TAG+")
				{
					
				}

			}
		}
	}
}
