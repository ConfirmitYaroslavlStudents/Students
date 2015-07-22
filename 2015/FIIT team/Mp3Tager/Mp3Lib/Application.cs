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

        private string _commandName;
        private string _commandForHelp;
        private string _path;
        private string _pattern;
        private string _tag;
        private string _newTagValue;

        public void Execute(string[] args)
        {
            ArgumentParser.ParseArguments(args, out _commandName, out _commandForHelp,
                out _path, out _pattern, out _tag, out _newTagValue);
            switch (_commandName)
            {
                case "help":
                    ShowHelp(new ConsoleWriter());
                    break;

                case "rename":
                    Actions.Rename(new Mp3File(_path), _pattern);
                    break;

                case "changeTag":
                    Actions.ChangeTag(new Mp3File(_path), _tag, _newTagValue);
                    break;
            }
        }

        private void ShowHelp(IWriter writer)
        {
            if (_commandForHelp == null)
            {
                foreach (var message in HelpMessages)
                {
                    writer.Write(message.Key + ": ");
                    writer.WriteLine(message.Value);
                }
            }
            else
            {
                writer.WriteLine(HelpMessages.ContainsKey(_commandForHelp)
                    ? HelpMessages[_commandForHelp]
                    : "There is no such command!");
            }
        }
    }
}
