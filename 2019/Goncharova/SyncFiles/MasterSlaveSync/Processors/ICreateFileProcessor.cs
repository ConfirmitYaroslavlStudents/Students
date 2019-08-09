using System.IO.Abstractions;

namespace MasterSlaveSync
{
    internal interface ICopyFileProcessor
    {
        bool Execute(IFileInfo masterFile, string masterPath, string slavePath);
    }
}