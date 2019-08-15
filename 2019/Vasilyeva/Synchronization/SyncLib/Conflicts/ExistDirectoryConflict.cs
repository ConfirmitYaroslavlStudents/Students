using System.IO;

namespace SyncLib
{
    internal class ExistDirectoryConflict : IConflict
    {
        private string path;

        public ExistDirectoryConflict(string path)
        {
            this.path = path;
        }

        public InfoLog Resolve()
        {
            Directory.Delete(path, true);

            return new InfoLog("delete", new DirectoryInfo(path));
        }
    }
}