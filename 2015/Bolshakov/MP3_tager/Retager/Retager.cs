using System.Collections.Generic;
using System.IO;
using Id3;

namespace RetagerLib
{
    public class Retager
    {
        public void TagFile(string path, string pattern)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException();
            var parsedTags = ParseTags(path, pattern);
            if (parsedTags == null)
                throw new NotValidPatternException();
            InsertTags(path, parsedTags);
        }

        private void InsertTags(string path, Dictionary<FrameType, string> tags)
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
                        #endregion
                }
            }
            mp3File.WriteTag(idTag, new WriteConflictAction());
            mp3File.Dispose();
        }

        private Dictionary<FrameType, string> ParseTags(string path, string pattern)
        {
            var clearPath = Path.GetFileNameWithoutExtension(path);

            var parser = new TagParser(pattern);
            var frames = parser.GetFrames(clearPath);
            return frames;
        }
    }
}
