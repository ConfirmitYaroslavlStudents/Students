using System.IO;

namespace RenamerLib
{
    public class MP3File : IMP3File
    {
        public readonly TagLib.File TaggedFile;
        public readonly IFileManager FileManager;

        public string Artist { set; get; }
        public string Title { set; get; }
        public string FilePath { set; get; }

        public MP3File(string path) : this(path, new FileSystemManager())
        {
        }

        public MP3File(string path, IFileManager fileManager)
        {
            TaggedFile = TagLib.File.Create(path);
            Artist = TaggedFile.Tag.FirstPerformer;
            Title = TaggedFile.Tag.Title;
            FilePath = TaggedFile.Name;

            FileManager = fileManager;
        }

        public void Move(string path)
        {
            if (FileManager.Exist(path))
                throw new IOException(path + " - already exist!");

            FileManager.Move(FilePath, path);
            FilePath = path;
        }

        public void Save()
        {
            TaggedFile.Tag.Performers = new[] {Artist};
            TaggedFile.Tag.Title = Title;
            TaggedFile.Save();
        }
    }
}
