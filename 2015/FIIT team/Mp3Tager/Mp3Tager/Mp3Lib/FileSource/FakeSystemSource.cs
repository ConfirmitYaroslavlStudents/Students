using System.Collections.Generic;

namespace Mp3Lib.FileSource
{
    // todo: is it correct place?
    class FakeSystemSource : ISource
    {
        public string SourceFolder { get; private set; }
        public IEnumerable<string> GetFileNames()
        {
            List<string> filenames = new List<string>();
            return filenames;
        }

        public IEnumerable<IMp3File> GetFiles()
        {
            List<IMp3File> mp3Files = new List<IMp3File>();
            //add bunch of FakeMp3File objects

            return mp3Files;
        }
    }
}
