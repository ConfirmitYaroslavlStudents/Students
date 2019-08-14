using System.IO.Abstractions;

namespace MasterSlaveSync
{
    internal interface ICopyDirectoryProcessor
    {
        bool Execute(IDirectoryInfo masterDirectory, string masterPath, string slavePath);
    }
}