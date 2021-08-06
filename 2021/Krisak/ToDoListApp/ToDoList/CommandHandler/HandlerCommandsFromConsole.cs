using System.Collections.Generic;
using ToDoLibrary.Commands;
using ToDoLibrary.HandlerСommand;

namespace ToDoLibrary.CommandHandler
{
    public class HandlerCommandsFromConsole : IHandlerCommand
    {
        public bool IsFinished { get; private set; }

        private readonly List<Task> _tasks;
        private readonly Rollback _rollback;
        private readonly IDefiner _definer;

        public HandlerCommandsFromConsole(List<Task> tasks)
        {
            _tasks = tasks;
            _rollback = new Rollback(_tasks);
            _definer = GetDefiner();
        }

        public void Handling(string[] partsOfCommand)
        {
            if (partsOfCommand.Length == 0)
                throw new WrongEnteredCommandException("Empty command");

            var command = _definer.TryGetCommand(partsOfCommand);

            switch (command)
            {
                case null:
                    throw new WrongEnteredCommandException("Unknown command.");
                case ExitCommand _:
                    IsFinished = true;
                    break;
                default:
                    _rollback.AddNewRollback(partsOfCommand);
                    command.PerformCommand();
                    break;
            }

        }

        private IDefiner GetDefiner()
        {
            var baseDefiner = new BaseCommandDefiner(_tasks);
            var rollbackDefiner = new RollbackCommandDefinerDecorator(baseDefiner, _tasks, _rollback);
            var showDefiner = new ShowCommandDefinerDecorator(rollbackDefiner, _tasks);
            var exitDefiner = new ExitCommandDefinerDecorator(showDefiner);
            return exitDefiner;
        }
    }
}