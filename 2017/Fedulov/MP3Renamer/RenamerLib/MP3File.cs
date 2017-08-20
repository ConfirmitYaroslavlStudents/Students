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
    public interface IMP3File
    {
        string Artist { set; get; }
        string Title { set; get; }
        string FilePath { set; get; }
        void Move(string path);
        void Save();
    }

    public class MP3File : IMP3File
    {
        public readonly TagLib.File taggedFile;
        public readonly IFileManager fileManager;

        public string Artist { set; get; }
        public string Title { set; get; }
        public string FilePath { set; get; }

        public MP3File(string path) : this(path, new FileManager())
        {
        }

        public MP3File(string path, IFileManager fileManager)
        {
            taggedFile = TagLib.File.Create(path);
            Artist = taggedFile.Tag.FirstPerformer;
            Title = taggedFile.Tag.Title;
            FilePath = taggedFile.Name;

            this.fileManager = fileManager;
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
