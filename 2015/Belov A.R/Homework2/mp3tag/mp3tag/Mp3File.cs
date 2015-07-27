using System;
using System.IO;
using Mp3TagLib;
using File = TagLib.File;

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
            get
            {
                var lastSlashIndex = _file.Name.LastIndexOf(@"\");
                return _file.Name.Substring(lastSlashIndex + 1, _file.Name.Length - lastSlashIndex - 1-".mp3".Length);
            } 
        }

        public void SetTags(Mp3Tags tags)
        {
            var album = tags.GetTag("Album");
            var yearString = tags.GetTag("Year");
            var comment = tags.GetTag("Comment");
            var title = tags.GetTag("Title");
            var artist = tags.GetTag("Artist");
            var genre = tags.GetTag("Genre");
            var trackString = tags.GetTag("Track");
            if(!string.IsNullOrEmpty(album))
            _file.Tag.Album = album;
            if (yearString!="0")
            {
                uint year ;
                if (!uint.TryParse(yearString, out year))
                { throw new ArgumentException("Bad year"); }
                _file.Tag.Year = year;
            }
            if (trackString != "0")
            {
                uint track;
                if (!uint.TryParse(yearString, out track))
                { throw new ArgumentException("Bad track"); }
                _file.Tag.Track = track;
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


        public Mp3Tags GetTags()
        {
            var tags=new Mp3Tags();
            tags.Album = _file.Tag.Album;
            tags.Artist = _file.Tag.FirstArtist;
            tags.Comment = _file.Tag.Comment;
            tags.Genre = _file.Tag.FirstGenre;
            tags.Title = _file.Tag.Title;
            tags.Year = _file.Tag.Year;
            tags.Track = _file.Tag.Track;
            return tags;

        }

        public void ChangeName(string newName)
        {
            string path = _file.Name.Substring(0, _file.Name.LastIndexOf(@"\"));
            path = path + @"\" + fixFileName(newName) + ".mp3";
            System.IO.File.Move(_file.Name,path);
            _file = File.Create(path);
        }

        string fixFileName(string name)
        {
            name = name.Replace("*", "");
            name = name.Replace("|", "");
            name = name.Replace(@"\", "");
            name = name.Replace(":", "");
            name = name.Replace("\"", "");
            name = name.Replace("<", "");
            name = name.Replace(">", "");
            name = name.Replace("?", "");
            name = name.Replace("/", "");
            return name;
        }
    }
}
