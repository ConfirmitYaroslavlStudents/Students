using MusicFileRenamerLib;

namespace MusicFileRenamerTests
{
    public class TestableFileSystem : IFileSystem
    {
        public bool Exists(string path)
        {
            return false;
        }

        public void Move(string sourceFileName, string destFileName)
        {
            
        }

        public void SetTags(Mp3File file)
        {
            
        }
    }
}
