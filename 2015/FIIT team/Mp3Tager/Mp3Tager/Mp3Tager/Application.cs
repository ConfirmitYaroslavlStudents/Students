using System.Collections.Generic;
using Mp3Lib;

namespace Mp3Tager
{
    internal class Application
    {
        private readonly Dictionary<string, string> HelpMessages = new Dictionary<string, string>
        {
            {"help", ""},
            {"rename", @"<path> <pattern>"},
            {"changeTag", @"<path> <tag> <newTagValue>"}
        };

        private Dictionary<string, string> _parsedArgs;
        private Actions _actions;

        internal Application ()
        {
            _actions = new Actions();
        }

        public void Execute(string[] args)
        {
            var parser = new ArgumentParser();
            _parsedArgs = parser.ParseArguments(args);
            switch (_parsedArgs["commandName"])
            {
                case "help":
                    ShowHelp(new ConsoleWriter());
                    break;

                case "rename":
                    _actions.Rename(new Mp3File(_parsedArgs["path"]), _parsedArgs["pattern"]);
                    break;

                case "changeTag":
                    _actions.ChangeTag(new Mp3File(_parsedArgs["path"]), _parsedArgs["tag"], _parsedArgs["newTagValue"]);
                    break;
            }
        }

        private void ShowHelp(IWriter writer)
        {
            if (!_parsedArgs.ContainsKey("commandForHelp"))
            {
                foreach (var message in HelpMessages)
                {
                    writer.Write(message.Key + ": ");
                    writer.WriteLine(message.Value);
                }
            }
            else
            {
                writer.WriteLine(HelpMessages.ContainsKey(_parsedArgs["commandForHelp"])
                    ? HelpMessages[_parsedArgs["commandForHelp"]]
                    : "There is no such command!");
            }
        }
    }
}
