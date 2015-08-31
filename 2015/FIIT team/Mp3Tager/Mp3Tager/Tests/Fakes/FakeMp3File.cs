using System.IO;
using FileLib;

namespace Tests
{
    public class FakeMp3File : IMp3File
    {


        public Mp3Tags Tags { get; private set; }

        public FakeMp3File(Mp3Tags tags, string path)
        {            
            Tags = tags;
            FullName = path;
        }
                
        public void Save()
        {
        }

        public string FullName { get; set; }

        public IMp3File CopyTo(string path)
        {
            return new FakeMp3File(Tags, path);            
        }

        public void MoveTo(string path)
        {
           // FullName = _checker.CreateUniqueName(path);
        }

        public void Delete()
        {
        }
    }
}
