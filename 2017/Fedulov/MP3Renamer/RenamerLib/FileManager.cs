using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Abstractions;

namespace RenamerLib
{
    public class FileManager
    {
        private readonly IFileSystem fileSystem;

        public FileManager() : this(new FileSystem())
        {
        }

        public FileManager(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        public bool Exist(string path) => fileSystem.File.Exists(path);

        public void Move(string source, string dest) => fileSystem.File.Move(source, dest);

        public string[] GetFiles(string path, string searchPattern, SearchOption searchOption)
            => fileSystem.Directory.GetFiles(path ?? fileSystem.Directory.GetCurrentDirectory(),
                searchPattern, searchOption);
    }
}
