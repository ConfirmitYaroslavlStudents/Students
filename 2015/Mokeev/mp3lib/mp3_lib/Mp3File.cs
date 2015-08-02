using System;
using System.Collections.Generic;
using System.IO;
using Id3;
using Id3.Frames;

namespace mp3lib
{
	public class Mp3File : IMp3File
    {
		private Mp3Stream Mp3FileStream { get; }
		public string FilePath { get; }
		private Id3Tag Tags { get; }

		public string Title { get { return Tags.Title; } set { Tags.Title.Value = value; Set(); } }
		public string Artist { get { return Tags.Artists; } set { Tags.Artists.TextValue = value; Set(); } }
		public string Album { get { return Tags.Album; } set { Tags.Album.Value = value; Set(); } }
		public string Year { get { return Tags.Year.Value.ToString(); } set { Tags.Year.Value = Convert.ToInt32(value); Set(); } }
		public string Comment { get { return Tags.Comments.ToString(); } set { Tags.Comments.Clear(); Tags.Comments.Add(new CommentFrame(){Comment = value}); Set(); } }
		public string TrackId { get { return Tags.Track.TrackCount.ToString(); } set { Tags.Track.Value = Convert.ToInt32(value); Set(); } }
		public string Genre { get { return Tags.Genre; }  set { Tags.Genre.Value = value; Set(); } }

		public Mp3File(string file)
		{
			if (!File.Exists(file)) throw new FileNotFoundException("Mp3File not found", file);
			FilePath = file;

			var fs = new FileStream(file, FileMode.Open, FileAccess.ReadWrite);

			Mp3FileStream = new Mp3Stream(fs, Mp3Permissions.ReadWrite);

			if (!Mp3FileStream.HasTags) return;
			var id3v1exists = Mp3FileStream.HasTagOfFamily(Id3TagFamily.Version1x);
			var id3v2exists = Mp3FileStream.HasTagOfFamily(Id3TagFamily.Version2x);

			if (!id3v2exists && id3v1exists) Tags = Mp3FileStream.GetTag(Id3TagFamily.Version1x);

			Tags = Mp3FileStream.GetTag(Id3TagFamily.Version2x);
		}

		private void Set()
		{
			Mp3FileStream.WriteTag(Tags, WriteConflictAction.Replace);
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
	}
}
