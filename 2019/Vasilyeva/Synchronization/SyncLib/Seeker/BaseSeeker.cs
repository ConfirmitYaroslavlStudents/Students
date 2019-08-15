using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SyncLib
{
    public abstract class BaseSeeker
    {
        internal string masterPath;
        internal string slavePath;
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

            Stack<string> directoryStack = new Stack<string>();

            directoryStack.Push(masterPath);

            while (directoryStack.Count != 0)
            {
                string current = directoryStack.Pop();
                if (directoryChecker.GetTypeConflict(current.Replace(masterPath, "")) == 2)
                    conflicts.Add(new NoExistDirectoryConflict(slavePath + current.Replace(masterPath, "")));

                var files = Directory.GetFiles(current).Select(x => x.Replace(masterPath, ""));

                foreach (var file in files)
                {
                    switch (fileChecker.GetTypeConflict(file))
                    {
                        case 1:
                            conflicts.Add(new NoExistFileConflict(masterPath + file, slavePath + file));
                            break;
                        case 2:
                            conflicts.Add(new DifferentContentConflict(masterPath + file, slavePath + file));
                            break;
                    }
                }

                var directories = Directory.GetDirectories(current).ToList();

                foreach (var dir in directories)
                    directoryStack.Push(dir);
            }

            return conflicts;
        }

        public abstract List<IConflict> GetSlaveConflicts();
    }
}