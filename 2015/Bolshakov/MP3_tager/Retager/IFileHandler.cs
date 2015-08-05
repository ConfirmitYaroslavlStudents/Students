using System;
using System.Collections.Generic;

namespace Mp3Handler
{
    public interface IFileHandler:IDisposable
    {
        string FilePath { get; set; }

        string FileName { get; }

        Dictionary<FrameType, string> GetTags(GetTagsOption option = GetTagsOption.RemoveEmptyTags);

        void SetTags(Dictionary<FrameType, string> tags);

        void Rename(string newName);
    }

    public enum GetTagsOption
    {
        RemoveEmptyTags,
        DontRemoveEmptyTags
    }
}
