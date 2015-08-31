using System;
using System.IO;
using System.Text;
using FileLib;

namespace CommandCreation
{
    public class AnalyseCommand : Command
    {
        internal readonly IMp3File File;
        private readonly IWorker _worker;
        private readonly MaskParser _maskParser;

        public AnalyseCommand(IMp3File mp3File, string mask, IWorker worker)
        {
            File = mp3File;
            _worker = worker;
            _maskParser = new MaskParser(mask);
        }

        public override void Execute()
        {
            try
            {
                _worker.Write(Analyse(File));
            }
            catch (InvalidDataException e)
            {
                _worker.WriteLine(e.Message + " for file " + File.FullName);
            }
        }

        public override void Undo()
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

        public override T Accept<T>(ICommandVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override bool IsPlanningCommand()
        {
            return false;
        }
    }
}
