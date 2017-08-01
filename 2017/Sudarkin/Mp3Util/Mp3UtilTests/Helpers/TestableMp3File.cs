using Mp3UtilLib;
using Mp3UtilLib.FileSystem;

namespace Mp3UtilTests.Helpers
{
    public class TestableMp3File : AudioFile
    {
        public bool Saved { get; private set; }

        public TestableMp3File(string filename) 
            :base(filename, new MockFileSystem())
        {
            
        }

        public TestableMp3File(string filename, IFileSystem fileSystem)
            :base(filename, fileSystem)
        {

        }

        public override void Save()
        {
            Saved = true;
        }
    }
}