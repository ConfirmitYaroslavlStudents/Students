using System;
using System.Collections.Generic;
using System.IO;

namespace mp3lib
{
	public class Mp3File : IMp3File
	{
		private TagLib.File File { get; set; }
		public string FilePath { get; private set; }

		public string Title { get { return File.Tag.Title; } set { File.Tag.Title = value; Set(); } }
		public string Artist { get { return File.Tag.FirstPerformer; } set { File.Tag.Performers = new [] {value}; Set(); } }
		public string Album { get { return File.Tag.Album; } set { File.Tag.Album = value; Set(); } }
		public string Year { get { return File.Tag.Year.ToString(); } set { File.Tag.Year = Convert.ToUInt32(value); Set(); } }

		public string this[TagType type]
		{
			get { return GetId3Data()[type]; }
			set
			{
				switch (type)
				{
					case TagType.Artist:
						Artist = value;
						break;
					case TagType.Id:
						TrackId = value;
						break;
					case TagType.Title:
						Title = value;
						break;
					case TagType.Album:
						Album = value;
						break;
					case TagType.Genre:
						Genre = value;
						break;
					case TagType.Year:
                         Year = value;
						break;
					case TagType.Comment:
						Comment = value;
						break;
					default:
						throw new ArgumentOutOfRangeException(type.ToString(), type, null);
				}
			}
		}

		public string Comment { get { return File.Tag.Comment; } set { File.Tag.Comment = value; Set(); } }
		public string TrackId { get { return File.Tag.Track.ToString(); } set { File.Tag.Track = Convert.ToUInt32(value); Set(); } }
		public string Genre { get { return File.Tag.FirstGenre; } set { File.Tag.Genres = new[] { value }; Set(); } }

		public Mp3File(string file)
		{
			if (!System.IO.File.Exists(file)) throw new FileNotFoundException("Mp3File not found", file);
			FilePath = file;

			File = TagLib.File.Create(file);
		}

		private void Set()
		{
			File.Save();
		}

		public Dictionary<TagType, string> GetId3Data()
		{
			return new Dictionary<TagType, string>
			{
				{TagType.Id, TrackId},
				{TagType.Title, Title},
				{TagType.Artist, Artist},
				{TagType.Album, Album},
				{TagType.Year, Year},
				{TagType.Comment, Comment},
				{TagType.Genre, Genre}
			};

		}

		public void ChangeFileName(string fileName)
		{
			if (fileName == FilePath) return;

			if (System.IO.File.Exists(Path.GetDirectoryName(FilePath) + fileName))
			{
				throw new IOException("File already exists");
			}

			File.Dispose();
			File = null;

			var path = Path.GetDirectoryName(FilePath) + "\\" + fileName + ".mp3";
            System.IO.File.Move(FilePath, path);
			FilePath = fileName;

			File = TagLib.File.Create(path);
		}
	}
}
