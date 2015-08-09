using System;
using FileBackuperLib;
using CommandCreation;

namespace Mp3Tager
{
    public class Application
    {
        public void Execute(string[] args)
        {
            var command = new CommandFactory().ChooseCommand(args);
            command.Execute();            
        }
    }
}
