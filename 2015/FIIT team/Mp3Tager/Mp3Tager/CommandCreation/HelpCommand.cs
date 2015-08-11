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

        public static int[] GetNumberOfArguments()
        {
            return new[] { 1, 2 };
        }
        public override string GetCommandName()
        {
            return CommandNames.Help;
        }

        public HelpCommand(string[] args)
        {
            _commandForHelp = args.Length == 2 ? args[1] : null;
        }

        public override void Execute()
        {
            if (_commandForHelp == null)
            {
                foreach (var message in HelpMessages)
                {
                    // todo: no console reference!
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