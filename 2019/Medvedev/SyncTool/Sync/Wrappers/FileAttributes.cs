using System;

namespace Sync.Wrappers
{
    public class FileAttributes
    {
        public long Size { get; }
        public DateTime LastWriteTime { get; }

        public FileAttributes(long size, DateTime lastWriteTime)
        {
            Size = size;
            LastWriteTime = lastWriteTime;
        }
    }
}