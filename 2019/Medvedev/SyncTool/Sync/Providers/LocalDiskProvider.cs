using System.IO;
using Sync.Wrappers;

namespace Sync.Loaders
{
    public class LocalDiskProvider : IProvider
    {
        private string PathToDirectory { get; }

        public LocalDiskProvider(string path)
        {
            PathToDirectory = path;
        }

        public DirectoryWrapper LoadDirectory()
        {
            return DFS(new DirectoryWrapper(PathToDirectory));
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