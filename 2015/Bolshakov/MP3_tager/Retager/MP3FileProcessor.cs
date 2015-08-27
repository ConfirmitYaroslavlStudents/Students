using System.Collections.Generic;
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
            var fileTags = _fileHandler.GetTags();
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

        public bool Synchronize(string pattern)
        {
            var parser = new TagParser(pattern);
            var pathTagsValue = parser.GetTagsValue(_fileHandler.FileName);

            var requringTags = new SortedSet<FrameType>(parser.GetTags());
            var fileTags = new SortedSet<FrameType>(_fileHandler.GetTags().Keys);

            var pathTagsIsBad = pathTagsValue == null;
            var fileTagsIsBad = !requringTags.IsSubsetOf(fileTags);

            if (pathTagsIsBad && !fileTagsIsBad)
                return RenameFile(pattern);

            if (!pathTagsIsBad && fileTagsIsBad)
                return RetagFile(pattern);

            return false;
        }

        public Dictionary<FrameType, TagDifference> Difference(string pattern)
        {
            var parser = new TagParser(pattern);
            var pathTags = parser.GetTagsValue(_fileHandler.FileName);
            if (pathTags == null)
                return null;

            var fileTags = _fileHandler.GetTags(GetTagsOption.DontRemoveEmptyTags);

            var tagDifference = new Dictionary<FrameType,TagDifference>();

            foreach (var pathTag in pathTags)
            {
                if (pathTag.Value != fileTags[pathTag.Key])
                    tagDifference.Add(pathTag.Key, new TagDifference(fileTags[pathTag.Key], pathTag.Value));
            }

            return tagDifference.Count != 0 ? tagDifference : null;
        }

        private IFileHandler _fileHandler;
    }
}