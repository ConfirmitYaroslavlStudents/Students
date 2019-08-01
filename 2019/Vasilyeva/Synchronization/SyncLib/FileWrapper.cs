using System;
using System.IO;

namespace SyncLib
{
    internal class FileWrapper 
    {
        public FileWrapper(string path)
        {
            Path = path;
        }

        private string Path;

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            if (!(obj is FileWrapper))
                throw new ArgumentException();

            var other = ((FileWrapper)obj).Path;

            using (var thisFileStream = File.OpenRead(Path))
            using (var otherFileStream = File.OpenRead(other))
            {
                while (true)
                {
                    var byteThis = thisFileStream.ReadByte();
                    var byteOther = otherFileStream.ReadByte();

                    if (byteThis != byteOther)
                    {
                        return false;
                    }

                    if (byteThis == -1 && byteOther == -1)
                        return true;
                }
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static void Replace(FileWrapper masterFileWrapper, FileWrapper slaveFileWrapper)
        {
            File.Delete(slaveFileWrapper.Path);
            File.Copy(masterFileWrapper.Path, slaveFileWrapper.Path);
        }
    }
}
