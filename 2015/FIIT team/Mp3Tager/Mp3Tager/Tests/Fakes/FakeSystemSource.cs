using System.Collections.Generic;
using System.Linq;
using FileLib;

namespace Tests.Fakes
{
    class FakeSystemSource : ISource
    {
        public string SourceFolder { get; private set; }

        private readonly List<IMp3File> _mp3Files;

        public FakeSystemSource(string sourceFilder, List<IMp3File> mp3Files)
        {
            SourceFolder = sourceFilder;
            _mp3Files = mp3Files;
        }

        public IEnumerable<string> GetFileNames()
        {
            return _mp3Files.Select(mp3File => mp3File.FullName).ToList();
        }

        public IEnumerable<IMp3File> GetFiles()
        {
            return _mp3Files;
        }
    }
}
