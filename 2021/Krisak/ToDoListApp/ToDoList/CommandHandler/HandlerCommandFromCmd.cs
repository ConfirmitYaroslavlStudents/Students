using System.Collections.Generic;
using ToDoLibrary.Commands;
using ToDoLibrary.HandlerСommand;

namespace ToDoLibrary.CommandHandler
{
    public class HandlerCommandFromCmd : IHandlerCommand
    {
        private List<Task> _tasks;
        private IDefiner _definer;

        public HandlerCommandFromCmd(List<Task> tasks)
        {
            _tasks = tasks;
            _definer = GetDefiner();
        }

        public void Handling(string[] partsOfCommand)
        {
            var command = _definer.TryGetCommand(partsOfCommand);

            switch (command)
            {
                case null:
                    throw new WrongEnteredCommandException("Unknown command.");
                default:
                    command.PerformCommand();
                    break;
            }

        }

        private IDefiner GetDefiner()
        {
            var baseDefiner = new BaseCommandDefiner(_tasks);
            var showDefiner = new ShowCommandDefinerDecorator(baseDefiner, _tasks);
            return showDefiner;
        }
    }
}