using System;
using System.Collections.Generic;
using Sync.Wrappers;

namespace Sync.Comparers
{
    public class DefaultFileSystemElementsComparer : IComparer<IFileSystemElementInfoWrapper>
    {
        private readonly IComparer<FileInfoInfoWrapper> _fileComparer;
        private readonly IComparer<DirectoryInfoWrapper> _directoryComparer;

        public DefaultFileSystemElementsComparer()
        {
            _fileComparer = new DefaultFileComparer();
            _directoryComparer = new DefaultDirectoryComparer();
        }

        public int Compare(IFileSystemElementInfoWrapper x, IFileSystemElementInfoWrapper y)
        {
            if (x.GetType() != y.GetType())
                throw new ArgumentException("Parameters must have same attribute");

            if (x is FileInfoInfoWrapper firstFile && y is FileInfoInfoWrapper secondFile)
                return _fileComparer.Compare(firstFile, secondFile);
            if (x is DirectoryInfoWrapper firstDirectory && y is DirectoryInfoWrapper secondDirectory)
                return _directoryComparer.Compare(firstDirectory, secondDirectory);
            return 0;
        }
    }
}