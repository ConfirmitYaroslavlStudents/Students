using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SyncLib
{
    public class Synchronization
    {
        public Synchronization(string masterPath, string slavePath, bool noDelete = true)
        {
            masterDirectory = masterPath;
            slaveDirectory = slavePath;
            NoDeleteOption = noDelete;
        }

        private string masterDirectory;

        private string slaveDirectory;

        private bool NoDeleteOption { get; }

        public void Synchronaze()
        {
            Merge(masterDirectory, slaveDirectory, true);
            Merge(slaveDirectory, masterDirectory, NoDeleteOption);
        }

        private void Merge(string firstPath, string secondPath, bool noDelete)
        {
            Stack<string> directoryStack = new Stack<string>();

            directoryStack.Push(firstPath);

            while (directoryStack.Count != 0)
            {
                string current = directoryStack.Pop();
                if (!Directory.Exists(current)) continue;

                var files = Directory.GetFiles(current).Select(x => GetShortName(x, firstPath)).ToList();

                AddMissingFiles(files, secondPath, firstPath, noDelete);

                var directories = Directory.GetDirectories(current).ToList();

                foreach (var dir in directories)
                    directoryStack.Push(dir);

                directories = directories.Select(x => GetShortName(x, firstPath)).ToList();

                AddMissingFolders(directories, secondPath, firstPath, noDelete);
            }
        }

        private string GetShortName(string fullName, string mainPath)
        {
            return fullName.Replace(mainPath, "");
        }

        private void AddMissingFiles(List<string> value, string destenation, string source, bool delete)
        {
            foreach (var path in value)
            {
                string directiry = (destenation + path).Substring(0, (destenation + path).LastIndexOf('\\'));

                if (!Directory.Exists(directiry) && delete)
                {
                    Directory.CreateDirectory(directiry);
                }

                if (!File.Exists(destenation + path))
                {
                    if (!delete)
                    {
                        File.Delete(source + path);
                        continue;
                    }

                    File.Copy(source + path, destenation + path);
                }
                else UpdateFile(path);
            }
        }

        private void AddMissingFolders(List<string> value, string destenation, string source, bool delete)
        {
            foreach (var path in value)
            {
                string dir = (destenation + path);

                if (!Directory.Exists(dir))
                {
                    if (!delete)
                    {
                        (new FolderWrapper(source + path)).Delete();
                        continue;
                    }
                    Directory.CreateDirectory(dir);
                }
            }
        }

        private void UpdateFile(string path)
        {
            var masterFileWrapper = new FileWrapper(masterDirectory + path);
            var slaveFileWrapper = new FileWrapper(slaveDirectory + path);

            if (!masterFileWrapper.Equals(slaveFileWrapper))
            {
                FileWrapper.Replace(masterFileWrapper, slaveFileWrapper);
            }

        }

    }
}
