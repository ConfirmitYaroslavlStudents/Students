using System;
using FileLib;

namespace CommandCreation
{
    public class CommandFactory
    {
        public Command ChooseCommand(string commandName, IMp3File file, IWorker worker, string mask)
        {          
            switch (commandName)
            {
                case CommandNames.Rename:
                    return new RenameCommand(file, mask);

                case CommandNames.ChangeTags:
                    return new ChangeTagsCommand(file, mask);
                  
                case CommandNames.Analyse:
                    return new AnalyseCommand(file, mask, worker);

                case CommandNames.Sync:
                    return new SyncCommand(file, mask);

                default:
                    throw new InvalidOperationException("Invalid operation: there is no such command!");
            }
        }
    }
}
