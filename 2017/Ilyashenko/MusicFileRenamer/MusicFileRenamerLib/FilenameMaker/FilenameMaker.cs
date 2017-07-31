using System.IO;

namespace MusicFileRenamerLib
{
    public class FilenameMaker : IFilenameMaker
    {
        public void MakeFilename(Mp3File file)
        {
            var newPath = Path.GetDirectoryName(file.path) + "\\" + file.Artist + " - " + file.Title + Path.GetExtension(file.path);
            if (!File.Exists(newPath))
            {
                File.Move(file.path, newPath);
                file.path = newPath;
            }
        }
    }
}
