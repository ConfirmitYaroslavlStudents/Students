using Mp3Lib;

namespace Mp3LibTests
{
    internal class FakeMp3File : IMp3File
    {
        public FakeMp3File(string path, string directoryName, TagBase tag)
        {
            DirectoryName = directoryName;
            Path = path;
            Tag = tag;
        }

        public void MoveTo(string newPath)
        {
            Path = newPath;
        }

        public void Save()
        {

        }

        public string Path { get; private set; }
        public string DirectoryName { get; private set; }
        public TagBase Tag { get; private set; }


    }
}
