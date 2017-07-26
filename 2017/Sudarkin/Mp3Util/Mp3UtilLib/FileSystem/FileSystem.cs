using System.Collections.Generic;
using System.IO;

namespace Mp3UtilLib.FileSystem
{
    public class FileSystem : IFileSystem
    {
        public bool Exists(string path) => File.Exists(path);

        public void Move(string source, string dest)
        {
            if (Exists(dest))
            {
                throw new IOException($"{dest} - File is already exists!");
            }

            File.Move(source, dest);
        }

        public IEnumerable<string> GetFiles(string directory, string searchPattern, SearchOption searchOption)
            => Directory.GetFiles(directory, searchPattern, searchOption);

        public IEnumerable<string> GetFilesFromCurrentDirectory(string searchPattern, bool recursive)
        {
            return GetFiles(
                Directory.GetCurrentDirectory(),
                searchPattern,
                recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
        }
    }
}