using Mp3Lib;

namespace Tests
{
    public class FakeMp3File : IMp3File
    {
        public string Path { get; private set; }
        public IMp3Tags Mp3Tags { get; private set; }


        public FakeMp3File(string path, IMp3Tags tags)
        {
            Path = path;
            Mp3Tags = tags;
        }

        public void MoveTo(string path)
        {
            Path = path;
        }

        public void Save()
        {

        }
    }
}
