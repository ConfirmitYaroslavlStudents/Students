using Logging;
using MasterSlaveSync.Loggers;
using System.IO;
using System.IO.Abstractions;

namespace MasterSlaveSync
{
    public class DefaultCopyDirectoryProcessor : ICopyDirectoryProcessor
    {
        private readonly Logger logger = LogManager.Logger;

        public void Execute(IDirectoryInfo masterDirectory, IDirectoryInfo target)
        {
            CopyAll(masterDirectory, target);

            var args = new ResolverEventArgs
            {
                ElementPath = target.FullName
            };

            logger.Summary(SummaryMessageCreator.DirectoryDeleted(args));
            logger.Verbose(VerboseMessageCreator.DirectoryDeleted(args));
        }

        private void CopyAll(IDirectoryInfo source, IDirectoryInfo target)
        {
            target.CreateSubdirectory(source.Name);

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
