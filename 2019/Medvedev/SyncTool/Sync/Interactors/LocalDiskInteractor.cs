using System.IO;
using Sync.Wrappers;

namespace Sync.Interactors
{
    public class LocalDiskInteractor : IInteractor
    {
        public void Delete(IFileSystemElementWrapper element)
        {
            if (element is FileWrapper)
                File.Delete(element.FullName);
            if (element is DirectoryWrapper)
                Directory.Delete(element.FullName, true);
        }

        public void CopyTo(IFileSystemElementWrapper element, string path)
        {
            if (element is FileWrapper)
                File.Copy(element.FullName, path);
            if (element is DirectoryWrapper dir)
                CopyDirectory(dir, path);
        }

        private void CopyDirectory(DirectoryWrapper dir, string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            foreach (var subDir in dir.EnumerateDirectories())
                CopyDirectory(subDir, Path.Combine(path, subDir.Name));

            foreach (var file in dir.EnumerateFiles())
                File.Copy(file.FullName, Path.Combine(path, file.Name));
        }
    }
}