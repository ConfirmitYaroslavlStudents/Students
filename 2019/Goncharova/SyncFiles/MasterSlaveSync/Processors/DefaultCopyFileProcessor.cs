using System.IO.Abstractions;

namespace MasterSlaveSync
{
    internal class DefaultCopyFileProcessor : ICopyFileProcessor
    {
        public bool Execute(IFileInfo masterFile, string masterPath, string slavePath)
        {
            string filePath = masterFile.FullName.Substring(masterPath.Length);
            string destPath = slavePath + "\\" + filePath;

            masterFile.CopyTo(destPath);

            return true;
        }
    }
}
