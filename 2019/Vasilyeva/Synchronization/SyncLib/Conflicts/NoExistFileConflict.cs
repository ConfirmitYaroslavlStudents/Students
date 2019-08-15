using System.IO;

namespace SyncLib
{
    internal class NoExistFileConflict : IConflict
    {
        private string source;
        private string target;

        public NoExistFileConflict(string source, string target)
        {
            this.source = source;
            this.target = target;
        }

        public InfoLog Resolve()
        {
            File.Copy(source, target);

            return new InfoLog("copy", source, target);
        }
    }
}