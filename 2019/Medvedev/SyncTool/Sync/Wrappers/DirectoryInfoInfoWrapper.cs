using System.IO;

namespace Sync.Wrappers
{
    public class DirectoryInfoInfoWrapper : IFileSystemElementInfoWrapper
    {
        public DirectoryInfo CurrentDirectory { get; }

        public DirectoryInfoInfoWrapper(DirectoryInfo currentDirectory)
        {
            CurrentDirectory = currentDirectory;
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

        public string Name => CurrentDirectory.Name;

        public string ParentDirectory => CurrentDirectory.Parent.FullName;

        public string ElementType => Type;
        public static string Type => "Directory";

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