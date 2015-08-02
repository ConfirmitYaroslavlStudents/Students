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

        protected override sealed int[] NumberOfArguments { get; set; }
        protected override sealed string CommandName { get; set; }

        public HelpCommand(string[] args)
        {
            NumberOfArguments = new[] { 1, 2 };
            CommandName = CommandNames.Help;
            CheckIfCanBeExecuted(args);
            _commandForHelp = args.Length == 2 ? args[1] : null;
        }

        public override void Execute()
        {
            if (_commandForHelp == null)
            {
                foreach (var message in HelpMessages)
                {
                    Console.Write(message.Key + ": ");
                    Console.WriteLine(message.Value);
                }
            }
            else
            {
                Console.WriteLine(HelpMessages.ContainsKey(_commandForHelp)
                    ? HelpMessages[_commandForHelp]
                    : "There is no such command!");
            }
        }
    }
}
