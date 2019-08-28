using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SyncLib
{
    public abstract class BaseSeeker
    {
        internal string masterPath;
        internal string slavePath;
        internal Stack<string> storage = new Stack<string>();
        public BaseSeeker(string masterPath, string slavePath)
        {
            this.masterPath = masterPath;
            this.slavePath = slavePath;
        }

        public List<IConflict> GetMasterConflict()
        {
            FileChecker fileChecker = new FileChecker(masterPath, slavePath);
            DirectoryChecker directoryChecker = new DirectoryChecker(slavePath);

            List<IConflict> conflicts = new List<IConflict>();

            storage.Push(masterPath);

            while (storage.Count != 0)
            {
                string current = storage.Pop();
                if (directoryChecker.GetTypeConflict(current.Replace(masterPath, "")) == DirectoryConflictType.NoExistConflict)
                    conflicts.Add(new NoExistDirectoryConflict(slavePath + current.Replace(masterPath, "")));

                var files = Directory.GetFiles(current).Select(x => x.Replace(masterPath, ""));

                foreach (var file in files)
                {
                    switch (fileChecker.GetTypeConflict(file))
                    {
                        case FileConflictType.NoExistConflict:
                            conflicts.Add(new NoExistFileConflict(masterPath + file, slavePath + file));
                            break;
                        case FileConflictType.DifferentContent:
                            conflicts.Add(new DifferentContentConflict(masterPath + file, slavePath + file));
                            break;
                    }
                }

                AddNewDirectories(current);
            }

            return conflicts;
        }

        internal void AddNewDirectories(string current)
        {
            var directories = Directory.GetDirectories(current).ToList();

            foreach (var dir in directories)
                storage.Push(dir);
        }

        public abstract List<IConflict> GetSlaveConflicts();
    }
}