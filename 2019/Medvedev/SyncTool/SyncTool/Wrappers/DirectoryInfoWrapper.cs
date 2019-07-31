using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SyncTool.Wrappers
{
    public class DirectoryInfoWrapper : IFileSystemElementWrapper
    {
        private DirectoryInfo CurrentDirectory { get; }

        public DirectoryInfoWrapper(DirectoryInfo currentDirectory)
        {
            CurrentDirectory = currentDirectory;
        }

        public int CompareTo(IFileSystemElementWrapper obj)
        {
            if (obj is null)
                return 1;
            if (!(obj is DirectoryInfoWrapper))
                throw new ArgumentException();

            var other = (DirectoryInfoWrapper) obj;

            return CurrentDirectory.LastWriteTime.CompareTo(other.CurrentDirectory.LastWriteTime);
        }

        public override int GetHashCode()
        {
            return CurrentDirectory.FullName.GetHashCode();
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


        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            if (!(obj is DirectoryInfoWrapper))
                return false;

            var other = (DirectoryInfoWrapper)obj;
            return CurrentDirectory.FullName == other.CurrentDirectory.FullName;
        }
    }
}