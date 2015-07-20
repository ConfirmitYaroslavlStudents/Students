using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace Mp3Library
{
    public class Mp3Lib
    {
        private readonly Dictionary<string, int[]> _commandList = new Dictionary<string, int[]>
        {
            {"help",  new [] {1, 2}},
            {"rename", new [] {3}},
            {"changeTag", new [] {4}}
        };

        private readonly HashSet<string> _tagSet = new HashSet<string>
        { "artist", "title", "genre", "album", "track" };

        private readonly string[] _args;
        
        public Mp3Lib(string[] args)
        {
            _args = args;
        }

        public void ExecuteCommand()
        {
            try
            {
                string command = _args[0];
                if (CheckArgs(command))
                {
                    switch (command)
                    {
                        case "help":
                            ShowHelp();
                            break;

                        case "rename":
                            Rename(_args);
                            break;

                        case "changeTag":
                            ChangeTag(_args);
                            break;
                    }
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            
        }

        public virtual bool CheckArgs(string commandName)
        {
            if (_commandList.Keys.Contains(commandName))
                if (_commandList[commandName].Contains(_args.Length))
                    return true;
            return false;
        }

        public virtual void ShowHelp()
        {
            Helper.ShowInstructions(_args);
        }

        public virtual void Rename(string[] args)
        {
            var filePath = args[1];
            var pattern = args[2];

            var file = new FileInfo(filePath);
            if (!file.Exists)
                throw new FileNotFoundException("File not found", filePath);

            var mp3 = TagLib.File.Create(filePath);

            file.MoveTo(file.DirectoryName + @"\" + GetNewNameByPattern(pattern, mp3) + ".mp3");
        }

        private string GetNewNameByPattern(string pattern, TagLib.File mp3)
        {
            var s = new StringBuilder(pattern);
            s.Replace("{artist}", mp3.Tag.FirstPerformer);
            s.Replace("{title}", mp3.Tag.Title);
            s.Replace("{genre}", mp3.Tag.FirstGenre);
            s.Replace("{album}", mp3.Tag.Album);
            s.Replace("{track}", mp3.Tag.Track.ToString());
            return s.ToString();
        }

        public virtual void ChangeTag(string[] args)
        {
            var filePath = args[1];
            var tagType = args[2];
            var tagValue = args[3];

            var file = new FileInfo(filePath);
            if (!file.Exists)
                throw new FileNotFoundException("File not found", filePath);

            if (!_tagSet.Contains(tagType))
                throw new ArgumentException("There is no such tag");
            
            var audioFile = TagLib.File.Create(filePath);
            switch (tagType)
            {
                case "artist":
                    audioFile.Tag.Performers = new []{tagValue}; 
                    break;
                case "title":
                    audioFile.Tag.Title = tagValue;
                    break;
                case "genre":
                    audioFile.Tag.Genres =  new []{tagValue};
                    break;
                case "album":
                    audioFile.Tag.Album = tagValue;
                    break;
                case "track":
                    audioFile.Tag.Track = Convert.ToUInt32(tagValue);
                    break;
            }
            audioFile.Save();
        }
    }
}
