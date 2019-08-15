using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncLib
{
    public class DefaultConflictSeeker : BaseSeeker
    {
        public DefaultConflictSeeker(string masterPath, string slavePath) : base(masterPath, slavePath)
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
                    conflicts.Add(new NoExistDirectoryConflict(masterPath + current.Replace(slavePath, "")));

                var files = Directory.GetFiles(current).Select(x => x.Replace(slavePath, ""));

                foreach (var file in files)
                {
                    if (fileChecker.GetTypeConflict(file) == 1)
                        conflicts.Add(new NoExistFileConflict(slavePath + file, masterPath + file));
                }

                var directories = Directory.GetDirectories(current).ToList();

                foreach (var directory in directories)
                    storage.Push(directory);
            }

            return conflicts;
        }
    }
}
