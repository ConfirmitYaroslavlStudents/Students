using System;
using System.IO;
using File = TagLib.File;

namespace FileLib
{
    public class Mp3File : IMp3File
    {
        private File _content;

        public Mp3Tags Tags { get; private set; }

        public string FullName { get; set; }

        public Mp3File(string path)
        {
            _content = File.Create(path);
            FullName = path;

            Tags = new Mp3Tags
            {
                Album = _content.Tag.Album,
                Title = _content.Tag.Title,
                Artist = _content.Tag.FirstPerformer,
                Genre = _content.Tag.FirstGenre,
                Track = _content.Tag.Track
            };
        }

        public void Save()
        {
            SaveTags();
            _content.Save();

            if (_content.Name == FullName)
                return;
            CheckForFileExists();
            MoveTo(FullName);
        }

        private void CheckForFileExists()
        {
            var index = 1;
            var indexStr = string.Empty;
            while (System.IO.File.Exists(FullName + indexStr))
            {
                indexStr = string.Format("({0})", index++);
            }
            FullName = Path.Combine(Path.GetDirectoryName(FullName),
                Path.GetFileNameWithoutExtension(FullName) + indexStr + ".mp3");
        }

        private void SaveTags()
        {
            if (Tags.Artist != null)
            {
                _content.Tag.Performers = null;
                _content.Tag.Performers = new[] { Tags.Artist };
            }
            if (Tags.Genre != null)
            {
                _content.Tag.Genres = null;
                _content.Tag.Genres = new[] { Tags.Genre };
            }
            _content.Tag.Title = Tags.Title;
            _content.Tag.Album = Tags.Album;
            _content.Tag.Track = Tags.Track;
        }

        public IMp3File CopyTo(string path)
        {
            System.IO.File.Copy(_content.Name, path, true);

            return new Mp3File(path);
        }

        public void Delete()
        {
            System.IO.File.Delete(_content.Name);
        }

        public void MoveTo(string path)
        {
            System.IO.File.Move(_content.Name, path);
            _content = File.Create(path);
        }       
    }
}