using System.IO.Abstractions;

namespace MasterSlaveSync
{
    class NoDeleteFileProcessor : IDeleteFileProcessor
    {
        public bool Execute(IFileInfo slaveFile)
        {
            return false;
        }
    }
}
