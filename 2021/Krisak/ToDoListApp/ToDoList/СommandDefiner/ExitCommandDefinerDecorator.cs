using ToDoLibrary.Commands;

namespace ToDoLibrary.HandlerСommand
{
    public class ExitCommandDefinerDecorator: BaseCommandDefinerDecorator
    {
        public ExitCommandDefinerDecorator(IDefiner definer) : base(definer) { }

        public override ICommand TryGetCommand(string[] partsOfCommand)
        {
            var command = base.Definer.TryGetCommand(partsOfCommand);

            if (command != null)
                return command;

            return partsOfCommand[0] != AllCommands.Exit ? null : new ExitCommand();
        }
    }
}