using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagLib;

namespace RenamerLib
{
    public class MP3File
    {
        public readonly TagLib.File taggedFile;
        public readonly FileManager fileManager;

        public string Artist { set; get; }
        public string Title { set; get; }
        public string FilePath { set; get; }

        public MP3File(string path) : this(path, null, new FileSystem())
        {
        }

        public MP3File(string path, IFileSystem fileSystem) : this(path, null, fileSystem)
        {
        }

        public MP3File(string path, Stream stream, IFileSystem fileSystem)
        {
            taggedFile = TagLib.File.Create(path); //(stream == null) ? TagLib.File.Create(path) : TagLib.File.Create(path, stream);

            fileManager = new FileManager(fileSystem);

            Artist = taggedFile.Tag.FirstPerformer;
            Title = taggedFile.Tag.Title;
            FilePath = taggedFile.Name;
        }

        public void Move(string path)
        {
            if (fileManager.Exist(path))
                throw new IOException(path + " - already exist!");

            fileManager.Move(FilePath, path);
            FilePath = path;
        }

        public void Save()
        {
            taggedFile.Tag.Performers = new[] {Artist};
            taggedFile.Tag.Title = Title;
            taggedFile.Save();
        }
    }
}
