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
        private int _finish;
        private string _tagValue;

        public void ChangeTags(string mask)
        {
            SetFileName();
            if (!ParseTagsAndSplits(mask))
                return;
            ChangeAllTags();
            _mp3File.Save();
        }

        private void SetFileName()
        {
            _fileName = Path.GetFileNameWithoutExtension(_mp3File.Path);
        }

        private bool ParseTagsAndSplits(string mask)
        {
            var parser = new MaskParser(mask);
            _splits = parser.GetSplits();
            _tags = parser.GetTags();

            if (_tags.Count == 0)
                return false;

            if (_splits.Any(split => split != String.Empty && !parser.IsEqualNumberOfSplitsInMaskAndFileName(split, _fileName)))
                throw new InvalidDataException();

            return true;
        }

        private void ChangeAllTags()
        {
            for (int i = 0; i < _tags.Count - 1; i++)
            {
                CheckIfTagCanBeFound(i);
                SetTagValue(i);
                ChangeTag(GetTagAtIndex(i));
                _fileName = RemoveSetTag(i);
            }

            CheckIfLastTagCanBeFound();
            SetLastTagValue();
            ChangeTag(GetTagAtIndex(_tags.Count - 1));
        }

        private void CheckIfTagCanBeFound(int index)
        {
            if (IsSplitEmptyString(index + 1))
            {
                throw new InvalidDataException("Ambiguous matching of mask and file name.");
            }
            if (index != 0 || (index == 0 && !IsSplitEmptyString(index)))
            {
                CheckIfRigthSplitInTheBeginning(index);
            }
        }

        private bool IsSplitEmptyString(int index)
        {
            return _splits[index] == String.Empty;
        }

        private void CheckIfRigthSplitInTheBeginning(int index)
        {
            if (!_fileName.StartsWith(_splits[index]))
            {
                throw new InvalidDataException("Mask doesn't match the file name.");
            }
        }

        private void SetTagValue(int index)
        {
            _finish = FindEndOfTag(index + 1);
            CheckFinish();
            _tagValue = FindTagAtIndex(index);
        }

        private void CheckIfRigthSplitInTheEnd(int index)
        {
            if (!_fileName.EndsWith(_splits[index]))
            {
                throw new InvalidDataException("Mask doesn't match the file name.");
            }
        }

        private int FindEndOfTag(int index)
        {
            return _fileName.IndexOf(_splits[index], 1, StringComparison.Ordinal);
        }

        private void CheckFinish()
        {
            if (_finish == -1)
            {
                throw new InvalidDataException("Mask doesn't match the file name.");
            }
        }

        private string FindTagAtIndex(int index)
        {
            return _fileName.Substring(_splits[index].Length, _finish - _splits[index].Length);
        }

        private void ChangeTag(string tag)
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
                    _mp3File.Mp3Tags.Artist = _tagValue;
                    break;
                case TagPatterns.Title:
                    _mp3File.Mp3Tags.Title = _tagValue;
                    break;
                case TagPatterns.Genre:
                    _mp3File.Mp3Tags.Genre = _tagValue;
                    break;
                case TagPatterns.Album:
                    _mp3File.Mp3Tags.Album = _tagValue;
                    break;
                case TagPatterns.Track:
                    _mp3File.Mp3Tags.Track = Convert.ToUInt32(_tagValue);
                    break;
            }
        }

        private string GetTagAtIndex(int index)
        {
            return _tags[index];
        }

        private string RemoveSetTag(int index)
        {
            return _fileName.Remove(0, _splits[index].Length + _tagValue.Length);
        }

        private void SetLastTagValue()
        {
            SetFinishForLastTag();
            _tagValue = FindTagAtIndex(_splits.Count - 2);
        }

        private void SetFinishForLastTag()
        {
            if (!IsSplitEmptyString(_splits.Count - 1))
            {
                CheckIfRigthSplitInTheEnd(_splits.Count - 1);
                _finish = FindEndOfTag(_splits.Count - 1);
            }
            else
            {
                _finish = _fileName.Length;
            }
        }

        private void CheckIfLastTagCanBeFound()
        {
            if (_splits.Count - 2 != 0 || ((_splits.Count - 2) == 0 && !IsSplitEmptyString(_splits.Count - 2)))
            {
                CheckIfRigthSplitInTheBeginning(_splits.Count - 2);
            }
        }
    }
}
