using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            var current = (string)fileName.Clone();
            int finish = current.IndexOf(currentSplit);

            while (finish != -1)
            {
                count++;
                current = current.Remove(0, finish + 1);
                finish = current.IndexOf(currentSplit);

            }
            return count;
        }

        public bool ValidateFileName(string fileName)
        {
            // Ambiguous matching of mask and file name.
            for (var i = 1; i < _splits.Count - 1; i++)
            {
                if (_splits[i] == String.Empty)
                    throw new InvalidDataException("Ambiguous matching of mask and file name");
            }
            // Ambiguous matching of mask and file name.
            if (
                _splits.Any(
                    split => split != String.Empty && CountSplitsInMask(split) < CountSplitsInFileName(split, fileName)))
            {
                throw new InvalidDataException("Ambiguous matching of mask and file name");
            }

            // Validation with regex
            var regexPattern = new StringBuilder();
            for (var i = 0; i < _splits.Count; i++)
            {
                regexPattern.Append(ConvertForRegex(_splits[i])
                    + (i != _splits.Count - 1 ? ".*" : "$"));
            }

            var regex = new Regex(regexPattern.ToString());
            return regex.IsMatch(@fileName);
        }

        private string ConvertForRegex(string split)
        {
            //'.', '\\', '+', '*', '[', ']', '{', '}', '|', '(', ')', '?', '^', '$'
            var splitForRegex  = new StringBuilder();
            foreach (var c in split)
            {
                switch (c)
                {
                    case '.':
                        splitForRegex.Append(@"\.");
                        break;
                    case '\\':
                        splitForRegex.Append(@"\\");
                        break;
                    case '+':
                        splitForRegex.Append(@"\+");
                        break;
                    case '*':
                        splitForRegex.Append(@"\*");
                        break;
                    case '[':
                        splitForRegex.Append(@"\[");
                        break;
                    case ']':
                        splitForRegex.Append(@"\]");
                        break;
                    case '{':
                        splitForRegex.Append(@"\{");
                        break;
                    case '}':
                        splitForRegex.Append(@"\}");
                        break;
                    case '|':
                        splitForRegex.Append(@"\|");
                        break;
                    case '(':
                        splitForRegex.Append(@"\)");
                        break;
                    case ')':
                        splitForRegex.Append(@"\)");
                        break;
                    case '?':
                        splitForRegex.Append(@"\?");
                        break;
                    case '^':
                        splitForRegex.Append(@"\^");
                        break;
                    case '$':
                        splitForRegex.Append(@"\$");
                        break;

                    default:
                        splitForRegex.Append(c);
                        break;
                }
            }

            return splitForRegex.ToString();
        }
    }

}
