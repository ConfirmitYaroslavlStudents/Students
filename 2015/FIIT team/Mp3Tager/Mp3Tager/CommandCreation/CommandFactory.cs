using System;
using Mp3Lib;
using TagLib;

namespace CommandCreation
{
    public class CommandFactory
    {
        public Command ChooseCommand(string[] args)
        {
            IMp3File file;
            string mask;

            var parser = new ArgumentParser(args);
            parser.CheckForEmptiness();

            var commandName = args[0];
            switch (commandName)
            {
                case CommandNames.Help:
                    parser.CheckIfCanBeExecuted(args, HelpCommand.GetNumberOfArguments());
                    return new HelpCommand(args);

                case CommandNames.Rename:
                    parser.CheckIfCanBeExecuted(args, RenameCommand.GetNumberOfArguments());
                    file = new Mp3File(File.Create(args[1]));
                    mask = args[2];
                    return new RenameCommand(file, mask);

                case CommandNames.ChangeTags:
                    parser.CheckIfCanBeExecuted(args, ChangeTagsCommand.GetNumberOfArguments());
                    file = new Mp3File(File.Create(args[1]));
                    mask = args[2];
                    return new ChangeTagsCommand(file, mask);

                default:
                    throw new InvalidOperationException("Invalid operation: there is no such command!");
            }
        }
    }
}
