using System.Collections.Generic;
using ToDoLibrary.Commands;

namespace ToDoLibrary.HandlerСommand
{
    public class ShowCommandDefinerDecorator: BaseCommandDefinerDecorator
    {
        private readonly CommandCreator _commandCreator;
        public ShowCommandDefinerDecorator(IDefiner definer, List<Task> tasks) : base(definer)
        {
            _commandCreator = new CommandCreator(tasks);
        }

        public override ICommand TryGetCommand(string[] partsOfCommand)
        {
            var command = base.Definer.TryGetCommand(partsOfCommand);
            if (command!=null)
                return command;

            return partsOfCommand[0] != AllCommands.Show ? null : _commandCreator.CreateShowСommand();
        }
    }
}