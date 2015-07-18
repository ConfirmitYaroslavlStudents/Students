using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Mp3Lib
{
    public class Mp3Lib
    {
        private Dictionary<string, int[]> _commandList = new Dictionary<string, int[]>()
        {
            {"help",  new [] {1, 2}},
            {"rename", new [] {3}},
        };

        private string[] _args;


        public Mp3Lib(string[] args)
        {
            _args = args;
        }

        private bool CheckArgs(string commandName)
        {
            if (_commandList.Keys.Contains(commandName))
                if (_commandList[commandName].Contains(_args.Length))
                    return true;
            return false;
        }

        public void ShowHelp()
        {
            var helper = new Helper();
            helper.ShowInstructions();
        }

        public void ExecuteCommand()
        {

            string command = _args[0];
            CheckArgs(command);
          
            switch (command)
            {
                case "help" : ShowHelp();
                    break;

                case "rename" : Rename(_args);
                    break;
            }

        }

        private void Rename(string[] args)
        {
            var path = args[1];
            var pattern = args[2];

            var file = new FileInfo(path);
            if (!file.Exists)
                return;
            var mp3 = TagLib.File.Create(path);

            //file.MoveTo(file.DirectoryName + @"\" + GetNewNameByPattern(pattern, mp3) + ".mp3");
            Console.WriteLine(file.DirectoryName + @"\" + GetNewNameByPattern(pattern, mp3) + ".mp3");
            
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
    }
}
