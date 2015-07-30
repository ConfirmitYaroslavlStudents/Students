using System;
using System.IO;

namespace CommandCreation
{
    public class CommandFactory
    {
        public Command ChooseCommand(string[] args)
        {
            CheckForEmtyness(args);
            var commandName = args[0];
            switch (commandName)
            {
                case CommandNames.Help:
                    return new HelpCommand(args);
                case CommandNames.Rename:
                    return new RenameCommand(args);
                default:
                    throw new InvalidDataException("There is no such command");
            }
        }

        private void CheckForEmtyness(string[] args)
        {
            if (args == null)
                throw new ArgumentNullException("args");
            if (args.Length == 0)
                throw new ArgumentException("You haven't passed any argument!");
        }
    }
}
