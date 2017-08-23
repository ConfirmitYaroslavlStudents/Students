using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RenamerLib
{
    public class FileSystemManager : IFileManager
    {
        public bool Exist(string path) => File.Exists(path);

        public void Move(string source, string dest) => File.Move(source, dest);

        public IEnumerable<IMP3File> GetFiles(string searchPattern, SearchOption searchOption)
        {
            IEnumerable<string> fileNames = Directory.GetFiles(Directory.GetCurrentDirectory(),
                searchPattern, searchOption);
            return fileNames.Select(file => new MP3File(file)).ToArray();
        }
    }
}
