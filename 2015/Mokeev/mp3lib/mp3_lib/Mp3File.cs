using System;
using System.Collections.Generic;
using System.IO;
using Id3;
using Id3.Frames;

namespace mp3lib
{
	public class Mp3File : IMp3File
    {
		private Mp3Stream mp3File { get; set; }
		public string FilePath { get; private set; }
		private Id3Tag Tags { get; set; }

		public string Title { get { return Tags.Title; } set { Tags.Title.Value = value; Set(); } }
		public string Artist { get { return Tags.Artists; } set { Tags.Artists.TextValue = value; Set(); } }
		public string Album { get { return Tags.Album; } set { Tags.Album.Value = value; Set(); } }
		public string Year { get { return Tags.Year.Value.ToString(); } set { Tags.Year.Value = Convert.ToInt32(value); Set(); } }
		public string Comment { get { return Tags.Comments.ToString(); } set { Tags.Comments.Clear(); Tags.Comments.Add(new CommentFrame(){Comment = value}); Set(); } }
		public ushort TrackId { get { return Convert.ToUInt16(Tags.Track.TrackCount); } set { Tags.Track.Value = value; Set(); } }
		public ushort Genre { get { return Convert.ToUInt16(Tags.Genre); }  set { Tags.Genre.Value = value.ToString(); Set(); } }

		public Mp3File(string file)
		{
			if (!File.Exists(file)) throw new FileNotFoundException("File not found", file);
			FilePath = file;

			var fs = new FileStream(file, FileMode.Open, FileAccess.ReadWrite);

			mp3File = new Mp3Stream(fs, Mp3Permissions.ReadWrite);

			if (!mp3File.HasTags) return;
			var id3v1exists = mp3File.HasTagOfFamily(Id3TagFamily.Version1x);
			var id3v2exists = mp3File.HasTagOfFamily(Id3TagFamily.Version2x);

			if (!id3v2exists && id3v1exists) Tags = mp3File.GetTag(Id3TagFamily.Version1x);

			Tags = mp3File.GetTag(Id3TagFamily.Version2x);
		}

		private void Set()
		{
			mp3File.WriteTag(Tags, WriteConflictAction.Replace);
		}

		public Dictionary<TagType, string> GetId3Data()
		{
			return new Dictionary<TagType, string>
			{
				{TagType.Id, TrackId.ToString()},
				{TagType.Title, Title},
				{TagType.Artist, Artist},
				{TagType.Album, Album},
				{TagType.Year, Year.ToString()},
				{TagType.Comment, Comment},
				{TagType.Genre, Genre.ToString()}
			};

		}
	}
}
