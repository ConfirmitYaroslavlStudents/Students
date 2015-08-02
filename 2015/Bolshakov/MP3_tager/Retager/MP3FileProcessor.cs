using System;
using System.Collections.Generic;
using System.IO;
using Id3;

namespace Mp3Handler
{
    public class Mp3FileProcessor
    {
        //todo: think about retuning type
        public bool RetagFile(string path, string pattern)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException();
            var parsedTags = ParseTags(path, pattern);
            if (parsedTags == null)
                return false;
            InsertTags(path, parsedTags);
            return true;
        }

        public void RenameFile(string path, string pattern)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException();
            GetTags(path);
        }

        private Dictionary<FrameType, string> GetTags(string path)
        {
            var mp3File = new Mp3File(path, Mp3Permissions.ReadWrite);
            var idTag = mp3File.GetTag(Id3TagFamily.FileStartTag);

            var enableTags = GetEmptyTagDictionary();
            var resultDictionary = new Dictionary<FrameType,string>();
            foreach (var tag in enableTags)
            {
                switch (tag)
                {
                    case FrameType.Artist:
                        resultDictionary.Add(tag,idTag.Artists);
                        break;
                    case FrameType.Title:
                        resultDictionary.Add(tag, idTag.Title);
                        break;
                    case FrameType.Album:
                        resultDictionary.Add(tag, idTag.Album);
                        break;
                    case FrameType.Track:
                        resultDictionary.Add(tag, idTag.Track);
                        break;
                    case FrameType.Year:
                        resultDictionary.Add(tag, idTag.Year);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            return resultDictionary;
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

        private List<FrameType> GetEmptyTagDictionary()
        {
            return new List<FrameType>
            {
                {FrameType.Album},
                {FrameType.Artist},
                {FrameType.Title},
                {FrameType.Track},
                {FrameType.Year}
            };
        }
    }
}
