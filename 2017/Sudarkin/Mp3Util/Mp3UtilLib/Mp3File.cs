using System.IO;
using Mp3UtilLib.FileSystem;
using TagLib;
using TagFile = TagLib.File;

namespace Mp3UtilLib
{
    public class Mp3File : AudioFile
    {
        private readonly TagFile _file;

        public Mp3File(string path) : base(path)
        {
            
        }

        public Mp3File(string path, IFileSystem fileSystem) : base(path, fileSystem)
        {
            try
            {
                _file = TagFile.Create(path);
            }
            catch (UnsupportedFormatException)
            {
                throw new FileLoadException($"{path} - This file is not mp3!");
            }

            if (_file.MimeType != "taglib/mp3")
            {
                throw new FileLoadException($"{path} - This file is not mp3!");
            }

            Artist = _file.Tag.FirstPerformer;
            Title = _file.Tag.Title;
        }

        public override void Save()
        {
            _file.Tag.Performers = new[] {Artist};
            _file.Tag.Title = Title;
            _file.Save();
        }
    }
}