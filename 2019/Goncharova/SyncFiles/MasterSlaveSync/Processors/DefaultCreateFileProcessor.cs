using System.IO.Abstractions;
using System.IO;

namespace MasterSlaveSync
{
    class DefaultCreateFileProcessor : ICreateFileProcessor
    {
        public void Execute(IFileInfo masterFile, string masterPath, string slavePath)
        {
            string filePath = masterFile.FullName.Substring(masterPath.Length);

            masterFile.CopyTo(Path.Combine(slavePath, filePath));
        }
    }
}
