using System.Collections.Generic;
using ToDoLibrary.Commands;
using ToDoLibrary.HandlerСommand;

namespace ToDoLibrary.CommandHandler
{
    public class CommandsFromConsoleHandler : IHandlerCommand
    {
        private readonly TaskStorage _taskStorage;
        private readonly RollbackStorage _rollbackStorage = new RollbackStorage();

        public CommandsFromConsoleHandler(TaskStorage taskStorage) => _taskStorage = taskStorage;

        public ResultHandler CommandHandler(string[] partsOfCommand)
        {

            if (partsOfCommand.Length == 0)
                throw new WrongEnteredCommandException("Empty command");

            var command = GetDefiner().TryGetCommand(partsOfCommand);

            switch (command)
            {
                case null:
                    throw new WrongEnteredCommandException("Unknown command.");
                case ExitCommand _:
                    return ResultHandler.Completed;
                default:
                    TryPushRollbackCommand(command);
                    _taskStorage.Set(command.PerformCommand(_taskStorage.Get()));
                    return ResultHandler.Working; ;
            }
        }

        private void TryPushRollbackCommand(ICommand command)
        {
            var rollbackCommandCreator = new BaseRollbackCommandCreator(_taskStorage.Get());
            var rollbackCommand = rollbackCommandCreator.TryGetCommand(command);
            
            if (rollbackCommand==null)
                return;

            var storage = _rollbackStorage.Get();
            storage.Push(rollbackCommand);
            _rollbackStorage.Set(storage);
        }

        private ICreator GetDefiner()
        {
            var tasks = _taskStorage.Get();
            var baseCommand = new BaseCommandCreator(tasks);
            var rollback = new RollbackCommandCreator(baseCommand, _rollbackStorage);
            var show = new ShowCommandCreatorDecorator(rollback,new ShowTasksInConsole());
            var exit = new ExitCommandCreator(show);

            return exit;
        }
    }
}