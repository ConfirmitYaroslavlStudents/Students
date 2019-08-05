using System;
using System.Collections.Generic;
using System.IO;

namespace Sync.Wrappers
{
    public class DirectoryWrapper : IFileSystemElementWrapper
    {
        private readonly HashSet<FileWrapper> _files;

        private readonly HashSet<DirectoryWrapper> _subDirectories;

        public DirectoryWrapper(string fullName, DirectoryWrapper parent = null)
        {
            if (parent != null &&
                string.Compare(
                    Path.GetDirectoryName(fullName),
                    parent.FullName,
                    StringComparison.InvariantCultureIgnoreCase) != 0)
                throw new ArgumentException("Path to directory does not math parent directory full name");

            _subDirectories = new HashSet<DirectoryWrapper>();
            _files = new HashSet<FileWrapper>();

            FullName = fullName;
            if (parent != null)
                Name = Path.GetFileName(fullName);
            else
                Name = fullName;

            ParentDirectory = parent;
        }

        public static string Type => "Directory";
        public string Name { get; }
        public DirectoryWrapper ParentDirectory { get; }
        public string ElementType => Type;
        public string FullName { get; }

        public DirectoryWrapper CreateDirectory(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name must not be null or empty");

            var subDirectory = new DirectoryWrapper(Path.Combine(FullName, name), this);
            _subDirectories.Add(subDirectory);
            return subDirectory;
        }

        public FileWrapper CreateFile(string name, FileAttributes attributes)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("File name must not be null or empty");

            var file = new FileWrapper(Path.Combine(FullName, name), attributes, this);
            _files.Add(file);
            return file;
        }

        public IEnumerable<DirectoryWrapper> EnumerateDirectories()
        {
            foreach (var dir in _subDirectories)
                yield return dir;
        }

        public IEnumerable<FileWrapper> EnumerateFiles()
        {
            foreach (var file in _files)
                yield return file;
        }

        public IEnumerable<IFileSystemElementWrapper> EnumerateContainment()
        {
            foreach (var dir in EnumerateDirectories())
                yield return dir;
            foreach (var file in EnumerateFiles())
                yield return file;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is DirectoryWrapper))
                return false;

            var other = (DirectoryWrapper) obj;

            return string.Compare(FullName, other.FullName, StringComparison.CurrentCultureIgnoreCase) == 0;
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