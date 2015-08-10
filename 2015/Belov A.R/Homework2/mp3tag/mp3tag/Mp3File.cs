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
                //[TODO] use Path
                return Path.GetFileNameWithoutExtension(_file.Name);
            } 
        }

        public bool NameChanged { get;private set; }

        public bool TagChanged { get;private set; }

        public string NewName { get; private set; }

        public Mp3Tags OldTags { get; private set; }

        public void SetTags(Mp3Tags tags)
        {
            OldTags = GetTags();
            //[TODO] extract string constants
            if (!string.IsNullOrEmpty(tags.Album))
                _file.Tag.Album = tags.Album;
            if (tags.Year != 0)
            {
                _file.Tag.Year = tags.Year;
            }
            if (tags.Track != 0)
            {
                _file.Tag.Track = tags.Track;
            }
            if (!string.IsNullOrEmpty(tags.Comment))
                _file.Tag.Comment = tags.Comment;
            if (!string.IsNullOrEmpty(tags.Title))
                _file.Tag.Title = tags.Title;
            if (!string.IsNullOrEmpty(tags.Artist))
                _file.Tag.Artists = new[] { tags.Artist };
            if (!string.IsNullOrEmpty(tags.Genre))
                _file.Tag.Genres = new[] { tags.Genre };
            TagChanged = true;
        }

        public void Save()
        {
            _file.Save();
            if (!string.IsNullOrEmpty(NewName))
            {
                var path = _file.Name.Substring(0, _file.Name.LastIndexOf(@"\"));
                path = path + @"\" + NewName + ".mp3";
                System.IO.File.Move(_file.Name, path);
                _file = File.Create(path);
            }
        }

        public Mp3Tags GetTags()
        {
            var tags = new Mp3Tags
            {
                Album = _file.Tag.Album,
                Artist = _file.Tag.FirstArtist,
                Comment = _file.Tag.Comment,
                Genre = _file.Tag.FirstGenre,
                Title = _file.Tag.Title,
                Year = _file.Tag.Year,
                Track = _file.Tag.Track
            };
            return tags;

        }

        public void ChangeName(string newName)
        {
            NewName = FixFileName(newName);
            if(string.IsNullOrEmpty(NewName))
                throw new ArgumentException("Bad name");
            NameChanged = true;
        }

        string FixFileName(string name)
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
