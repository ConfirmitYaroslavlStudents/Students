using System;
using System.Collections.Generic;

namespace SyncTool.Wrappers
{
    public interface IFileSystemElementWrapper : IComparable<IFileSystemElementWrapper>
    {
        void Delete();
        void MoveTo(string destination);
        void CopyTo(string destination);
        string Name();
    }
}