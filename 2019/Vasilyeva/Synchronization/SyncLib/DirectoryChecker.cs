using SyncLib.Conflicts;
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
        public DirectoryConflictType GetTypeConflict(string relativePath)
        {
            if (Directory.Exists(mainPath + relativePath)) return DirectoryConflictType.ExistConflict;

            return DirectoryConflictType.NoExistConflict;
        }
    }
}
