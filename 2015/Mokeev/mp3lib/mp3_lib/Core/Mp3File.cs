using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using mp3lib.Rollback;
using TagLib;

namespace mp3lib.Core
{
	public class Mp3File : IMp3File
	{
		private TagLib.File File { get; set; }
		private Tag Tag { get; }

		private bool _fileChanged = false;
		private string _oldFilePath;
		private ISaver _saver;

		public string FilePath { get; private set; }

		public string Title { get { return Tag.Title; } set { File.Tag.Title = value; Set(); } }
		public string Artist { get { return Tag.FirstPerformer; } set { File.Tag.Performers = new [] {value}; Set(); } }
		public string Album { get { return Tag.Album; } set { File.Tag.Album = value; Set(); } }
		public string Year { get { return Tag.Year.ToString(); } set { File.Tag.Year = Convert.ToUInt32(value); Set(); } }

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

		public string Comment { get { return Tag.Comment; } set { File.Tag.Comment = value; Set(); } }
		public string TrackId { get { return Tag.Track.ToString(); } set { File.Tag.Track = Convert.ToUInt32(value); Set(); } }
		public string Genre { get { return Tag.FirstGenre; } set { File.Tag.Genres = new[] { value }; Set(); } }

		public Mp3File(string file, ISaver saver)
		{
			if (!System.IO.File.Exists(file)) throw new FileNotFoundException("Mp3File not found", file);
			FilePath = file;

			_oldFilePath = file.Clone() as string;
			_saver = saver;

			File = TagLib.File.Create(file);
			Tag = File.GetTag(TagTypes.Id3v2);
		}

		private void Set()
		{
			File.Save();
			_fileChanged = true;
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

		public void Rollback(RollbackInfo rollbackInfo)
		{
			foreach (var tag in rollbackInfo.Tags)
			{
				this[tag.Key] = tag.Value;
			}

			ChangeFileName(rollbackInfo.OldFileName);
		}

		public void Dispose()
		{
			if(!_fileChanged) return;

			var data = new RollbackInfo(GetId3Data().ToList(), FilePath, _oldFilePath);
			new RollbackManager(_saver).AddAction(data).Dispose();
		}
	}
}
