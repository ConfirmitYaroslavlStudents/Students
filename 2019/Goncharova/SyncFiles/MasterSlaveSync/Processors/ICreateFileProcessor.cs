using System.IO.Abstractions;

namespace MasterSlaveSync
{
    internal interface ICreateFileProcessor
    {
        void Execute(IFileInfo masterFile, string masterPath, string slavePath);
    }
}