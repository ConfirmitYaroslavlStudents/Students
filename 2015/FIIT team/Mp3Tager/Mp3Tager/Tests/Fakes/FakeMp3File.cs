using System.Collections.Generic;
using FileLib;

namespace Tests
{
    public class FakeMp3File : IMp3File
    {
        private readonly BaseUniquePathCreator _checker;

        public Mp3Tags Tags { get; private set; }

        public FakeMp3File(Mp3Tags tags, string path, BaseUniquePathCreator checker)
        {            
            Tags = tags;
            FullName = path;
            _checker = checker;
        }
                
        public void Save()
        {
        }

        public string FullName { get; set; }

        public IMp3File CopyTo(string uniquePath)
        {
            return new FakeMp3File(Tags, uniquePath, _checker);            
        }

        public void MoveTo(string uniquePath)
        {
            FullName = _checker.CreateUniqueName(uniquePath);
        }

        public void Delete()
        {
        }
    }
}
