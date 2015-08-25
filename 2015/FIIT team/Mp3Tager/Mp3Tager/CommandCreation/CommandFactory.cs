using System;
using System.Collections.Generic;
using FileLib;
using TagLib;
using System.IO;

namespace CommandCreation
{
    // todo : add tests
    public class CommandFactory
    {
        public readonly Dictionary<string, int[]> NumberOfCommandArguments = new Dictionary<string, int[]>()
        {
            {CommandNames.Help, new[] {1, 2}},
            {CommandNames.Rename, new[] {3, 4}},
            {CommandNames.ChangeTags, new[] {3, 4}},
            {CommandNames.Analyse, new[] {3}},
        };

        public Command ChooseCommand(string[] args)
        {
            List<IMp3File> files;
            string mask;

            var parser = new ArgumentParser(args);
            parser.CheckForEmptiness();

            var commandName = args[0].ToLower();
            switch (commandName)
            {
                case CommandNames.Help:
                    parser.CheckIfCanBeExecuted(args, NumberOfCommandArguments[CommandNames.Help]);
                    return new HelpCommand(args);

                case CommandNames.Rename:
                    parser.CheckIfCanBeExecuted(args, NumberOfCommandArguments[CommandNames.Rename]);
                    files = GetFilesFromPath(args[1]);
                    mask = args[2];
                    return new RenameCommand(files, new Mp3Directory(), mask)
                    {
                        EnableBackup = !(args.Length == 4 && args[3] == "--backup-ignore")
                    };

                case CommandNames.ChangeTags:
                    parser.CheckIfCanBeExecuted(args, NumberOfCommandArguments[CommandNames.ChangeTags]);
                    files = GetFilesFromPath(args[1]);
                    mask = args[2];
                    return new ChangeTagsCommand(files, mask)
                    {
                        EnableBackup = !(args.Length == 4 && args[3] == "--backup-ignore")
                    };

                case CommandNames.Analyse:
                    parser.CheckIfCanBeExecuted(args, NumberOfCommandArguments[CommandNames.Analyse]);
                    files = GetFilesFromPath(args[1]);
                    mask = args[2];
                    return new AnalyseCommand(files, mask);

                default:
                    throw new InvalidOperationException("Invalid operation: there is no such command!");
            }
        }

        private List<IMp3File> GetFilesFromPath(string path)
        {
            var files = new List<IMp3File>();
            if (Path.GetExtension(path) != string.Empty)
            {
                // todo : mp3File - wrapper pattern
                files.Add(new Mp3File(TagLib.File.Create(path)));
            }
            else
            {
                var directory = new Mp3Directory();

                files.AddRange(directory.GetFiles(path));
            }
            return files;
        }
    }
}