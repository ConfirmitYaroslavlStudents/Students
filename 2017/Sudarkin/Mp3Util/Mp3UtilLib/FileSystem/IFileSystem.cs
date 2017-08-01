using System.Collections.Generic;

namespace Mp3UtilLib.FileSystem
{
    public interface IFileSystem
    {
        bool Exists(string path);
        void Move(string source, string dest);
        IEnumerable<AudioFile> GetAudioFilesFromCurrentDirectory(string searchPattern, bool recursive);
    }
}