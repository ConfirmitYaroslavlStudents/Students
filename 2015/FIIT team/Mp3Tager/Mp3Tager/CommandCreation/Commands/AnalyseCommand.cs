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
        private IWriter _writer;
        private MaskParser _maskParser;

        public AnalyseCommand(ISource source, string mask, IWriter writer)
        {
            _source = source;
            _writer = writer;
            _maskParser = new MaskParser(mask);
        }

        public override void Execute()
        {
            foreach (var file in _source.GetFiles())
            {
                try
                {
                    _writer.Write(Analyse(file));
                }
                catch (InvalidDataException e)
                {
                    _writer.WriteLine(e.Message + "for file " + file.FullName);
                }
            }
        }

        private string Analyse(IMp3File _mp3File)
        {
            if (!_maskParser.ValidateFileName(_mp3File.FullName))
                throw new InvalidDataException("Mask doesn't match the file name.");

            var resultMessage = new StringBuilder();            
            var fileName = Path.GetFileNameWithoutExtension(_mp3File.FullName);
            int finish;
            string tagValue, tagValueReal;

            for (int i = 0; i < _maskParser.GetTags().Count - 1; i++)
            {
                finish = fileName.IndexOf(_maskParser.GetSplits()[i + 1], StringComparison.Ordinal);

                tagValue = fileName.Substring(_maskParser.GetSplits()[i].Length, finish - _maskParser.GetSplits()[i].Length);
                tagValueReal = GetTagValueByTagPattern(_mp3File, _maskParser.GetTags()[i]);

                if (tagValue != tagValueReal)
                {
                    resultMessage.Append(_maskParser.GetTags()[i] + " in file name: " + tagValue + "; ");
                    resultMessage.Append(_maskParser.GetTags()[i] + " in tags: " + tagValueReal + "\n");
                }

                fileName = fileName.Remove(0, _maskParser.GetSplits()[i].Length + tagValue.Length);
            }

            finish = _maskParser.GetSplits()[_maskParser.GetSplits().Count - 1] != String.Empty
                ? fileName.IndexOf(_maskParser.GetSplits()[_maskParser.GetSplits().Count - 1], StringComparison.Ordinal)
                : fileName.Length;

            tagValue = fileName.Substring(_maskParser.GetSplits()[_maskParser.GetSplits().Count - 2].Length, finish - _maskParser.GetSplits()[_maskParser.GetSplits().Count - 2].Length);
            tagValueReal = GetTagValueByTagPattern(_mp3File, _maskParser.GetTags()[_maskParser.GetTags().Count - 1]);

            if (tagValue != tagValueReal)
            {
                resultMessage.Append(_maskParser.GetTags()[_maskParser.GetTags().Count - 1] + " in file name: " + tagValue + "; ");
                resultMessage.Append(_maskParser.GetTags()[_maskParser.GetTags().Count - 1] + " in tags: " + tagValueReal + "\n");
            }

            if (resultMessage.ToString() != "")
            {
                resultMessage.Insert(0, "File: " + _mp3File.FullName + "\n");
                resultMessage.Append("\n");
            }
            return resultMessage.ToString();
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
