using System.IO;

namespace SyncLib
{
    public class DirectoryChecker
    {
        private readonly string mainPath;

        public DirectoryChecker(string path)
        {
            mainPath = path;
        }
        public int GetTypeConflict(string relativePath)
        {
            if (Directory.Exists(mainPath + relativePath)) return 1;

            return 2;
        }
    }
}
