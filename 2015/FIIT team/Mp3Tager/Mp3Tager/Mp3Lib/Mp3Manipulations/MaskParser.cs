using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mp3Lib
{
    public class MaskParser
    {
        private string _mask;
        private List<string> _tags;
        private List<string> _splits;

        public List<string> GetTags()
        {
            return _tags;
        }

        public List<string> GetSplits()
        {
            return _splits;
        }

        public MaskParser(string mask)
        {
            _mask = mask;
            _tags = new List<string>();
            _splits = new List<string>();
            GetTagsAndSplits();
        }

        private void GetTagsAndSplits()
        {
            bool writeTag = false;

            var tag = new StringBuilder();
            var split = new StringBuilder();

            foreach (char c in _mask)
            {
                if (c != '{' && !writeTag)
                {
                    split.Append(c);
                }

                if (c != '{' && c != '}' && writeTag)
                {
                    tag.Append(c);
                }

                if (c == '{' && writeTag)
                {
                    split.Append(tag);
                    tag.Clear();
                    tag.Append(c);
                }

                if (c == '{' && !writeTag)
                {
                    tag.Append(c);
                    writeTag = true;
                }

                if (c == '}' && writeTag)
                {
                    tag.Append(c);
                    writeTag = false;
                    _tags.Add(tag.ToString());
                    tag.Clear();
                    _splits.Add(split.ToString());
                    split.Clear();
                }                
            }
            _splits.Add(split.ToString() + tag.ToString());
        }

        public string GetMaskFromTagsAndSplits()
        {
            var mask = new StringBuilder();
            int i = 0;
            while (i < _tags.Count)
            {
                mask.Append(_splits[i]);
                mask.Append(_tags[i]);
                i++;
            }
            mask.Append(_splits[i]);
            return mask.ToString();
        }

        /*public bool IsEqualNumberOfSplitsInMaskAndFileName(string fileName)
        {
            if (_splits.Any(split => split != String.Empty && CountSplitsInFileName(split, fileName) != CountSplitsInMask(split)))
            {
                return false;
            }
            return true;
        }

        private int CountSplitsInMask(string necessarySplit)
        {
            return _splits.Count(split => split == necessarySplit);
        }

        private int CountSplitsInFileName(string necessarySplit, string fileName)
        {
            int count = 0;
            var current = fileName;
            int finish = current.IndexOf(necessarySplit, StringComparison.Ordinal);

            while (finish != -1)
            {
                count++;
                current = current.Remove(0, finish + 1);
                finish = current.IndexOf(necessarySplit, StringComparison.Ordinal);

            }
            return count;
        }*/

        public bool IsEqualNumberOfSplitsInMaskAndFileName(string split, IEnumerable<string> splitsInMask, string fileName)
        {
            return CountSplitsInFileName(split, fileName) == CountSplitsInMask(split, splitsInMask);

        }

        private int CountSplitsInMask(string necessarySplit, IEnumerable<string> splits)
        {
            int count = 0;
            foreach (var split in splits)
            {
                if (split == necessarySplit)
                    count++;
            }
            return count;
        }

        private int CountSplitsInFileName(string necessarySplit, string fileName)
        {
            int count = 0;
            var current = fileName;
            int finish = current.IndexOf(necessarySplit);

            while (finish != -1)
            {
                count++;
                current = current.Remove(0, finish + 1);
                finish = current.IndexOf(necessarySplit);

            }
            return count;
        }
    }

}
