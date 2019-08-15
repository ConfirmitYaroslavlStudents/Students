using System.IO;

namespace SyncLib
{
    internal class NoExistDirectoryConflict : IConflict
    {
        private string path;

        public NoExistDirectoryConflict(string path)
        {
            this.path = path;
        }

        public InfoLog Resolve()
        {
            Directory.CreateDirectory(path);

            return new InfoLog("copy", new DirectoryInfo(path));
        }
    }
}