using System.IO;
using Mp3TagLib;

namespace mp3tager
{
    class FileLoader:IFileLoader
    {
        public bool FileExist(string path)
        {
            return File.Exists(path);
        }
        public IMp3File Load(string path)
        {
            if (FileExist(path))
                return new Mp3File(TagLib.File.Create(path));
            return null;
        }
    }
}
