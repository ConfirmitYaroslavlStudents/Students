using System.Collections.Generic;

namespace Mp3Lib
{
    public class Application
    {
        private static readonly Dictionary<string, string> HelpMessages = new Dictionary<string, string>
        {
            {"help", ""},
            {"rename", @"<path> <pattern>"},
            {"changeTag", @"<path> <tag> <newTagValue>"}
        };

        private Dictionary<string, string> _parsedArgs; 

        public void Execute(string[] args)
        {
            _parsedArgs = ArgumentParser.ParseArguments(args);
            switch (_parsedArgs["commandName"])
            {
                case "help":
                    ShowHelp(new ConsoleWriter());
                    break;

                case "rename":
                    Actions.Rename(new Mp3File(_parsedArgs["path"]), _parsedArgs["pattern"]);
                    break;

                case "changeTag":
                    Actions.ChangeTag(new Mp3File(_parsedArgs["path"]), _parsedArgs["tag"], _parsedArgs["newTagValue"]);
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
