using System.IO;

namespace MusicFileRenamerLib
{
    public class FileSystem : IFileSystem
    {
        public bool Exists(string path)
        {
            return File.Exists(path);
        }

        public void Move(string sourceFileName, string destFileName)
        {
            if (Exists(destFileName))
            {
                throw new IOException($"Файл {destFileName} уже существует!");
            }
            File.Move(sourceFileName, destFileName);
        }

        public void SetTags(Mp3File file)
        {
            var taggedFile = TagLib.File.Create(file.path);
            taggedFile.Tag.Performers = new string[] { file.Artist };
            taggedFile.Tag.Title = file.Title;
            taggedFile.Save();
        }
    }
}
