using System;
using System.Collections.Generic;

namespace CommandCreation
{
    internal class HelpCommand : Command
    {
        private static readonly Dictionary<string, string> HelpMessages = new Dictionary<string, string>
        {
            {CommandNames.Help, ""},
            {CommandNames.Rename, @"<path> <pattern>"},
            {CommandNames.ChangeTags, @"<path> <mask>"}
        };

        private readonly string _commandForHelp;
        private IWriter _writer;


        public HelpCommand(string[] args, IWriter writer)
        {
            _commandForHelp = args.Length == 2 ? args[1] : null;
            _writer = writer;
        }

        public override void Execute()
        {
            if (_commandForHelp == null)
            {
                foreach (var message in HelpMessages)
                {
                    _writer.WriteLine(message.Key + ": " + message.Value);                    
                }
            }
            else
            {
                _writer.WriteLine(HelpMessages.ContainsKey(_commandForHelp)
                    ? HelpMessages[_commandForHelp]
                    : "There is no such command!");
            }
        }
    }
}