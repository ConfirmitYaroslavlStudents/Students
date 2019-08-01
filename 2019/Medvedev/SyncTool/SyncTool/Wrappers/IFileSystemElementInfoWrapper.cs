using System;

namespace SyncTool.Wrappers
{
    public interface IFileSystemElementInfoWrapper : IComparable<IFileSystemElementInfoWrapper>
    {
        void Delete();
        void CopyTo(string destination);
        string Name();
        string GetPath();
    }
}