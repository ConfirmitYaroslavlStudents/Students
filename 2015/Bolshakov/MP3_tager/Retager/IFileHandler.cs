using System.Collections.Generic;

namespace Mp3Handler
{
    public interface IFileHandler
    {
        string FilePath { get; }

        string FileName { get; }

        Dictionary<FrameType, string> GetTags(bool removeEmptyTags);

        void SetTags(Dictionary<FrameType, string> tags);

        void Rename(string newName);
    }
}
