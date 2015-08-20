using FileLib;
using System.Collections.Generic;

namespace Tests.Fakes
{
    class FakeUniquePathCreator : BaseUniquePathCreator
    {
        private readonly HashSet<string> _paths = new HashSet<string>
        {
            @"D:\music\artist.mp3"
        };

        protected override bool Exists(string path)
        {
            return _paths.Contains(path);
        }
    }
}
