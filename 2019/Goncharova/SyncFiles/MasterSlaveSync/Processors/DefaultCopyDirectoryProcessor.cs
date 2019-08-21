using System;
using System.IO;
using System.IO.Abstractions;

namespace MasterSlaveSync
{
    public class DefaultCopyDirectoryProcessor : ICopyDirectoryProcessor
    {
        public event EventHandler<ResolverEventArgs> DirectoryCopied;

        public void Execute(IDirectoryInfo masterDirectory, IDirectoryInfo target)
        {
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
            target.CreateSubdirectory(target.FullName);

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
