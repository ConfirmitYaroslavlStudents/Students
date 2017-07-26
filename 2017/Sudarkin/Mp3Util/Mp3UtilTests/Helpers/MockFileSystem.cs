using System.Collections.Generic;
using System.IO;
using Mp3UtilLib.FileSystem;

namespace Mp3UtilTests.Helpers
{
    public class MockFileSystem : IFileSystem
    {
        private readonly List<string> _files;

        public string CurrentDirectory { get; set; }
        public string[] AllFiles => _files.ToArray();

        public MockFileSystem() : this(new List<string>())
        {
            
        }

        public MockFileSystem(IEnumerable<string> files)
        {
            _files = new List<string>();

            AddRange(files);
        }

        public void AddRange(IEnumerable<string> collection)
        {
            foreach (string item in collection)
            {
                if (!Exists(item))
                {
                    _files.Add(item);
                }
            }
        }

        public bool Exists(string path)
        {
            return _files.IndexOf(path) != -1;
        }

        public void Move(string source, string dest)
        {
            if (Exists(dest))
            {
                throw new IOException("File is already exists!");
            }

            _files.Remove(source);
            _files.Add(dest);
        }

        public IEnumerable<string> GetFiles(string directory, string searchPattern, SearchOption searchOption)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<string> GetFilesFromCurrentDirectory(string searchPattern, bool recursive)
        {
            return GetFiles(
                CurrentDirectory, 
                searchPattern,
                recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
        }
    }
}