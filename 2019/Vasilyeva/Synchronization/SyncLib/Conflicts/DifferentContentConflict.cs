using System.IO;

namespace SyncLib
{
    internal class DifferentContentConflict : IConflict
    {
        private string source;
        private string target;

        public DifferentContentConflict(string source, string target)
        {
            this.source = source;
            this.target = target;
        }

        public InfoLog Resolve()
        {
            File.Copy(source, target, true);

            return new InfoLog("update", target, source);
        }
    }
}