using Logging;
using MasterSlaveSync.Loggers;
using System.IO.Abstractions;

namespace MasterSlaveSync
{
    public class DefaultCopyFileProcessor : ICopyFileProcessor
    {
        private readonly Logger logger = LogManager.Logger;

        public void Execute(IFileInfo masterFile, string masterPath, string slavePath)
        {
            string filePath = masterFile.FullName.Substring(masterPath.Length);
            string destPath = slavePath + "\\" + filePath;

            masterFile.CopyTo(destPath);

            var args = new ResolverEventArgs
            {
                ElementPath = destPath
            };

            logger.Summary(SummaryMessageCreator.FileCopied(args));
        }
    }
}
