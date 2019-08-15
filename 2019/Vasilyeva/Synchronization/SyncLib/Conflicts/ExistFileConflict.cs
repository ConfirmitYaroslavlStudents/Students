using System.IO;

namespace SyncLib
{
    internal class ExistFileConflict : IConflict
    {
        private string path;

        public ExistFileConflict(string path)
        {
            this.path = path;
        }

        public InfoLog Resolve()
        {
            File.Delete(path);

            return new InfoLog("delete", new FileInfo(path));
        }
    }
}