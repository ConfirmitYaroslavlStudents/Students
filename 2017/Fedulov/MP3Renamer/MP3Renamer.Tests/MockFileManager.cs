using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RenamerLib;

namespace MP3Renamer.Tests
{
    class MockFileManager : IFileManager
    {
        public Dictionary<string, IMP3File> Files;

        public MockFileManager()
        {
            Files = new Dictionary<string, IMP3File>();
        }

        public MockFileManager(Dictionary<string, IMP3File> files)
        {
            Files = files;
        }

        public void AddFile(string name, IMP3File file) => Files.Add(name, file);

        public bool Exist(string path) => Files.ContainsKey(path);

        public void Move(string source, string dest)
        {
            Files.Add(dest, Files[source]);
            Files.Remove(source);
        }

        public IEnumerable<IMP3File> GetFiles(string searchPattern, 
            SearchOption searchOption) => Files.Values.ToArray();
    }
}
