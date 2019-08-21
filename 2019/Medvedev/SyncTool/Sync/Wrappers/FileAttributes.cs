using System;

namespace Sync.Wrappers
{
    public class FileAttributes
    {
        public FileAttributes(long size, DateTime lastWriteTime)
        {
            Size = size;
            LastWriteTime = lastWriteTime;
        }

        public long Size { get; }
        public DateTime LastWriteTime { get; }
    }
}