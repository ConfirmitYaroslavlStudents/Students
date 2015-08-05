using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Mp3Handler
{
    public class Mp3FileProcessor
    {
        public Mp3FileProcessor(IFileHandler fileHandler)
        {
            _fileHandler = fileHandler;
        }

        public Mp3FileProcessor(string filePath)
        {
            _fileHandler = new Id3LibFileHandler(filePath);
        }

        public bool RetagFile(string pattern)
        {
            var parser = new TagParser(pattern);
            var parsedTags = parser.GetTagsValue(_fileHandler.FileName);
            if (parsedTags == null)
                return false;
            _fileHandler.SetTags(parsedTags);
            return true;
        }

        public bool RenameFile(string pattern)
        {
            var fileTags = _fileHandler.GetTags(true);
            var newFileName = new StringBuilder(pattern);
            foreach (var fileTag in fileTags)
            {
                newFileName.Replace(Frame.GetString(fileTag.Key), fileTag.Value);
            }
            if (newFileName.Length != newFileName.Replace("<", "").Length)
                return false;
            _fileHandler.Rename(newFileName.ToString());
            return true;
        }

        public Dictionary<FrameType, TagDifference> Difference(string pattern)
        {
            var parser = new TagParser(pattern);
            var pathTags = parser.GetTagsValue(_fileHandler.FileName);
            if (pathTags == null)
                return null;

            var fileTags = _fileHandler.GetTags(false);

            var tagDifference = new Dictionary<FrameType,TagDifference>();

            //todo: convert into linq
            foreach (var pathTag in pathTags)
            {
                if (pathTag.Value != fileTags[pathTag.Key])
                    tagDifference.Add(pathTag.Key, new TagDifference(fileTags[pathTag.Key], pathTag.Value));
            }

            return tagDifference.Count != 0 ? tagDifference : null;
        }

        public bool Synchronize(string pattern)
        {
            return false;
        }

        private IFileHandler _fileHandler;

#region old
        //private Dictionary<FrameType, string> GetTags(string path)
        //{
        //    var enableTags = GetEmptyTagDictionary();
        //    var resultDictionary = new Dictionary<FrameType,string>();
        //    foreach (var tag in enableTags)
        //    {
        //        switch (tag)
        //        {
        //            case FrameType.Artist:
        //                resultDictionary.Add(tag,idTag.Artists);
        //                break;
        //            case FrameType.Title:
        //                resultDictionary.Add(tag, idTag.Title);
        //                break;
        //            case FrameType.Album:
        //                resultDictionary.Add(tag, idTag.Album);
        //                break;
        //            case FrameType.Track:
        //                resultDictionary.Add(tag, idTag.Track);
        //                break;
        //            case FrameType.Year:
        //                resultDictionary.Add(tag, idTag.Year);
        //                break;
        //            default:
        //                throw new ArgumentOutOfRangeException();
        //        }
        //    }
        //    return resultDictionary;
        //}

        //private void InsertTags(string path, Dictionary<FrameType, string> tags)
        //{
        //    var mp3File = new Mp3File(path, Mp3Permissions.ReadWrite);
        //    var idTag = mp3File.GetTag(Id3TagFamily.FileStartTag);
        //    foreach (var item in tags)
        //    {
        //        switch (item.Key)
        //        {
        //            #region Boring cases
        //            case FrameType.Artist:
        //                idTag.Artists.Values.Clear();
        //                idTag.Artists.Value = item.Value;
        //                break;
        //            case FrameType.Title:
        //                idTag.Title.Value = item.Value;
        //                break;
        //            case FrameType.Album:
        //                idTag.Album.Value = item.Value;
        //                break;
        //            case FrameType.Track:
        //                idTag.Track.Value = item.Value;
        //                break;
        //            case FrameType.Year:
        //                idTag.Year.Value = item.Value;
        //                break;
        //                #endregion
        //        }
        //    }
        //    mp3File.WriteTag(idTag, new WriteConflictAction());
        //    mp3File.Dispose();
        //}

        //private List<FrameType> GetEmptyTagDictionary()
        //{
        //    return new List<FrameType>
        //    {
        //        {FrameType.Album},
        //        {FrameType.Artist},
        //        {FrameType.Title},
        //        {FrameType.Track},
        //        {FrameType.Year}
        //    };
        //}
#endregion
    }
}
