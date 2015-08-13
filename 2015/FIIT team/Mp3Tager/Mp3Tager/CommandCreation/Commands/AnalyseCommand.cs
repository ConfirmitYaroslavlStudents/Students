using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System;
using FileLib;

namespace CommandCreation
{
    class AnalyseCommand : Command
    {        
        private ISource _source;
        private string _mask;
        private IWriter _writer;

        private List<string> _splits;
        private List<string> _tags;
        private string _fileName;

        private int _finish;
        private string _tagValue;
        private string _tagValueReal;

        public AnalyseCommand(ISource source, string mask, IWriter writer)
        {
            _source = source;
            _mask = mask;
            _writer = writer;
        }

        public override void Execute()
        {
            foreach (var file in _source.GetFiles())
            {
                _writer.WriteLine(Analyse(file));
            }
        }

        private string Analyse(IMp3File _mp3File)
        {
            var resultMessage = new StringBuilder();
            _fileName = Path.GetFileNameWithoutExtension(_mp3File.FullName);

            if (!ParseArgsAndSplits())
                return String.Empty;

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
                _finish = _fileName.IndexOf(_splits[i + 1], StringComparison.Ordinal);

                if (_finish == -1)
                {
                    throw new InvalidDataException("Mask doesn't match the file name.");
                }

                _tagValue = _fileName.Substring(_splits[i].Length, _finish - _splits[i].Length);

                _tagValueReal = GetTagValueByTagPattern(_mp3File, _tags[i]);
                if (_tagValue != _tagValueReal)
                {
                    resultMessage.Append(_tags[i] + " in file name: " + _tagValue + "; ");
                    resultMessage.Append(_tags[i] + " in tags: " + _tagValueReal);
                }

                _fileName = _fileName.Remove(0, _splits[i].Length + _tagValue.Length);
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
                _finish = _fileName.IndexOf(_splits[_splits.Count - 1], StringComparison.Ordinal);
                if (!_fileName.EndsWith(_splits[_splits.Count - 1]))
                {
                    throw new InvalidDataException("Mask doesn't match the file name.");
                }
            }
            else
            {
                _finish = _fileName.Length;
            }

            _tagValue = _fileName.Substring(_splits[_splits.Count - 2].Length, _finish - _splits[_splits.Count - 2].Length);
            _tagValueReal = GetTagValueByTagPattern(_mp3File, _tags[_tags.Count - 1]);

            if (_tagValue != _tagValueReal)
            {
                resultMessage.Append(_tags[_tags.Count - 1] + " in file name: " + _tagValue + "; ");
                resultMessage.Append(_tags[_tags.Count - 1] + " in tags: " + _tagValueReal);
            }

            return resultMessage.ToString();
        }

        private bool ParseArgsAndSplits()
        {
            var parser = new MaskParser(_mask);
            _splits = parser.GetSplits();
            _tags = parser.GetTags();


            if (_tags.Count == 0)
                return false;

            if (_splits.Any(split => split != String.Empty && !parser.IsEqualNumberOfSplitsInMaskAndFileName(split, _fileName)))
            {
                throw new InvalidDataException();
            }

            return true;
        }

        private string GetTagValueByTagPattern(IMp3File _mp3File, string tagPattern)
        {
            switch (tagPattern)
            {
                case TagNames.Artist:
                    return _mp3File.Tags.Artist;
                case TagNames.Title:
                    return _mp3File.Tags.Title;
                case TagNames.Genre:
                    return _mp3File.Tags.Genre;
                case TagNames.Album:
                    return _mp3File.Tags.Album;
                case TagNames.Track:
                    return _mp3File.Tags.Track.ToString();
                default:
                    throw new ArgumentException(tagPattern);
            }
        }
    }
}
