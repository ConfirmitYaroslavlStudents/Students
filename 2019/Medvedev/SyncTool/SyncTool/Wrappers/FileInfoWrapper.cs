using System;
using System.IO;

namespace SyncTool.Wrappers
{
    public class FileInfoWrapper : IFileSystemElementWrapper
    {
        public FileInfo File { get; }

        public FileInfoWrapper(FileInfo file)
        {
            File = file;
        }

        public int CompareTo(IFileSystemElementWrapper obj)
        {
            if (obj is null)
                return -1;
            if (!(obj is FileInfoWrapper))
                throw new ArgumentException();

            var other = (FileInfoWrapper) obj;

            using (FileStream fs = File.OpenRead())
            {
                using (FileStream fsOther = other.File.OpenRead())
                {
                    return CompareFiles(fs, fsOther);
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
            if (!(obj is FileInfoWrapper))
                return false;

            var other = (FileInfoWrapper) obj;
            return File.FullName == other.File.FullName;
        }

        private static int CompareFiles(FileStream fs, FileStream fsOther)
        {
            while (true)
            {
                int a = fs.ReadByte();
                int b = fsOther.ReadByte();

                if (a == -1 && b != -1)
                    return 1;
                if (a != -1 && b == -1)
                    return -1;
                if (a == -1 && b == -1)
                    return 0;

                if (a != b)
                    return 1;
            }
        }

        public void Delete()
        {
            File.Delete();
        }

        public void MoveTo(string destination)
        {
            File.MoveTo(destination);
        }

        public void CopyTo(string destination)
        {
            File.CopyTo(destination);
        }

        public string Name()
        {
            return File.Name;
        }
    }
}