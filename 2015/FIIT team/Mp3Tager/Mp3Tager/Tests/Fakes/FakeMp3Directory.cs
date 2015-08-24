using FileLib;
using System.Collections.Generic;

namespace Tests.Fakes
{
    class FakeMp3Directory : BaseDirectory
    {
        private readonly HashSet<string> _paths = new HashSet<string>
        {
            @"D:\music\artist.mp3"
        };

        protected override bool Exists(string path)
        {
            return _paths.Contains(path);
        }

        public override IEnumerable<IMp3File> GetFiles(string directory)
        {
            return new List<IMp3File>();
        }
    }
}
