using System;
using System.IO;
using CommandCreation;

namespace Mp3Tager
{
    public class Application
    {
        public void Execute(string[] args, IWriter writer)
        {
            var command = new CommandFactory().ChooseCommand(args, writer);
            Console.WriteLine(command.Execute());
            Console.WriteLine("\n\nSave changes? y/n");
            if (Console.ReadLine() == "y")
                command.Complete();
        }
    }
}
