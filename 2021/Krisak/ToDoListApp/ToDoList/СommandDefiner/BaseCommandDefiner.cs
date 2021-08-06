using System.Collections.Generic;
using ToDoLibrary.Commands;

namespace ToDoLibrary.HandlerСommand
{
    public class BaseCommandDefiner: IDefiner
    {
        private readonly CommandCreator _commandCreator;

        public BaseCommandDefiner(List<Task> tasks)
        {
            _commandCreator = new CommandCreator(tasks);
        }

        public ICommand TryGetCommand(string[] partsOfCommand)
        {
            return partsOfCommand[0] switch
            {
                AllCommands.Add => _commandCreator.CreateAddСommand(partsOfCommand),
                AllCommands.Edit => _commandCreator.CreateEditСommand(partsOfCommand),
                AllCommands.Toggle => _commandCreator.CreateToggleСommand(partsOfCommand),
                AllCommands.Delete => _commandCreator.CreateDeleteСommand(partsOfCommand),
                _ => null
            };
        }
    }
}