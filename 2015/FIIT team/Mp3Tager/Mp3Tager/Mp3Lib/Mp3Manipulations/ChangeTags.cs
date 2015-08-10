using System;
using System.Collections.Generic;
using System.IO;

namespace Mp3Lib
{    
    public partial class Mp3Manipulations
    {
        public void ChangeTags(string mask)
        {
            var parser = new MaskParser(mask);
            var splits = parser.GetSplits();
            var tags = parser.GetTags();
            var fileName = Path.GetFileNameWithoutExtension(_mp3File.Path);

            int finish;
            string tagValue;

            if (tags.Count == 0)
                return;

            for (int i = 0; i < tags.Count - 1; i++)
            {
                if (splits[i + 1] == String.Empty)
                {
                    throw new InvalidDataException("Ambiguous matching of mask and file name.");
                }
                if (i != 0 || (i == 0 && splits[i] != String.Empty))
                {
                    if (!fileName.StartsWith(splits[i]))
                    {
                        throw new InvalidDataException("Mask doesn't match the file name.");
                    }
                }
                finish = fileName.IndexOf(splits[i + 1], StringComparison.Ordinal);

                tagValue = fileName.Substring(splits[i].Length, finish - splits[i].Length);
                ChangeTag(tags[i], tagValue);

                fileName = fileName.Remove(0, splits[i].Length + tagValue.Length);
            }

            if (splits[splits.Count - 2] == String.Empty)
            {
                throw new InvalidDataException("Ambiguous matching of mask and file name.");
            }
            if (!fileName.StartsWith(splits[splits.Count - 2]))
            {
                throw new InvalidDataException("Mask doesn't match the file name.");
            }
            if (splits[splits.Count - 1] != String.Empty)
            {
                finish = fileName.IndexOf(splits[splits.Count - 1], StringComparison.Ordinal);
                if (!fileName.EndsWith(splits[splits.Count - 1]))
                {
                    throw new InvalidDataException("Mask doesn't match the file name.");
                }
            }
            else
            {
                finish = fileName.Length;
            }

            tagValue = fileName.Substring(splits[splits.Count - 2].Length, finish - splits[splits.Count - 2].Length);
            ChangeTag(tags[tags.Count - 1], tagValue);

            _mp3File.Save();
        }
        
              
        private void ChangeTag(string tag, string newTagValue)
        {
            var tagSet = new HashSet<string>
            {
                TagPatterns.Artist,
                TagPatterns.Album,
                TagPatterns.Genre,
                TagPatterns.Title,
                TagPatterns.Track
            };

            if (!tagSet.Contains(tag))
                throw new ArgumentException("There is no such tag.");

            switch (tag)
            {
                case TagPatterns.Artist:
                    _mp3File.Mp3Tags.Artist = newTagValue;
                    break;
                case TagPatterns.Title:
                    _mp3File.Mp3Tags.Title = newTagValue;
                    break;
                case TagPatterns.Genre:
                    _mp3File.Mp3Tags.Genre = newTagValue;
                    break;
                case TagPatterns.Album:
                    _mp3File.Mp3Tags.Album = newTagValue;
                    break;
                case TagPatterns.Track:
                    _mp3File.Mp3Tags.Track = Convert.ToUInt32(newTagValue);
                    break;
            }
        }

    }
}
