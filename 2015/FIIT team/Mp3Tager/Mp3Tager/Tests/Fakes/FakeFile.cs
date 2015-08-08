using System.IO;
using BackupLib;

namespace Tests.Fakes
{
    public class FakeFile : IFile
    {
        public string FullName { get; private set; }

        public FakeFile(string path)
        {
            FullName = path;
        }

        public IFile CopyTo(string path)
        {
            return new FakeFile(path);
        }

        public void MoveTo(string path)
        {
            FullName = path;
        }

        public void Delete()
        {
            
        }
    }
}
