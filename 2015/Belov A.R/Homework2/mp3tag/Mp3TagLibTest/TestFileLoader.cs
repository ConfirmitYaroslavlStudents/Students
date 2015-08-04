using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mp3TagLib;

namespace Mp3TagTest
{
    class TestFileLoader:IFileLoader
    {
        public bool FileExist(string path)
        {
            return true;
        }

        public IMp3File Load(string path)
        {
            if (path.Contains(".UnknownExtension"))
                return null;
            return new TestMp3File() {Path = path, Tags = new Mp3Tags(){Album = "testAlbum",Artist="testArtist",Comment = "testComment",Genre = "testGenre",Title = "testTitle",Track = 1,Year = 2015}};
        }
    }
}
