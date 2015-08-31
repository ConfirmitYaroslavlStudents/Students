using System;
using System.Collections.Generic;
using System.IO;
using FileLib;

namespace CommandCreation
{
    public class ChangeTagsCommand : Command
    {
        internal IMp3File File;
        private readonly MaskParser _maskParser;
        internal Mp3Tags OldTags;

        public ChangeTagsCommand(IMp3File mp3File, string mask)
        {
            File = mp3File;
            _maskParser = new MaskParser(mask);
            OldTags = new Mp3Tags();
            File.Tags.CopyTo(OldTags);
        }

        public override void Execute()
        {
            ChangeTags();
        }

        public override void Undo()
        {
            File.Tags.Album = OldTags.Album;
            File.Tags.Artist = OldTags.Artist;
            File.Tags.Genre = OldTags.Genre;
            File.Tags.Title = OldTags.Title;
            File.Tags.Track = OldTags.Track;
        }

        private void ChangeTags()
        {
            var fileName = Path.GetFileNameWithoutExtension(File.FullName);

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
                    File.Tags.Artist = newTagValue;
                    break;
                case TagNames.Title:
                    File.Tags.Title = newTagValue;
                    break;
                case TagNames.Genre:
                    File.Tags.Genre = newTagValue;
                    break;
                case TagNames.Album:
                    File.Tags.Album = newTagValue;
                    break;
                case TagNames.Track:
                    File.Tags.Track = Convert.ToUInt32(newTagValue);
                    break;
            }
        }
        
        public override T Accept<T>(ICommandVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override bool IsPlanningCommand()
        {
            return true;
        }
    }
}
