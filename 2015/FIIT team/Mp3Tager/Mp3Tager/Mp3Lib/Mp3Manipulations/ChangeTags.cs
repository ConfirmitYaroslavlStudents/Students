using System;
using System.Collections.Generic;
using System.IO;

namespace Mp3Lib
{
    public partial class Mp3Manipulations
    {
        public void ChangeTags(string mask)
        {
            var fileName = Path.GetFileNameWithoutExtension(_mp3File.Path);
            var splits = GetSplits(mask);
            var tags = GetTags(mask);
            for (int i = 0; i < splits.Count; i++)
            {
                var index = fileName.IndexOf(splits[i], StringComparison.Ordinal);
                var value = fileName.Substring(0, index);
                fileName = fileName.Remove(0, index + splits[i].Length);
                ChangeTag(tags[i], value);
            }
            ChangeTag(tags[tags.Count - 1], fileName);

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

        private List<string> GetSplits(string mask)
        {
            var splits = new List<string>();
            int start = 0;
            int finish = 0;

            while (true)
            {
                start = mask.IndexOf('}', finish) + 1;
                finish = mask.IndexOf('{', start);
                if (start != -1 && finish != -1)
                    splits.Add(mask.Substring(start, finish - start));
                else
                    break;
            }

            return splits;
        }

        private List<string> GetTags(string mask)
        {
            var tags = new List<string>();
            int start = 0;
            int finish = 0;

            while (true)
            {
                start = mask.IndexOf('{', finish);
                finish = mask.IndexOf('}', start != -1 ? start : finish) + 1;
                if (start != -1 && finish != -1)
                    tags.Add(mask.Substring(start, finish - start));
                else
                    break;
            }

            return tags;
        } 
    }
}
