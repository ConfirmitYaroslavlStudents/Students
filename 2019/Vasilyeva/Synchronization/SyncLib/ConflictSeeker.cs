using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncLib
{
    public class ConflictSeeker
    {
        private void FindConflicts(string firstPath, string secondPath, bool noDelete)
        {
            Stack<string> waitingDirectories = new Stack<string>();

            waitingDirectories.Push(firstPath);

            while (waitingDirectories.Count != 0)
            {
                string current = waitingDirectories.Pop();
                if (!Directory.Exists(current)) continue;

                var files = Directory.GetFiles(current).Select(x => GetShortName(x, firstPath)).ToList();

                AddMissingFiles(files, secondPath, firstPath, noDelete);

                var directories = Directory.GetDirectories(current).ToList();

                foreach (var dir in directories)
                    waitingDirectories.Push(dir);

                directories = directories.Select(x => GetShortName(x, firstPath)).ToList();

                AddMissingFolders(directories, secondPath, firstPath, noDelete);
            }
        }
    }
}
