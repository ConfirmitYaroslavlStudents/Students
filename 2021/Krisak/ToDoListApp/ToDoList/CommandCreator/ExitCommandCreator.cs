using ToDoLibrary.Commands;

namespace ToDoLibrary.HandlerСommand
{
    public class ExitCommandCreator: AbstractCommandCreatorDecorator
    {
        public ExitCommandCreator(ICreator creator) : base(creator) { }

        public override ICommand TryGetCommand(string[] partsOfCommand)
        {
            var command = base.Creator.TryGetCommand(partsOfCommand);

            if (command != null)
                return command;

            return partsOfCommand[0] != AllCommands.Exit ? null : new ExitCommand();
        }
    }
}