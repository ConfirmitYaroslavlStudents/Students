using System;

namespace Sync.Wrappers
{
    public class FileAttributes
    {
        public long Size { get; }
        public long Hash { get; }
        public DateTime LastWriteTime { get; }

        public FileAttributes(long size, long hash, DateTime lastWriteTime)
        {
            Size = size;
            Hash = hash;
            LastWriteTime = lastWriteTime;
        }
    }
}