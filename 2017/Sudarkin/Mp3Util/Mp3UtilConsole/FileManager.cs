using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;

namespace Mp3UtilConsole
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

        public IEnumerable<string> GetFiles(string searchPattern, bool recursive)
        {
            return GetFiles(
                _fileSystem.Directory.GetCurrentDirectory(), searchPattern, recursive);
        }

        public IEnumerable<string> GetFiles(string directory, string searchPattern, bool recursive)
        {
            return _fileSystem.Directory.GetFiles(
                directory, 
                searchPattern,
                recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
        }
    }
}