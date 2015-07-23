using System;
using Mp3TagLib;
using TagLib;

namespace mp3tager
{
    class Mp3File:IMp3File
    {
        private  File _file;
        public Mp3File(File file)
        {
            _file = file;
        }
        public string Name
        {
            get { return _file.Name; }
        }

        public void SetTags(Mp3Tags tags)
        {
            var album = tags.GetTag("Album");
            var yearString = tags.GetTag("Year");
            var comment = tags.GetTag("Comment");
            var title = tags.GetTag("Title");
            var artist = tags.GetTag("Artist");
            var genre = tags.GetTag("Genre");

            if(!string.IsNullOrEmpty(album))
            _file.Tag.Album = album;
            if (yearString!="0")
            {
                uint year ;
                if (!uint.TryParse(yearString, out year))
                { throw new ArgumentException("Bad year"); }
                _file.Tag.Year = year;
            }
            if (!string.IsNullOrEmpty(comment))
                _file.Tag.Comment = comment;
            if (!string.IsNullOrEmpty(album))
                _file.Tag.Title =title;
            if (!string.IsNullOrEmpty(artist))
                _file.Tag.AlbumArtists = new[] { artist};
            if (!string.IsNullOrEmpty(genre))
                _file.Tag.Genres = new[] { genre };
        }

        public void Save()
        {
            _file.Save();
        }
    }
}
