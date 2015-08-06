using System;
using CommandCreation;

namespace Mp3Tager
{
    public class Application
    {
        public void Execute(string[] args)
        {
            var backup = new BackupMp3Tager();
            backup.MakeMp3Backup(args);

            var command = CreateCommand(args);

            try
            {
                command.Execute();
            }
                //restore for all! exceptions
            catch (Exception e)
            {
                if(command.GetCommandName != CommandNames.Help)
                    backup.RestoreFromMp3Backup();
                throw e;
            }
        }

        private Command CreateCommand(string[] args)
        {
            var factory = new CommandFactory();
            var command = factory.ChooseCommand(args);
            return command;
        }
 
    }
}
