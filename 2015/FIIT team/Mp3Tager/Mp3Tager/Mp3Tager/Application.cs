using System;
using Backuper;
using CommandCreation;

namespace Mp3Tager
{
    public class Application
    {
        public void Execute(string[] args)
        {
            var backup = new Backup(new File(args[1]));
            backup.MakeBackup();

            var command = new CommandFactory().ChooseCommand(args);
            try
            {
                command.Execute();
            }
            catch (Exception e)
            {
               backup.RestoreFromBackup();
            }
        }
    }
}
