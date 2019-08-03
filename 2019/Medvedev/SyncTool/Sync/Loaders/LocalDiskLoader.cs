using System.IO;
using Sync.Wrappers;

namespace Sync.Loaders
{
    public class LocalDiskLoader : ILoader
    {
        private string PathToDirectory { get; }

        public LocalDiskLoader(string path)
        {
            PathToDirectory = path;
        }

        public DirectoryWrapper LoadDirectory()
        {
            return DFS(new DirectoryWrapper(PathToDirectory));
        }

        private DirectoryWrapper DFS(DirectoryWrapper currentDirectory)
        {
            foreach (var dirPath in Directory.EnumerateDirectories(currentDirectory.FullName))
            {
                var dir = currentDirectory.CreateDirectory(dirPath);
                DFS(dir);
            }

            foreach (var filePath in Directory.EnumerateFiles(currentDirectory.FullName))
            {
                var info = new FileInfo(filePath);

                var attributes = new Sync.Wrappers.FileAttributes(info.Length, info.LastWriteTime);
                currentDirectory.CreateFile(Path.GetFileName(filePath), attributes);
            }

            return currentDirectory;
        }
    }
}