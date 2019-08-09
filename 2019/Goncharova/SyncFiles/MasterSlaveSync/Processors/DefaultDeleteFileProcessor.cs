using System.IO.Abstractions;

namespace MasterSlaveSync
{
    internal class DefaultDeleteFileProcessor : IDeleteFileProcessor
    {
        public bool Execute(IFileInfo slaveFile)
        {
            slaveFile.Delete();

            return true;
        }
    }
}
