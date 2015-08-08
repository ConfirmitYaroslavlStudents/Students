using Mp3Lib;
using System.Collections.Generic;

namespace Tests.Fakes
{
    class FakeFileExistenceChecker : IFileExistenceChecker
    {
        private readonly HashSet<string> _paths = new HashSet<string>
        {
            @"D:\TestFile.mp3", @"D:\TestFile (1).mp3"
        };
        public bool CheckIfExists(string path)
        {
            return _paths.Contains(path);
        }
    }
}


        


