using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Id3;
using Id3.Frames;

// ReSharper disable InconsistentNaming

namespace mp3_lib
{
	public class Mp3File
	{
		private Mp3Stream mp3File { get; set; }
		public string FilePath { get; private set; }
		private Id3Tag _tags { get; set; }

		public string Title
		{
			get
			{
				return _tags.Title;
			}
			set { _tags.Title.Value = value; Set(); }
		}

		public string Artist { get { return _tags.Artists; } set { _tags.Artists.TextValue = value; Set(); } }
		public string Album { get { return _tags.Album; } set { _tags.Album.Value = value; Set(); } }
		public int? Year { get { return _tags.Year.Value; } set { _tags.Year.Value = value; Set(); } }
		public string Comment { get { return _tags.Comments.ToString(); } set { _tags.Comments.Clear(); _tags.Comments.Add(new CommentFrame(){Comment = value}); Set(); } }
		public ushort TrackId { get { return Convert.ToUInt16(_tags.Track.TrackCount); } set { _tags.Track.Value = value; Set(); } }
		public ushort Genre { get { return Convert.ToUInt16(_tags.Genre); }  set { _tags.Genre.Value = value.ToString(); Set(); } }

		public Mp3File(string file)
		{
			if (!File.Exists(file)) throw new FileNotFoundException("File not found", file);
			FilePath = file;

			var fs = new FileStream(file, FileMode.Open, FileAccess.ReadWrite);

			mp3File = new Mp3Stream(fs, Mp3Permissions.ReadWrite);

			if (!mp3File.HasTags) return;
			var id3v1exists = mp3File.HasTagOfFamily(Id3TagFamily.Version1x);
			var id3v2exists = mp3File.HasTagOfFamily(Id3TagFamily.Version2x);

			if (!id3v2exists && id3v1exists) _tags = mp3File.GetTag(Id3TagFamily.Version1x);

			_tags = mp3File.GetTag(Id3TagFamily.Version2x);
		}

		private void Set()
		{
			mp3File.WriteTag(_tags, WriteConflictAction.Replace);
		}
	}
}
