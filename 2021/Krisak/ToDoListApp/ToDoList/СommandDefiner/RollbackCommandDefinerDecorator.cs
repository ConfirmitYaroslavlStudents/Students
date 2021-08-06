using System.Collections.Generic;
using ToDoLibrary.Commands;

namespace ToDoLibrary.HandlerСommand
{
    public class RollbackCommandDefinerDecorator: BaseCommandDefinerDecorator
    {
        private readonly CommandCreator _commandCreator;
        private readonly Rollback _rollback;
        public RollbackCommandDefinerDecorator(IDefiner definer, List<Task> tasks, Rollback rollback) : base(definer)
        {
            _commandCreator = new CommandCreator(tasks);
            _rollback = rollback;
        }

        public override ICommand TryGetCommand(string[] partsOfCommand)
        {
            var command = base.Definer.TryGetCommand(partsOfCommand);

            if (command != null)
                return command;

            return partsOfCommand[0] != AllCommands.Rollback ? null : _commandCreator.CreateRollbackСommand(partsOfCommand, _rollback);
        }
    }
}