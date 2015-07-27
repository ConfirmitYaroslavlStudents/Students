using System.IO;

namespace Mp3Lib
{
    public class TagBase
    {
        public TagBase(string[] performers, string[] genres, string title, string album, uint track)
        {
            Performers = performers;
            Genres = genres;
            Title = title;
            Album = album;
            Track = track;
        }
        public string[] Performers { get; set; }
        public string[] Genres { get; set; }
        public string Title { get; set; }
        public string Album { get; set; }
        public uint Track { get; set; }
    }

    public class Mp3File : IMp3File
    {
        public Mp3File(string path)
        {
            var file = new FileInfo(path);

            if (!file.Exists)
                throw new FileNotFoundException("Mp3 file not found", path);

            DirectoryName = file.DirectoryName;

            _content = TagLib.File.Create(path);
            Path = path;
            Tag = new TagBase(_content.Tag.Performers, _content.Tag.Genres, _content.Tag.Title, _content.Tag.Album, _content.Tag.Track);
        }

        public void MoveTo(string newPath)
        {
            var temp = new FileInfo(Path);
            temp.MoveTo(newPath);
            Path = newPath;
        }

        public void Save()
        {
            _content.Save();
        }

        public string Path { get; private set; }
        public string DirectoryName { get; private set; }
        public TagBase Tag { get; private set; }

        private readonly TagLib.File _content;
    }
}
