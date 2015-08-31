using System;
using System.Collections.Generic;
using System.Linq;
using CommandCreation;
using FileLib;
using FileLib.FileSource;

namespace Mp3Tager
{
    public class Application
    {
        public void Execute(string[] args, IWorker worker)
        {
            var parser = new ArgumentParser(args);
            parser.CheckIfCanBeExecuted();

            if (args[0] == CommandNames.Help)
            {
                ShowHelp(args, worker);
                return;
            }

            var files = new FileSource().GetFiles(args[1]);
            var commandPool = files.Select(file => new CommandFactory().ChooseCommand(args[0], file, worker, args[2])).ToList();

            if (!commandPool.Any(command => command.IsPlanningCommand()))
            {
                commandPool.ForEach(command => command.Execute());
                return;
            }

            foreach (var command in commandPool)
            {
                try
                {
                    command.Execute();
                    ShowPlan(command, worker);
                }
                catch (Exception e)
                {
                    worker.WriteLine("Command " + command.Accept(new GetCommandNameVisitor())
                        + "cannot be executed for file" + command.Accept(new GetFilePathVisitor())
                        + "because of exception:\n" + e.Message);
                }
            }

            SaveChanges(files, worker);
        }

        private void ShowHelp(string[] args, IWorker worker)
        {
            var helpMessages = new Dictionary<string, string>
            {
                {CommandNames.Help, ""},
                {CommandNames.Rename, @"<path> <pattern>"},
                {CommandNames.ChangeTags, @"<path> <mask>"},
                {CommandNames.Analyse, @"<path> <mask>"},
                {CommandNames.Sync, @"<path> <mask>"},
            };
            var commandForHelp = args.Length == 2 ? args[1] : null;
            if (commandForHelp == null)
                foreach (var message in helpMessages)
                    worker.WriteLine(message.Key + ": " + message.Value);
            else
                worker.WriteLine(helpMessages.ContainsKey(commandForHelp)
                    ? helpMessages[commandForHelp]
                    : "There is no such command!");
        }

        
        private void ShowPlan(Command command, IWorker worker)
        {
            worker.WriteLine(command.Accept(new GetFilePathVisitor()));
            worker.WriteLine(command.Accept(new DifferenceVisitor()));
        }

        private void SaveChanges(IEnumerable<IMp3File> files, IWorker worker)
        {
            worker.WriteLine("Save changes? y/n");

            if (worker.ReadLine() != "y")
            {
                worker.WriteLine("Command cancelled.");
                return;
            }

            foreach (var mp3File in files)
            {
                var backup = new FileBackuper(mp3File);
                using (backup)
                {
                    try
                    {
                        mp3File.Save();
                    }
                    catch (Exception e)
                    {
                        backup.RestoreFromBackup();
                        throw new Exception("File " + mp3File.FullName
                            + " was restored from backup because of exception:", e);
                    }
                }
            }
            worker.WriteLine("Command succesfully executed.");
        }
    }
}
