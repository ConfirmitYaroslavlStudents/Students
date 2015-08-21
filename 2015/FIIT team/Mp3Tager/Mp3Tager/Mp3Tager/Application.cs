using System;
using System.IO;
using CommandCreation;

namespace Mp3Tager
{
    public class Application
    {
        public void Execute(string[] args, IWorker worker)
        {
            var command = new CommandFactory().ChooseCommand(args);
            worker.WriteLine(command.Execute());

            if (command.ShouldBeCompleted)
            {
                worker.WriteLine("\n\nSave changes? Input y/n");
                if (worker.ReadLine() == "y")
                    command.Complete();
            }
        }
    }
}
