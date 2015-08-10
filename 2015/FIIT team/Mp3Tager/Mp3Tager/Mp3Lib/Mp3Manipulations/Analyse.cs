using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mp3Lib
{
    public partial class Mp3Manipulations
    {
        public string Analyse(string mask)
        {
            var parser = new MaskParser(mask);
            var splits = parser.GetSplits();
            var tags = parser.GetTags();
            var fileName = Path.GetFileNameWithoutExtension(_mp3File.Path);
            var resultMessage = new StringBuilder();

            int finish;
            string tagValue;
            string tagValueReal;

            if (tags.Count == 0)
                return String.Empty;

            //if (!parser.IsEqualNumberOfSplitsInMaskAndFileName(fileName))
             //   throw new InvalidDataException();
            if (splits.Any(split => split != String.Empty && !parser.IsEqualNumberOfSplitsInMaskAndFileName(split, splits, fileName)))
            {
                throw new InvalidDataException();
            }

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

                if (finish == -1)
                {
                    throw new InvalidDataException("Mask doesn't match the file name.");
                }

                tagValue = fileName.Substring(splits[i].Length, finish - splits[i].Length);

                tagValueReal = GetTagValueByTagPattern(tags[i]);
                if (tagValue != tagValueReal)
                {
                    resultMessage.Append(tags[i] + " in file name: " + tagValue + "; ");
                    resultMessage.Append(tags[i] + " in tags: " + tagValueReal);
                }

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
            tagValueReal = GetTagValueByTagPattern(tags[tags.Count - 1]);

            if (tagValue != tagValueReal)
            {
                resultMessage.Append(tags[tags.Count - 1] + " in file name: " + tagValue + "; ");
                resultMessage.Append(tags[tags.Count - 1] + " in tags: " + tagValueReal);
            }

            return resultMessage.ToString();
        }

        private string GetTagValueByTagPattern(string tagPattern)
        {
            switch (tagPattern)
            {
                case TagPatterns.Artist:
                    return _mp3File.Mp3Tags.Artist;
                case TagPatterns.Title:
                    return _mp3File.Mp3Tags.Title;
                case TagPatterns.Genre:
                    return _mp3File.Mp3Tags.Genre;
                case TagPatterns.Album:
                    return _mp3File.Mp3Tags.Album;
                case TagPatterns.Track:
                    return _mp3File.Mp3Tags.Track.ToString();
                default:
                    throw new ArgumentException(tagPattern);
            }
        }
    }
}
