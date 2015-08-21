using FileLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CommandCreation
{
    // todo: *done* for folder
    internal class ChangeTagsCommand : Command
    {
        private readonly IEnumerable<IMp3File> _mp3Files;
        private readonly MaskParser _maskParser;
    
        public ChangeTagsCommand(IEnumerable<IMp3File> mp3Files, string mask)
        {
            _mp3Files = mp3Files;
            _maskParser = new MaskParser(mask);
        }

        public override string Execute()
        {
            var resultMessage = new StringBuilder();
            foreach (var mp3File in _mp3Files)
            {
                resultMessage.Append(ChangeTags(mp3File));
            }
            return resultMessage.ToString();
        }

        protected override void SetIfShouldBeCompleted()
        {
            ShouldBeCompleted = true;
        }

        public override void Complete()
        {
            foreach (var mp3File in _mp3Files)
            {
                mp3File.Save();
            }
        }

        private string ChangeTags(IMp3File mp3File)
        {
            var resultMessage = new StringBuilder();
            resultMessage.Append(mp3File.FullName + ":\n");

            var fileName = Path.GetFileNameWithoutExtension(mp3File.FullName);

            if (!_maskParser.ValidateFileName(fileName))
                throw new InvalidDataException("Mask doesn't match the file name.");

            var tagPatternsInMask = _maskParser.GetTags();
            var splitsInMask = _maskParser.GetSplits();

            fileName = fileName.Remove(0, splitsInMask[0].Length); // Remove first split
            for (var i = 0; i < splitsInMask.Count - 1; i++)
            {
                var indexOfSplit = splitsInMask[i + 1] != String.Empty
                    ? fileName.IndexOf(splitsInMask[i + 1], StringComparison.Ordinal)
                    : fileName.Length;
                var tagValueInFileName = fileName.Substring(0, indexOfSplit);

                resultMessage.Append(tagPatternsInMask[i] + " " + GetTagValueByTagPattern(mp3File, tagPatternsInMask[i])
                    + " ----> " + tagValueInFileName + "\n");
                ChangeTag(mp3File, tagPatternsInMask[i], tagValueInFileName);

                fileName = fileName.Remove(0, indexOfSplit + splitsInMask[i + 1].Length);
            }

            resultMessage.Append("\n");
            return resultMessage.ToString();
        }

        private static void ChangeTag(IMp3File mp3File, string tagPattern, string newTagValue)
        {
            var tagSet = new HashSet<string>
            {
                TagNames.Artist,
                TagNames.Album,
                TagNames.Genre,
                TagNames.Title,
                TagNames.Track
            };

            if (!tagSet.Contains(tagPattern))
                throw new ArgumentException("There is no such tag.");

            switch (tagPattern)
            {
                case TagNames.Artist:
                    mp3File.Tags.Artist = newTagValue;
                    break;
                case TagNames.Title:
                    mp3File.Tags.Title = newTagValue;
                    break;
                case TagNames.Genre:
                    mp3File.Tags.Genre = newTagValue;
                    break;
                case TagNames.Album:
                    mp3File.Tags.Album = newTagValue;
                    break;
                case TagNames.Track:
                    mp3File.Tags.Track = Convert.ToUInt32(newTagValue);
                    break;
            }
        }

        private static string GetTagValueByTagPattern(IMp3File mp3File, string tagPattern)
        {
            switch (tagPattern)
            {
                case TagNames.Artist:
                    return mp3File.Tags.Artist;
                case TagNames.Title:
                    return mp3File.Tags.Title;
                case TagNames.Genre:
                    return mp3File.Tags.Genre;
                case TagNames.Album:
                    return mp3File.Tags.Album;
                case TagNames.Track:
                    return mp3File.Tags.Track.ToString();
                default:
                    throw new ArgumentException(tagPattern);
            }
        }        
    }
}
