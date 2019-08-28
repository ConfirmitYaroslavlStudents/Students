using SyncLib.Conflicts;
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
        public FileConflictType GetTypeConflict(string shortCut)
        {
            if (!File.Exists(target + shortCut))
                return FileConflictType.NoExistConflict;

            if (new FileInfo(source + shortCut).Length != new FileInfo(target + shortCut).Length)
                return FileConflictType.DifferentContent;

            return FileConflictType.ExistConflict;
        }
    }
}
