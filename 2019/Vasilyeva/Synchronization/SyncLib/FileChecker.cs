using System.IO;

namespace SyncLib
{
    public class FileChecker
    {
        private readonly string source;
        private readonly string target;

        public FileChecker(string master, string slave)
        {
            source = master;
            target = slave;
        }
        public int GetTypeConflict(string shortCut)
        {
            if (!File.Exists(target + shortCut))
                return 1;

            if (new FileInfo(source + shortCut).Length != new FileInfo(target + shortCut).Length)
                return 2;

            return 3;
        }
    }
}
