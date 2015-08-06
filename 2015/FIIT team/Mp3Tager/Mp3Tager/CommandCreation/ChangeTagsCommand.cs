using System;
using System.Collections.Generic;
using System.IO;
using Mp3Lib;

namespace CommandCreation
{
    internal class ChangeTagsCommand : Command
    {
        
        private readonly HashSet<string> TagSet = new HashSet<string>
        {
            TagNames.Artist,
            TagNames.Album,
            TagNames.Genre,
            TagNames.Title,
            TagNames.Track
        };

        private string _path;
        private string _mask;

        protected override sealed int[] NumberOfArguments { get; set; }
        public override sealed string CommandName { get; protected set; }

        public ChangeTagsCommand(string[] args)
        {
            NumberOfArguments = new[] {3};
            CommandName = CommandNames.ChangeTags;
            CheckIfCanBeExecuted(args);
            _path = args[1];
            _mask = args[2];
        }

        public override void Execute()
        {
            var audioFile = new Mp3File(_path);
            ChangeTags(audioFile, _mask);
            audioFile.Save();
        }

        private void ChangeTags(IMp3File audioFile, string mask)
        {
            var fileName = new FileInfo(audioFile.Path).Name;
            var splits = GetSplits(mask);
            var tags = GetTags(mask);
            for (int i = 0; i < splits.Count; i++)
            {
                var index = fileName.IndexOf(splits[i], StringComparison.Ordinal);
                var value = fileName.Substring(0, index);
                fileName = fileName.Remove(0, index + splits[i].Length);
                ChangeTag(audioFile, tags[i], value);
            }
            ChangeTag(audioFile, tags[tags.Count - 1], fileName);
        }

        private List<string> GetSplits(string mask)
        {
            var splits = new List<string>();
            int start = 0;
            int finish = 0;

            while (true)
            {
                start = mask.IndexOf('}', finish) + 1;
                finish = mask.IndexOf('{', start);
                if (start != -1 && finish != -1)
                    splits.Add(mask.Substring(start, finish - start));
                else
                    break;
            }

            return splits;
        }

        private List<string> GetTags(string mask)
        {
            var tags = new List<string>();
            int start = 0;
            int finish = 0;

            while (true)
            {
                start = mask.IndexOf('{', finish);
                finish = mask.IndexOf('}', start != -1 ? start : finish) + 1;
                if (start != -1 && finish != -1)
                    tags.Add(mask.Substring(start, finish - start));
                else
                    break;
            }

            return tags;
        }

        internal void ChangeTag(IMp3File audioFile, string tag, string newTagValue)
        {
            if (!TagSet.Contains(tag))
                throw new ArgumentException("There is no such tag.");

            switch (tag)
            {
                case TagNames.Artist:
                    audioFile.Tag.Performers = new[] { newTagValue };
                    break;
                case TagNames.Title:
                    audioFile.Tag.Title = newTagValue;
                    break;
                case TagNames.Genre:
                    audioFile.Tag.Genres = new[] { newTagValue };
                    break;
                case TagNames.Album:
                    audioFile.Tag.Album = newTagValue;
                    break;
                case TagNames.Track:
                    audioFile.Tag.Track = Convert.ToUInt32(newTagValue);
                    break;
            }
        }
    }
}
