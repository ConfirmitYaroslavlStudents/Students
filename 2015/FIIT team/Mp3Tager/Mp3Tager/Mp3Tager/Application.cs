using CommandCreation;

namespace Mp3Tager
{
    public class Application
    {
        public void Execute(string[] args)
        {
            var command = CreateCommand(args);
            command.Execute();
        }

        private Command CreateCommand(string[] args)
        {
            var factory = new CommandFactory();
            var command = factory.ChooseCommand(args);
            return command;
        }
    }
}
