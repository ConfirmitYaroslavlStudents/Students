using System;
using FileBackuperLib;
using CommandCreation;

namespace Mp3Tager
{
    public class Application
    {
        public void Execute(string[] args)
        {
            var backup = new FileBackuper();

            if (args.Length > 1)
                backup.MakeBackup(new File(args[1]));

            var command = new CommandFactory().ChooseCommand(args);
            try
            {
                command.Execute();
            }
            catch (Exception e)
            {
               backup.RestoreFromBackup();
            }

            backup.Free();
        }
    }
}
