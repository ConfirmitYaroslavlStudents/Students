using System;
using System.IO;

namespace SyncTool.Wrappers
{
    public class FileInfoWrapper : IFileSystemElement
    {
        public FileInfo File { get; }

        public FileInfoWrapper(FileInfo file)
        {
            File = file;
        }

        public int CompareTo(IFileSystemElement obj)
        {
            if (obj is null)
                return 1;
            if (!(obj is FileInfoWrapper))
                throw new ArgumentException();

            var other = (FileInfoWrapper) obj;

            using (FileStream fs = File.OpenRead())
            {
                using (FileStream fsOther = other.File.OpenRead())
                {
                    if (!CompareFiles(fs, fsOther))
                        return -1;

                    if (fs.CanRead && !fsOther.CanRead)
                        return -1;
                    if (!fs.CanRead && fsOther.CanRead)
                        return 1;
                    return 0;
                }
            }
        }

        private static bool CompareFiles(FileStream fs, FileStream fsOther)
        {
            while (fs.CanRead && fsOther.CanRead)
            {
                int a = fs.ReadByte();
                int b = fsOther.ReadByte();

                if (a != b)
                    return false;
            }

            return true;
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