using System.IO;

namespace SyncLib
{
    public class DirectoryChecker
    {
        private readonly string target;

        public DirectoryChecker(string target)
        {
            this.target = target;
        }
        public int GetTypeConflict(string shortCut)
        {
            if (Directory.Exists(target + shortCut)) return 1;

            return 2;
        }
    }
}
