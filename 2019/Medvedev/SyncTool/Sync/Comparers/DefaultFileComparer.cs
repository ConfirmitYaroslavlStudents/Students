using System.Collections.Generic;
using System.IO;
using Sync.Wrappers;

namespace Sync.Comparers
{
    public class DefaultFileComparer : IComparer<FileInfoInfoWrapper>
    {
        public int Compare(FileInfoInfoWrapper x, FileInfoInfoWrapper y)
        {
            if (x is null)
                return -1;
            if (y is null)
                return 1;

            using (var fs = x.File.OpenRead())
            using (var fsOther = y.File.OpenRead())
            {
                var comparision = CompareContainment(fs, fsOther);
                if (comparision != FailedComparision)
                    return comparision;

                return y.File.LastWriteTime.CompareTo(x.File.LastWriteTime);
            }
        }

        public int CompareContainment(FileStream x, FileStream y)
        {
            while (true)
            {
                var a = x.ReadByte();
                var b = y.ReadByte();

                if (a == EndOfFile && b != EndOfFile)
                    return 1;
                if (a != EndOfFile && b == EndOfFile)
                    return -1;
                if (a == EndOfFile && b == EndOfFile)
                    return 0;

                if (a != b)
                    return FailedComparision;
            }
        }

        private const int EndOfFile = -1;
        private const int FailedComparision = 2;
    }
}