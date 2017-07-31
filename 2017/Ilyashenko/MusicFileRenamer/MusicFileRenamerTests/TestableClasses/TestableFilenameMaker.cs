using System.IO;
using MusicFileRenamerLib;

namespace MusicFileRenamerTests
{
    public class TestableFilenameMaker : IFilenameMaker
    {
        public void MakeFilename(Mp3File file)
        {
            var newPath = Path.GetDirectoryName(file.path) + "\\" + file.Artist + " - " + file.Title + Path.GetExtension(file.path);
            file.path = newPath;
        }
    }
}
