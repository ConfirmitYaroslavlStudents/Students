using System.Collections.Generic;
using ToDoLibrary.Commands;
using ToDoLibrary.HandlerСommand;

namespace ToDoLibrary.CommandHandler
{
    public class CommandFromCmdHandler : IHandlerCommand
    {
        private readonly TaskStorage _taskStorage;

        public CommandFromCmdHandler(TaskStorage taskStorage) => _taskStorage = taskStorage;

        public ResultHandler CommandHandler(string[] partsOfCommand)
        {
            var command = GetDefiner().TryGetCommand(partsOfCommand);

            switch (command)
            {
                case null:
                    throw new WrongEnteredCommandException("Unknown command.");
                default:
                    _taskStorage.Set(command.PerformCommand(_taskStorage.Get()));
                    return ResultHandler.Completed;
            }
        }

        private ICreator GetDefiner()
        {
            var tasks = _taskStorage.Get();
            var baseDefiner = new BaseCommandCreator(tasks);
            var showDefiner = new ShowCommandCreatorDecorator(baseDefiner, new ShowTasksInConsole());
            return showDefiner;
        }
    }
}