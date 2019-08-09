using System.IO;
using System.IO.Abstractions;

namespace MasterSlaveSync
{
    internal class DefaultCopyDirectoryProcessor : ICopyDirectoryProcessor
    {
        private IFileSystem _fileSystem;
        public DefaultCopyDirectoryProcessor()
        {
            _fileSystem = new FileSystem();
        }
        public bool Execute(IDirectoryInfo masterDirectory, string masterPath, string slavePath)
        {
            string smth = masterDirectory.FullName.Substring(masterPath.Length);
            IDirectoryInfo target = _fileSystem.Directory.CreateDirectory(Path.Combine(slavePath, smth));

            CopyAll(masterDirectory, target);

            return true;
        }

        private static void CopyAll(IDirectoryInfo source, IDirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            foreach (var fi in source.GetFiles())
            {
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }

            foreach (var diSourceSubDir in source.GetDirectories())
            {
                var nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }

    }
}
