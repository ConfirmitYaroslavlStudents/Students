using System;
using System.IO;

namespace Sync.Wrappers
{
    public class FileWrapper : IFileSystemElementWrapper
    {
        private DirectoryWrapper _parentDirectory;

        public FileWrapper(string fullName, FileAttributes attributes, DirectoryWrapper parentDirectory)
        {
            if (string.Compare(
                    Path.GetDirectoryName(fullName),
                    parentDirectory.FullName,
                    StringComparison.InvariantCultureIgnoreCase) != 0)
                throw new ArgumentException("Path to file does not math parent directory");

            Name = Path.GetFileName(fullName);
            FullName = fullName;

            ParentDirectory = parentDirectory;

            Attributes = attributes;
        }

        public FileAttributes Attributes { get; }
        public static string Type => "File";
        public string Name { get; }

        public DirectoryWrapper ParentDirectory
        {
            get => _parentDirectory;
            set
            {
                if (value is null)
                    throw new ArgumentException("Parent directory must not be null");
                _parentDirectory = value;
            }
        }

        public string ElementType => Type;
        public string FullName { get; }

        public override bool Equals(object obj)
        {
            if (!(obj is FileWrapper))
                return false;

            var other = (FileWrapper) obj;
            return string.Compare(FullName, other.FullName, StringComparison.InvariantCultureIgnoreCase) == 0;
        }

        public override int GetHashCode()
        {
            return FullName.ToLower().GetHashCode();
        }

        public override string ToString()
        {
            return FullName;
        }
    }
}