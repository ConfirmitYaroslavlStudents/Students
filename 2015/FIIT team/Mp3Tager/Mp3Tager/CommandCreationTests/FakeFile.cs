using System.Collections.Generic;
using Mp3Lib;

namespace CommandCreationTests
{
    internal class FakeFile : IFile
    {
        private readonly HashSet<string> _paths = new HashSet<string>
        {
            @"D:\TestFile.mp3", @"D:\TestFile (1).mp3"
        };

        public bool Exists(string path)
        {
            return _paths.Contains(path);
        }
    }
}
