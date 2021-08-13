using System.Collections.Generic;
using ToDoLibrary.ChainOfResponsibility;
using ToDoLibrary.ChainOfResponsibility.ValidatorForUserInput;
using ToDoLibrary.Commands;

namespace ToDoLibrary.HandlerСommand
{
    public class RollbackCommandCreator: AbstractCommandCreatorDecorator
    {
        private readonly RollbackStorage _storage;

        public RollbackCommandCreator(ICreator creator, RollbackStorage storage) : base(creator) =>
            _storage = storage;

        public override ICommand TryGetCommand(string[] partsOfCommand)
        {
            var command = base.Creator.TryGetCommand(partsOfCommand);

            if (command != null)
                return command;

            if (partsOfCommand[0] != AllCommands.Rollback)
                return null;

            var rollbackCommand = new RollbackCommand(_storage);
            rollbackCommand.SetParameters(partsOfCommand);
            rollbackCommand.RunValidate();
            return rollbackCommand;
        }
    }
}