using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncLib
{
    class FolderWrapper
    {
        public FolderWrapper(string path)
        {
            Path = path;
        }

        private string Path;

        public void Delete()
        {
            Stack<string> dirStack = new Stack<string>();

            dirStack.Push(Path);

            while (dirStack.Count != 0)
            {
                string current = dirStack.Peek();

                string[] dirs = Directory.GetDirectories(current);

                foreach (var dir in dirs)
                {
                    dirStack.Push(dir);
                }

                var files = Directory.GetFiles(current);

                foreach (var file in files)
                    File.Delete(file);

                if (dirs.Length == 0)
                {
                    dirStack.Pop();
                    Directory.Delete(current);
                }
            }
        }
    }
}
