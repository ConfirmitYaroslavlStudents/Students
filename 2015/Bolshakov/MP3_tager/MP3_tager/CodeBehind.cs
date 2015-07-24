using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using Id3;

namespace MP3_tager
{
    //[TODO] non static and rename
    static class CodeBehind
    {
        public static void TagFile(string path, string pattern)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException();
            var parsedTags = ParseTags(path, pattern);
            if (parsedTags == null)
                throw new NotValidPatternException();
            InsertTags(path, parsedTags);
        }

        private static void InsertTags(string path, Dictionary<FrameType, string> tags)
        {
            var mp3File = new Mp3File(path, Mp3Permissions.ReadWrite);
            var idTag = mp3File.GetTag(Id3TagFamily.FileStartTag);
            foreach (var item in tags)
            {
                switch (item.Key)
                {
                    #region Boring cases
                    case FrameType.Artist:
                        idTag.Artists.Values.Clear();
                        idTag.Artists.Value = item.Value;
                        break;
                    case FrameType.Title:
                        idTag.Title.Value = item.Value;
                        break;
                    case FrameType.Album:
                        idTag.Album.Value = item.Value;
                        break;
                    case FrameType.Track:
                        idTag.Track.Value = item.Value;
                        break;
                    case FrameType.Year:
                        idTag.Year.Value = item.Value;
                        break;
                    default:
                        break;                    
                    #endregion
                }
            }
            mp3File.WriteTag(idTag, new WriteConflictAction());
            mp3File.Dispose();
        }

        private static Dictionary<FrameType, string> ParseTags(string path, string pattern)
        {
            var a = new TagParser(pattern);

            var regex = new Regex(@"[^\\]*?(?=(\.mp3))");
            var math = regex.Match(path);
            var clearPath = math.Value;

            var dict = a.GetFrames(clearPath);
            return dict;
        }
    }
}
