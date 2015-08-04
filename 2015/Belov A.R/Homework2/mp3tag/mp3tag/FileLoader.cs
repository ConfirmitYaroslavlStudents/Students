using System;
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
            //[TODO] use Path
            if (ValidateFile(path))
                return TryCreate(path);
            return null;
        }

        Mp3File TryCreate(string path)
        {
            try
            {
                return new Mp3File(TagLib.File.Create(path));
            }
            catch (Exception)
            {

                return null;
            }
            
        }
        public bool ValidateFile(string path)
        {
            return FileExist(path) && Path.GetExtension(path).ToLower() == ".mp3";
        }
    }
}
