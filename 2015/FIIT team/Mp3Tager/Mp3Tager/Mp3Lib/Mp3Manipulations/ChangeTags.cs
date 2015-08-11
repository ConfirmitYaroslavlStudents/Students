using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Mp3Lib
{
    public partial class Mp3Manipulations
    {
        private List<string> _splits;
        private List<string> _tags;
        private string _fileName;       
        private MaskParser _parser;

        public void ChangeTags(string mask)
        {
            _parser = new MaskParser(mask);
            _splits = _parser.GetSplits();
            _tags = _parser.GetTags();        
            _fileName = Path.GetFileNameWithoutExtension(_mp3File.Path);

            int finish;
            string tagValue;

            if (_tags.Count == 0)
                return;

            if (_splits.Any(split => split != String.Empty && !_parser.IsEqualNumberOfSplitsInMaskAndFileName(split, _fileName)))
            {
                throw new InvalidDataException();
            }

            for (int i = 0; i < _tags.Count - 1; i++)
            {
                if (_splits[i + 1] == String.Empty)
                {
                    throw new InvalidDataException("Ambiguous matching of mask and file name.");
                }
                if (i != 0 || (i == 0 && _splits[i] != String.Empty))
                {
                    if (!_fileName.StartsWith(_splits[i]))
                    {
                        throw new InvalidDataException("Mask doesn't match the file name.");
                    }
                }
                finish = _fileName.IndexOf(_splits[i + 1], 1, StringComparison.Ordinal);


                if (finish == -1)
                {
                    throw new InvalidDataException("Mask doesn't match the file name.");
                }

                tagValue = _fileName.Substring(_splits[i].Length, finish - _splits[i].Length);
                ChangeTag(_tags[i], tagValue);

                _fileName = _fileName.Remove(0, _splits[i].Length + tagValue.Length);
            }
            
            if (_splits.Count - 2 != 0 || ((_splits.Count - 2) == 0 && _splits[_splits.Count - 2] != String.Empty))
            {
                if (!_fileName.StartsWith(_splits[_splits.Count - 2]))
                {
                    throw new InvalidDataException("Mask doesn't match the file name.");
                }
            }
            if (_splits[_splits.Count - 1] != String.Empty)
            {
                finish = _fileName.IndexOf(_splits[_splits.Count - 1], 1, StringComparison.Ordinal);
                if (!_fileName.EndsWith(_splits[_splits.Count - 1]))
                {
                    throw new InvalidDataException("Mask doesn't match the file name.");
                }
            }
            else
            {
                finish = _fileName.Length;
            }

            tagValue = _fileName.Substring(_splits[_splits.Count - 2].Length, finish - _splits[_splits.Count - 2].Length);
            ChangeTag(_tags[_tags.Count - 1], tagValue);

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
