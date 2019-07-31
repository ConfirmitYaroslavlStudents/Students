using System;
using System.Collections.Generic;

namespace SyncTool.Wrappers
{
    public interface IFileSystemElement : IComparable<IFileSystemElement>
    {
        void Delete();
        void MoveTo(string destination);
        void CopyTo(string destination);
        string Name();
    }
}