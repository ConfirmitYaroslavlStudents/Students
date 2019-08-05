using System;
using System.Collections.Generic;
using Sync.Wrappers;

namespace Sync.Comparers
{
    public class DefaultFileSystemElementsComparer : IComparer<IFileSystemElementWrapper>
    {
        private readonly IComparer<DirectoryWrapper> _directoryComparer;
        private readonly IComparer<FileWrapper> _fileComparer;

        public DefaultFileSystemElementsComparer()
        {
            _fileComparer = new DefaultFileComparer();
            _directoryComparer = new DefaultDirectoryComparer();
        }

        public int Compare(IFileSystemElementWrapper x, IFileSystemElementWrapper y)
        {
            if (x is null && y is null)
                return 0;
            if (x is null)
                return 1;
            if (y is null)
                return -1;

            if (x.GetType() != y.GetType())
                throw new ArgumentException("Parameters must have same attribute");

            if (x is FileWrapper firstFile && y is FileWrapper secondFile)
                return _fileComparer.Compare(firstFile, secondFile);
            if (x is DirectoryWrapper firstDirectory && y is DirectoryWrapper secondDirectory)
                return _directoryComparer.Compare(firstDirectory, secondDirectory);
            return 0;
        }
    }
}