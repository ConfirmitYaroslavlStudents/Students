using System.IO;

namespace Sync.Wrappers
{
    public class FileInfoInfoWrapper : IFileSystemElementInfoWrapper
    {
        public FileInfo File { get; }

        public FileInfoInfoWrapper(FileInfo file)
        {
            File = file;
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

        public void Delete()
        {
            File.Delete();
        }

        public void CopyTo(string destination)
        {
            File.CopyTo(Path.Combine(destination, Name));
        }

        public string Name => File.Name;

        public string ParentDirectory => File.DirectoryName;

        public string ElementType => Type;

        public static string Type => "File";
    }
}