using System;
using System.IO;

namespace SyncTool.Wrappers
{
    public class FileInfoInfoWrapper : IFileSystemElementInfoWrapper
    {
        public FileInfo File { get; }

        public FileInfoInfoWrapper(FileInfo file)
        {
            File = file;
        }

        public int CompareTo(IFileSystemElementInfoWrapper obj)
        {
            if (obj is null)
                return -1;
            if (!(obj is FileInfoInfoWrapper))
                throw new ArgumentException();

            var other = (FileInfoInfoWrapper) obj;

            using (var fs = File.OpenRead())
            {
                using (var fsOther = other.File.OpenRead())
                {
                    int comparision = CompareFiles(fs, fsOther);
                    if (comparision != FailedComparision)
                        return comparision;

                    return other.File.LastWriteTime.CompareTo(File.LastWriteTime);
                }
            }
        }

        public override int GetHashCode()
        {
            return File.FullName.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            if (!(obj is FileInfoInfoWrapper))
                return false;

            var other = (FileInfoInfoWrapper) obj;
            return File.FullName == other.File.FullName;
        }

        private static int CompareFiles(FileStream fs, FileStream fsOther)
        {
            while (true)
            {
                var a = fs.ReadByte();
                var b = fsOther.ReadByte();

                if (a == EndOfFile && b != EndOfFile)
                    return 1;
                if (a != EndOfFile && b == EndOfFile)
                    return -1;
                if (a == EndOfFile && b == EndOfFile)
                    return 0;

                if (a != b)
                    return FailedComparision;
            }
        }

        public void Delete()
        {
            File.Delete();
        }

        public void CopyTo(string destination)
        {
            File.CopyTo(Path.Combine(destination, Name()));
        }

        public string Name()
        {
            return File.Name;
        }

        public string GetPath()
        {
            return File.DirectoryName;
        }

        private const int EndOfFile = -1;
        private const int FailedComparision = 2;
    }
}