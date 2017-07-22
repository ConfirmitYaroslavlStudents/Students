using System.IO;
using System.IO.Abstractions;
using TagLib;
using TagFile = TagLib.File;

namespace Mp3UtilLib
{
    public class Mp3File
    {
        private readonly TagFile _file;
        private readonly FileManager _fileManager;

        public string Artist { get; set; }
        public string Title { get; set; }

        public string FullName { get; set; }

        public Mp3File(string path) 
            : this(path, null, new FileSystem())
        {
            
        }

        public Mp3File(string path, IFileSystem fileSystem) 
            : this(path, null, fileSystem)
        {

        }

        public Mp3File(string path, Stream stream, IFileSystem fileSystem)
        {
            _file = CreateTagFileFromStream(path, stream);

            if (_file.MimeType != "taglib/mp3")
            {
                throw new FileLoadException($"{path} - This file is not mp3!");
            }

            _fileManager = new FileManager(fileSystem);

            FullName = _file.Name;

            Artist = _file.Tag.FirstPerformer;
            Title = _file.Tag.Title;
        }

        private TagFile CreateTagFileFromStream(string path, Stream stream = null)
        {
            try
            {
                return (stream == null) 
                    ? TagFile.Create(path) 
                    : TagFile.Create(new FileAbstraction(path, stream));
            }
            catch (UnsupportedFormatException)
            {
                throw new FileLoadException($"{path} - This file is not mp3!");
            }
        }

        public void MoveTo(string path)
        {
            if (_fileManager.Exists(path))
            {
                throw new IOException($"{path} - File is already exists!");
            }

            _fileManager.Move(FullName, path);

            FullName = path;
        }

        public void Save()
        {
            _file.Tag.Performers = new[] {Artist};
            _file.Tag.Title = Title;
            _file.Save();
        }
    }
}