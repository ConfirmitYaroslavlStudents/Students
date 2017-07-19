using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;

namespace Mp3UtilLib
{
    public class FileManager
    {
        private readonly IFileSystem _fileSystem;

        public FileManager() : this(new FileSystem())
        {
            
        }

        public FileManager(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public bool Exists(string path) => _fileSystem.File.Exists(path);

        public void Move(string source, string dest) => _fileSystem.File.Move(source, dest);

        public IEnumerable<string> GetFiles(string directory, string searchPattern, SearchOption searchOption)
            => _fileSystem.Directory.GetFiles(directory, searchPattern, searchOption);

        public IEnumerable<string> GetFilesFromCurrentDirectory(string searchPattern, bool recursive)
        {
            return GetFiles(
                _fileSystem.Directory.GetCurrentDirectory(), 
                searchPattern, 
                recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
        }
    }
}