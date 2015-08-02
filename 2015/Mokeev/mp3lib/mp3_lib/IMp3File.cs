using System.Collections.Generic;

namespace mp3lib
{
    public interface IMp3File
    {
        string Album { get; set; }
        string Artist { get; set; }
        string Comment { get; set; }
        string FilePath { get; }
        ushort Genre { get; set; }
        string Title { get; set; }
        ushort TrackId { get; set; }
        string Year { get; set; }

        Dictionary<TagType, string> GetId3Data();
    }
}