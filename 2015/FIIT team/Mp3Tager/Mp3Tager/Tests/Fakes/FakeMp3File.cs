using System.Collections.Generic;
using FileLib;

namespace Tests
{
    public class FakeMp3File : IMp3File
    {
        private readonly BaseFileExistenceChecker _checker;

        public Mp3Tags Tags { get; private set; }

        public FakeMp3File(Mp3Tags tags, string path, BaseFileExistenceChecker checker)
        {            
            Tags = tags;
            FullName = path;
            _checker = checker;
        }
                
        public void Save()
        {
        }

        public string FullName { get; set; }

        public IMp3File CopyTo(string path)
        {
            return new FakeMp3File(Tags, path, _checker);            
        }

        public void MoveTo(string path)
        {
            FullName = _checker.CreateUniqueName(path);
        }

        public void Delete()
        {
        }
    }
}
