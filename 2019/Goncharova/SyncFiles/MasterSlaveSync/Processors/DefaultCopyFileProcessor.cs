using System;
using System.IO.Abstractions;

namespace MasterSlaveSync
{
    public class DefaultCopyFileProcessor : ICopyFileProcessor
    {
        public event EventHandler<ResolverEventArgs> FileCopied;

        public void Execute(IFileInfo masterFile, string masterPath, string slavePath)
        {
            string filePath = masterFile.FullName.Substring(masterPath.Length);
            string destPath = slavePath + "\\" + filePath;

            masterFile.CopyTo(destPath);

            var args = new ResolverEventArgs
            {
                ElementPath = masterFile.FullName
            };
            OnFileCopied(args);

        }
        protected virtual void OnFileCopied(ResolverEventArgs e)
        {
            FileCopied?.Invoke(this, e);
        }
    }
}
