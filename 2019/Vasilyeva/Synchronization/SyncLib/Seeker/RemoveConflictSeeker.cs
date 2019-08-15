using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SyncLib
{
    class RemoveConflictSeeker : BaseSeeker
    {
        public RemoveConflictSeeker(string masterPath, string slavePath) : base(masterPath, slavePath)
        { }

        public override List<IConflict> GetSlaveConflicts()
        {
            FileChecker fileChecker = new FileChecker(slavePath, masterPath);
            DirectoryChecker directoryChecker = new DirectoryChecker(masterPath);

            List<IConflict> conflicts = new List<IConflict>();

            Stack<string> storage = new Stack<string>();

            storage.Push(slavePath);

            while (storage.Count != 0)
            {
                string current = storage.Pop();

                if (directoryChecker.GetTypeConflict(current.Replace(slavePath, "")) == 2)
                {
                    conflicts.Add(new ExistDirectoryConflict(current));
                    continue;
                }

                var files = Directory.GetFiles(current).Select(x => x.Replace(slavePath, ""));

                foreach (var file in files)
                {
                    if (fileChecker.GetTypeConflict(file) == 1)
                        conflicts.Add(new ExistFileConflict(slavePath + file));
                }

                var directories = Directory.GetDirectories(current).ToList();

                foreach (var dir in directories)
                    storage.Push(dir);
            }

            return conflicts;
        }
    }
}
