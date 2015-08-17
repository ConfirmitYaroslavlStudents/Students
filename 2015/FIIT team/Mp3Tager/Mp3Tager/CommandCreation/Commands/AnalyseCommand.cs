using System.IO;
using System.Text;
using System;
using System.Linq;
using FileLib;

namespace CommandCreation
{
    class AnalyseCommand : Command
    {
        private readonly ISource _source;
        private readonly IWriter _writer;
        private readonly MaskParser _maskParser;

        public AnalyseCommand(ISource source, string mask, IWriter writer)
        {
            _source = source;
            _writer = writer;
            _maskParser = new MaskParser(mask);
        }

        public override void Execute()
        {
            var files = _source.GetFiles();
            var index = 1;
            var count = files.Count();
            foreach (var file in files)
            {
                try
                {
                    _writer.Write(Analyse(file, index++, count));
                }
                catch (InvalidDataException e)
                {
                    _writer.WriteLine(e.Message + "for file " + file.FullName);
                }
            }
        }

        private string Analyse(IMp3File mp3File, int index, int count)
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

                if (tagPatternsInMask[i] == "{index}")
                {
                    if (tagValueInFileName != ConvertToIndexForm(index, count))
                        resultMessage.Append("Expected index: " + ConvertToIndexForm(index, count) + "\n");
                }
                else
                {
                    var tagValueInTags = GetTagValueByTagPattern(mp3File, tagPatternsInMask[i]);

                    if (tagValueInFileName != tagValueInTags)
                    {
                        resultMessage.Append(_maskParser.GetTags()[i] + " in file name: " + tagValueInFileName + "; ");
                        resultMessage.Append(_maskParser.GetTags()[i] + " in tags: " + tagValueInTags + "\n");
                    }
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

        private string ConvertToIndexForm(int index, int count)
        {
            var maxDigits = 1 + Math.Floor(Math.Log10(count));
            var currentDigits = 1 + Math.Floor(Math.Log10(index));

            var convertedIndex = new StringBuilder(index.ToString());
            for (var i = 0; i < maxDigits - currentDigits; i++)
                convertedIndex.Insert(0, "0");

            return convertedIndex.ToString();
        }

        private string GetTagValueByTagPattern(IMp3File mp3File, string tagPattern)
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
