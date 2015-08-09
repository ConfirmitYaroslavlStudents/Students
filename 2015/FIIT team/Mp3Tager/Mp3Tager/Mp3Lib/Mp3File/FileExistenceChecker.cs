using System.IO;

namespace Mp3Lib
{
    public class FileExistenceChecker : IFileExistenceChecker
    {
        public bool CheckIfExists(string path)
        {
            return File.Exists(path);
        }
    }
}
