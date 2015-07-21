using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Id3;

namespace MP3_tager
{
    static class CodeBehind
    {
        public static void TagFile(string path, string pattern)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException();
            var parsedTags = PrseTags(path, pattern);
            InsertTags(path, parsedTags);
        }

        private static void InsertTags(string path, Dictionary<TagTypes, string> parsedTags)
        {
            var mp3File = new Mp3File(path, Mp3Permissions.ReadWrite);
            var tag = mp3File.GetTag(Id3TagFamily.FileStartTag);
            foreach (var item in parsedTags)
            {
                switch (item.Key)
                {
                    case TagTypes.Artist:
                        tag.Artists.Value = item.Value;
                        break;
                    case TagTypes.Title:
                        tag.Title.Value = item.Value;
                        break;
                    case TagTypes.Album:
                        tag.Album.Value = item.Value;
                        break;
                    case TagTypes.Track:
                        tag.Track.Value = item.Value;
                        break;
                    default:
                        break;
                }
            }
            mp3File.WriteTag(tag, new WriteConflictAction());
            mp3File.Dispose();
        }

        private static Dictionary<TagTypes, string> PrseTags(string path, string pattern)
        {
            var dict = new Dictionary<TagTypes, string>();
            dict.Add(TagTypes.Album, "New album");
            dict.Add(TagTypes.Artist, "New artist");
            dict.Add(TagTypes.Title, "New title");
            dict.Add(TagTypes.Track, "10");
            return dict;
        }
    }
}
