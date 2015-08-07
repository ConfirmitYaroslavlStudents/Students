using System.IO;
using TagLib;

namespace Mp3Lib
{
    public class Mp3File : IMp3File
    {
        private readonly TagLib.File _file;

        public string Path { get; private set; }

        public IMp3Tags Mp3Tags { get; private set; }

        public Mp3File(TagLib.File file)
        {
            _file = file;
            Path = _file.Name;

            Mp3Tags = new Mp3Tags
            {
                Album = file.Tag.Album,
                Title = file.Tag.Title,
                Artist = file.Tag.Performers[0],
                Genre = file.Tag.Genres[0],
                Track = file.Tag.Track
            };
        }

        public void MoveTo(string path)
        {
            new FileInfo(Path).MoveTo(path);
            Path = path;
        }

        public void Save()
        {
            _file.Save();
        }
    }
}
