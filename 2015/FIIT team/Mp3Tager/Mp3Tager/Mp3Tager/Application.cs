using System;
using System.Collections.Generic;
using Mp3Lib;

namespace Mp3Tager
{
    internal class Application
    {
        private static readonly Dictionary<string, string> HelpMessages = new Dictionary<string, string>
        {
            {"help", ""},
            {"rename", @"<path> <pattern>"},
            {"changeTag", @"<path> <tag> <newTagValue>"}
        };

        private ParsedArgs _parsedArgs; 
        private readonly Actions _actions;

        internal Application ()
        {
            _actions = new Actions();
        }

        public void Execute(string[] args)
        {
            var parser = new ArgumentParser();
            _parsedArgs = parser.ParseArguments(args);

            switch (_parsedArgs.CommandName)
            {
                case "help":
                    ShowHelp();
                    break;

                case "rename":
                    _actions.Rename(new Mp3File(_parsedArgs.Path), _parsedArgs.Pattern);
                    break;

                case "changeTag":
                    _actions.ChangeTag(new Mp3File(_parsedArgs.Path), _parsedArgs.Tag, _parsedArgs.NewTagValue);
                    break;
            }
        }

        private void ShowHelp()
        {
            if (_parsedArgs.CommandForHelp == null)
            {
                foreach (var message in HelpMessages)
                {
                    Console.Write(message.Key + ": ");
                    Console.WriteLine(message.Value);
                }
            }
            else
            {
                Console.WriteLine(HelpMessages.ContainsKey(_parsedArgs.CommandForHelp)
                    ? HelpMessages[_parsedArgs.CommandForHelp]
                    : "There is no such command!");
            }
        }
    }
}
