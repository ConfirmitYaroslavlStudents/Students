using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCreation
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

        public bool IsEqualNumberOfSplitsInMaskAndFileName(string currentSplit, string fileName)
        {
            return CountSplitsInMask(currentSplit) == CountSplitsInFileName(currentSplit, fileName);
        }

        private int CountSplitsInMask(string currentSplit)
        {
            int count = 0;
            foreach (var split in _splits)
            {
                if (split == currentSplit)
                    count++;
            }
            return count;
        }

        private int CountSplitsInFileName(string currentSplit, string fileName)
        {
            int count = 0;
            var current = fileName;
            int finish = current.IndexOf(currentSplit);

            while (finish != -1)
            {
                count++;
                current = current.Remove(0, finish + 1);
                finish = current.IndexOf(currentSplit);

            }
            return count;
        }
    }

}
