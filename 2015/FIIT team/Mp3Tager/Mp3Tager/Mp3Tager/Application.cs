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
                var answer = worker.ReadLine();
                // how it should react in case of other letters??
                if (answer == "y")
                {
                    command.Complete();
                    worker.WriteLine("Command successfully executed.");
                }
                else if (answer == "n")
                {
                    worker.WriteLine("Command canceled");
                }
            }
        }
    }
}
