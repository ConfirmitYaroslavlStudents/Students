using System;
using System.Collections.Generic;
using System.IO;
using Mp3Lib;

namespace CommandCreation
{
    internal class ChangeTagsCommand : Command
    {
        private const string Artist = "{artist}";
        private const string Title = "{title}";
        private const string Genre = "{genre}";
        private const string Album = "{album}";
        private const string Track = "{track}";

        private readonly HashSet<string> TagSet = new HashSet<string>
        {
            Artist,
            Album,
            Genre,
            Title,
            Track
        };

        private string _path;
        private string _mask;

        protected override sealed int[] NumberOfArguments { get; set; }
        protected override sealed string CommandName { get; set; }

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

        private void ChangeTag(IMp3File audioFile, string tag, string newTagValue)
        {
            if (!TagSet.Contains(tag))
                throw new ArgumentException("There is no such tag.");

            switch (tag)
            {
                case Artist:
                    audioFile.Tag.Performers = new[] { newTagValue };
                    break;
                case Title:
                    audioFile.Tag.Title = newTagValue;
                    break;
                case Genre:
                    audioFile.Tag.Genres = new[] { newTagValue };
                    break;
                case Album:
                    audioFile.Tag.Album = newTagValue;
                    break;
                case Track:
                    audioFile.Tag.Track = Convert.ToUInt32(newTagValue);
                    break;
            }
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

    }
}
