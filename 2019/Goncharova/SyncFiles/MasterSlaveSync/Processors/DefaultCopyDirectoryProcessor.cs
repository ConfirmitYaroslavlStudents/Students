using System;
using System.IO;
using System.IO.Abstractions;

namespace MasterSlaveSync
{
    internal class DefaultCopyDirectoryProcessor : ICopyDirectoryProcessor
    {
        private IFileSystem fileSystem;
        public DefaultCopyDirectoryProcessor()
        {
            fileSystem = new FileSystem();
        }

        public event EventHandler<ResolverEventArgs> DirectoryCopied;

        public void Execute(IDirectoryInfo masterDirectory, string masterPath, string slavePath)
        {
            string directoryPath = masterDirectory.FullName.Substring(masterPath.Length);
            IDirectoryInfo target = fileSystem.Directory.CreateDirectory(Path.Combine(slavePath, directoryPath));

            CopyAll(masterDirectory, target);

            var args = new ResolverEventArgs
            {
                ElementPath = masterDirectory.FullName
            };
            OnDirectoryCopied(args);
        }
        protected virtual void OnDirectoryCopied(ResolverEventArgs e)
        {
            DirectoryCopied?.Invoke(this, e);
        }

        private void CopyAll(IDirectoryInfo source, IDirectoryInfo target)
        {
            fileSystem.Directory.CreateDirectory(target.FullName);

            foreach (var file in source.GetFiles())
            {
                file.CopyTo(Path.Combine(target.FullName, file.Name), true);
            }

            foreach (var sourceSubDirectory in source.GetDirectories())
            {
                var nextTargetSubDirectory = target.CreateSubdirectory(sourceSubDirectory.Name);
                CopyAll(sourceSubDirectory, nextTargetSubDirectory);
            }
        }

    }
}
