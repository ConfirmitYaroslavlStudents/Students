using System.IO;
using Sync.Wrappers;
using FileAttributes = Sync.Wrappers.FileAttributes;

namespace Sync.Providers
{
    public class LocalDiskProvider : IProvider
    {
        public DirectoryWrapper LoadDirectory(string pathToDirectory)
        {
            return DFS(new DirectoryWrapper(pathToDirectory));
        }

        private DirectoryWrapper DFS(DirectoryWrapper currentDirectory)
        {
            foreach (var dirPath in Directory.EnumerateDirectories(currentDirectory.FullName))
            {
                var dir = currentDirectory.CreateDirectory(Path.GetFileName(dirPath));
                DFS(dir);
            }

            foreach (var filePath in Directory.EnumerateFiles(currentDirectory.FullName))
            {
                var info = new FileInfo(filePath);

                var attributes = new FileAttributes(info.Length, info.LastWriteTime);
                currentDirectory.CreateFile(Path.GetFileName(filePath), attributes);
            }

            return currentDirectory;
        }
    }
}