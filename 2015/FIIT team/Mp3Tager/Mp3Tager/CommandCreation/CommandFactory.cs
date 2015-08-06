using System;
using System.IO;

namespace CommandCreation
{
    public class CommandFactory
    {
        public Command ChooseCommand(string[] args)
        {
            var parser = new ArgumentParser(args);
            parser.CheckForEmptiness();
            var commandName = args[0];
            switch (commandName)
            {
                case CommandNames.Help:
                    return new HelpCommand(args);
                case CommandNames.Rename:
                    return new RenameCommand(args);
                case CommandNames.ChangeTags:
                    return new ChangeTagsCommand(args);
                default:
                    throw new InvalidOperationException("Invalid operation: there is no such command!");
            }
        }        
    }
}
