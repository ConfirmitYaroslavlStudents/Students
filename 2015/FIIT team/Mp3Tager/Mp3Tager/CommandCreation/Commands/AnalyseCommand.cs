using System.IO;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using FileLib;

namespace CommandCreation
{
    class AnalyseCommand : Command
    {
        private readonly IEnumerable<IMp3File> _mp3Files;
        private readonly MaskParser _maskParser;
        private Dictionary<string, List<string>> _indexDict;

        public AnalyseCommand(IEnumerable<IMp3File> mp3Files, string mask)
        {
            _mp3Files = mp3Files;
            _maskParser = new MaskParser(mask);

            _indexDict = new Dictionary<string, List<string>>();
        }

        public override string Execute()
        {
            var resultMessage = new StringBuilder();
            foreach (var mp3File in _mp3Files)
            {
                try
                {
                    resultMessage.Append(Analyse(mp3File));
                }
                catch (InvalidDataException e)
                {
                    resultMessage.Append(e.Message + " for file " + mp3File.FullName);
                }
            }
            if (_indexDict.Count > 0)
                resultMessage.Append(IndexAnalyse());
            return resultMessage.ToString();
        }
        
        protected override void SetIfShouldBeCompleted()
        {
            ShouldBeCompleted = false;
        }

        public override void Complete()
        {
        }

        private string Analyse(IMp3File mp3File)
        {
            var fileName = Path.GetFileNameWithoutExtension(mp3File.FullName);

            if (!_maskParser.ValidateFileName(fileName))
                throw new InvalidDataException("Mask doesn't match the file name.");

            var resultMessage = new StringBuilder();

            var tagPatternsInMask = _maskParser.GetTags();
            var splitsInMask = _maskParser.GetSplits();

            fileName = fileName.Remove(0, splitsInMask[0].Length);
            for (var i = 0; i < splitsInMask.Count - 1; i++)
            {
                var indexOfSplit = splitsInMask[i + 1] != String.Empty
                    ? fileName.IndexOf(splitsInMask[i + 1], StringComparison.Ordinal)
                    : fileName.Length;

                var tagValueInFileName = fileName.Substring(0, indexOfSplit);

                // analyse index logic starts here
                if (tagPatternsInMask[i] == "{index}")
                {
                    IndexAdd(fileName, tagValueInFileName);
                    fileName = fileName.Remove(0, indexOfSplit + splitsInMask[i + 1].Length);
                    continue;
                }

                var tagValueInTags = GetTagValueByTagPattern(mp3File, tagPatternsInMask[i]);

                if (tagValueInFileName != tagValueInTags)
                {
                    resultMessage.Append(_maskParser.GetTags()[i] + " in file name: " + tagValueInFileName + "; ");
                    resultMessage.Append(_maskParser.GetTags()[i] + " in tags: " + tagValueInTags + "\n");
                }

                fileName = fileName.Remove(0, indexOfSplit + splitsInMask[i + 1].Length);
            }

            // Add file name to message
            if (resultMessage.Length > 0)
            {
                resultMessage.Insert(0, "File: " + mp3File.FullName + "\n");
                resultMessage.Append("\n");
            }

            return resultMessage.ToString();
        }

        private void IndexAdd(string filename, string index)
        {
            if (!_indexDict.Keys.Contains(index))
                _indexDict[index] = new List<string>();

            _indexDict[index].Add(filename);
        }

        private string SameOrInvalidIndexAnalyse()
        {
            var resultMessage = new StringBuilder();
            var count = _indexDict.Keys.SelectMany(key => _indexDict[key]).Count();

            int i;
            foreach (string key in _indexDict.Keys)
            {
                if (!Int32.TryParse(key, out i))
                {
                    resultMessage.Append("Index must be number\n");
                    AddInfoInMessage(key, resultMessage);
                    continue;
                }

                if (i > count)
                {
                    resultMessage.Append("Index out of range. Maximum is " + count + ":\n");
                    AddInfoInMessage(key, resultMessage);
                    continue;
                }

                if (_indexDict[key].Count > 1)
                {
                    resultMessage.Append("More than one item with index " + key + ":\n");
                    AddInfoInMessage(key, resultMessage);
                    continue;
                }

                if (key.StartsWith("0") && key.Length > count.ToString().Length)
                {
                    var expectedIndex = ConvertToIndexForm(i, count);
                    resultMessage.Append("Wrong  index, expected " + expectedIndex + ":\n");
                    AddInfoInMessage(key, resultMessage);
                }                
            }
            return resultMessage.ToString();
        }

        private void AddInfoInMessage(string key, StringBuilder resultString)
        {
            foreach (var item in _indexDict[key])
            {
                resultString.Append("\t" + item + "\n");
            }
            resultString.Append("\n");
        }

        private string OnlyIndexAnalyse()
        {
            var resultMessage = new StringBuilder();
            var indexes = new List<bool>();
            var count = _indexDict.Keys.SelectMany(key => _indexDict[key]).Count();

            for (int i = 0; i < count; i++)
            {
                indexes.Add(false);
            }

            for (int i = 1; i < count + 1; i++)
            {
                var expectedIndex = ConvertToIndexForm(i, count);
                foreach (var form in GetAllPossibleIndexForm(i, count))
                {
                    if (_indexDict.ContainsKey(form) && form != expectedIndex)
                    {
                        resultMessage.Append("Wrong  index, expected " + expectedIndex + ":\n");
                        AddInfoInMessage(form, resultMessage);
                    }                                              
                }
                
                if (_indexDict.ContainsKey(expectedIndex))
                {
                    indexes[i - 1] = true;
                }
            }

            if (indexes.Contains(false))
            {
                resultMessage.Append("Some indexes are missing: ");
                bool writing = false;
                int firstWrittenIndex = 0;
                for (int i = 0; i < count; i++)
                {
                    if (!indexes[i] && !writing)
                    {
                        writing = true;
                        resultMessage.Append(", " + ConvertToIndexForm(i + 1, count));
                        firstWrittenIndex = i;
                        continue;
                    }

                    if (indexes[i] && writing)
                    {
                        writing = false;
                        if (i - firstWrittenIndex > 1)
                            resultMessage.Append(" - " + ConvertToIndexForm(i, count));
                    }
                }
                if (writing && firstWrittenIndex < count - 1)
                    resultMessage.Append(" - " + ConvertToIndexForm(count, count));

                resultMessage.Remove(resultMessage.ToString().
                    IndexOf("Some indexes are missing: ") + 26, 
                    2);
                resultMessage.Append(".\n\n");
            }

            return resultMessage.ToString();            
        }

        private string ConvertToIndexForm(int index, int count)
        {
            var shouldStartWith = new StringBuilder();
            for (int j = 0; j < count.ToString().Length - index.ToString().Length; j++)
            {
                shouldStartWith.Append("0");
            }
            var expectedIndex = shouldStartWith.ToString() + index;
            return expectedIndex;
        }

        private List<string> GetAllPossibleIndexForm(int index, int count)
        {
            var forms = new List<string>();

            for (int i = 1; i <= count; i++ )
            {
                var currentForm = ConvertToIndexForm(index, i);
                if (!forms.Contains(currentForm))
                    forms.Add(currentForm);
            }

            return forms;
        }

        private string IndexAnalyse()
        {
            var resultMessage = new StringBuilder();

            resultMessage.Append(SameOrInvalidIndexAnalyse());
            resultMessage.Append(OnlyIndexAnalyse());

            return resultMessage.ToString();
        }

        private static string GetTagValueByTagPattern(IMp3File mp3File, string tagPattern)
        {
            switch (tagPattern)
            {
                case TagNames.Artist:
                    return mp3File.Tags.Artist;
                case TagNames.Title:
                    return mp3File.Tags.Title;
                case TagNames.Genre:
                    return mp3File.Tags.Genre;
                case TagNames.Album:
                    return mp3File.Tags.Album;
                case TagNames.Track:
                    return mp3File.Tags.Track.ToString();
                default:
                    throw new ArgumentException(tagPattern);
            }
        }
    }
}
