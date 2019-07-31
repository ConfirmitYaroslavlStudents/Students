using System;
using System.IO;

namespace SyncTool.Wrappers
{
    public class DirectoryInfoWrapper : IFileSystemElement
    {
        private DirectoryInfo CurrentDirectory { get; }

        public DirectoryInfoWrapper(DirectoryInfo currentDirectory)
        {
            CurrentDirectory = currentDirectory;
        }

        public int CompareTo(IFileSystemElement obj)
        {
            if (obj is null)
                return 1;
            if (!(obj is DirectoryInfoWrapper))
                throw new ArgumentException();

            var other = (DirectoryInfoWrapper) obj;

            return CurrentDirectory.LastWriteTime.CompareTo(other.CurrentDirectory.LastWriteTime);
        }

        public void Delete()
        {
            CurrentDirectory.Delete(true);
        }

        public void MoveTo(string destination)
        {
            CurrentDirectory.MoveTo(destination);
        }

        public void CopyTo(string destination)
        {
            string path = Path.Combine(destination, CurrentDirectory.Name);
            if (Directory.Exists(destination + CurrentDirectory.Name))
                Directory.CreateDirectory(path);

            foreach (var file in CurrentDirectory.EnumerateFiles())
                file.CopyTo(path);

            foreach (var dir in CurrentDirectory.EnumerateDirectories())
                new DirectoryInfoWrapper(dir).CopyTo(path);
        }

        public string Name()
        {
            return CurrentDirectory.Name;
        }
    }
}