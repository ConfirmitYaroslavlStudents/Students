using System;
using System.IO;

namespace SyncTool.Wrappers
{
    public class DirectoryInfoInfoWrapper : IFileSystemElementInfoWrapper
    {
        private DirectoryInfo CurrentDirectory { get; }

        public DirectoryInfoInfoWrapper(DirectoryInfo currentDirectory)
        {
            CurrentDirectory = currentDirectory;
        }

        public int CompareTo(IFileSystemElementInfoWrapper obj)
        {
            if (obj is null)
                return -1;
            if (!(obj is DirectoryInfoInfoWrapper))
                throw new ArgumentException();

            var other = (DirectoryInfoInfoWrapper) obj;

            return string.Compare(CurrentDirectory.Name, other.CurrentDirectory.Name, StringComparison.Ordinal);
        }

        public override int GetHashCode()
        {
            return CurrentDirectory.FullName.GetHashCode();
        }

        public void Delete()
        {
            CurrentDirectory.Delete(true);
        }

        public void CopyTo(string destination)
        {
            var path = Path.Combine(destination, CurrentDirectory.Name);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            foreach (var file in CurrentDirectory.EnumerateFiles())
                file.CopyTo(Path.Combine(path, file.Name));

            foreach (var dir in CurrentDirectory.EnumerateDirectories())
                new DirectoryInfoInfoWrapper(dir).CopyTo(path);
        }

        public string Name()
        {
            return CurrentDirectory.Name;
        }

        public string GetPath()
        {
            return CurrentDirectory.Parent.FullName;
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            if (!(obj is DirectoryInfoInfoWrapper))
                return false;

            var other = (DirectoryInfoInfoWrapper) obj;
            return CurrentDirectory.FullName == other.CurrentDirectory.FullName;
        }
    }
}