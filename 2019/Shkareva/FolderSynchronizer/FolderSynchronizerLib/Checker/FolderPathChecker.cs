using System.IO;

namespace FolderSynchronizerLib
{
    public class FolderPathChecker : IChecker
    {
        public bool IsValid(string path)
        {
            return Directory.Exists(path);
        }
    }
}
