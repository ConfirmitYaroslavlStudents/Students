using System;
using CommandCreation;

namespace Mp3Tager
{
    public class Application
    {
        public void Execute(string[] args, IWriter writer)
        {
            var command = new CommandFactory().ChooseCommand(args, writer);
            command.Execute();            
        }
    }
}
