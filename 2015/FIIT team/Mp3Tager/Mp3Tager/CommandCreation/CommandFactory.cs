using System;
using System.Collections.Generic;
using FileLib;
using TagLib;

namespace CommandCreation
{
    public class CommandFactory
    {
        public readonly Dictionary<string, int[]> NumberOfCommandArguments = new Dictionary<string, int[]>()
        {
            {CommandNames.Help, new[] {1, 2}},
            {CommandNames.Rename, new []{3} },
            {CommandNames.ChangeTags, new []{3}},
            {CommandNames.Analyse, new []{3}},
        };

        public Command ChooseCommand(string[] args, IWriter writer)
        {
            IMp3File file;
            ISource source;
            string mask;

            var parser = new ArgumentParser(args);
            parser.CheckForEmptiness();

            var commandName = args[0].ToLower();
            switch (commandName)
            {
                case CommandNames.Help:
                    parser.CheckIfCanBeExecuted(args, NumberOfCommandArguments[CommandNames.Help]);
                    return new HelpCommand(args, writer);

                case CommandNames.Rename:
                    parser.CheckIfCanBeExecuted(args, NumberOfCommandArguments[CommandNames.Rename]);
                    file = new Mp3File(TagLib.File.Create(args[1]), new FileExistenceChecker());
                    mask = args[2];
                    return new RenameCommand(file, new FileExistenceChecker(), mask);

                case CommandNames.ChangeTags:
                    parser.CheckIfCanBeExecuted(args, NumberOfCommandArguments[CommandNames.ChangeTags]);
                    file = new Mp3File(TagLib.File.Create(args[1]), new FileExistenceChecker());
                    mask = args[2];
                    return new ChangeTagsCommand(file, mask);

                case CommandNames.Analyse:
                    parser.CheckIfCanBeExecuted(args, NumberOfCommandArguments[CommandNames.Analyse]);
                    source = new FileSystemSource(args[1]);                    
                    mask = args[2];
                    return new AnalyseCommand(source, mask, writer);

                default:
                    throw new InvalidOperationException("Invalid operation: there is no such command!");
            }
        }
    }
}
