using FileLib;
using System;
using System.Collections.Generic;
using System.IO;

namespace CommandCreation
{
    internal class ChangeTagsCommand : Command
    {
        private readonly IMp3File _mp3File;
        private readonly MaskParser _maskParser;
    
        public ChangeTagsCommand(IMp3File mp3File, string mask)
        {
            _mp3File = mp3File;
            _maskParser = new MaskParser(mask);
        }

        public override void Execute()
        {
            ChangeTags();
            _mp3File.Save();
        }

        private void ChangeTags()
        {
            var fileName = Path.GetFileNameWithoutExtension(_mp3File.FullName);

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

                ChangeTag(tagPatternsInMask[i], tagValueInFileName);

                fileName = fileName.Remove(0, indexOfSplit + splitsInMask[i + 1].Length);
            }
        }

        private void ChangeTag(string tagPattern, string newTagValue)
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
                    _mp3File.Tags.Artist = newTagValue;
                    break;
                case TagNames.Title:
                    _mp3File.Tags.Title = newTagValue;
                    break;
                case TagNames.Genre:
                    _mp3File.Tags.Genre = newTagValue;
                    break;
                case TagNames.Album:
                    _mp3File.Tags.Album = newTagValue;
                    break;
                case TagNames.Track:
                    _mp3File.Tags.Track = Convert.ToUInt32(newTagValue);
                    break;
            }
        }
    }
}
