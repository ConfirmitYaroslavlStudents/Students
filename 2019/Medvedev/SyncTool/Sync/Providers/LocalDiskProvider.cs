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

        public void Delete(IFileSystemElementWrapper element)
        {
            if (element is FileWrapper)
                File.Delete(element.FullName);
            if (element is DirectoryWrapper)
                Directory.Delete(element.FullName, true);
        }

        public void CopyTo(IFileSystemElementWrapper element, string path)
        {
            if (element is FileWrapper)
                File.Copy(element.FullName, path);
            if (element is DirectoryWrapper dir)
                CopyDirectory(dir, path);
        }

        private void CopyDirectory(DirectoryWrapper dir, string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            foreach (var subDir in dir.EnumerateDirectories())
                CopyDirectory(subDir, Path.Combine(path, subDir.Name));

            foreach (var file in dir.EnumerateFiles())
                File.Copy(file.FullName, Path.Combine(path, file.Name));
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