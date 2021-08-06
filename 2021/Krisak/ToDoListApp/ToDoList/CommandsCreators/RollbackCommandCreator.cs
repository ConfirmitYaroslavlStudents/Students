using System.Collections.Generic;
using ToDoLibrary.Commands;

namespace ToDoLibrary
{
    public class RollbackCommandCreator
    {
        private readonly List<Task> _tasks;
        private string[] _partsOfCommand;

        public RollbackCommandCreator(List<Task> tasks)
        {
            _tasks = tasks;
        }

        public ICommand AddNewCommand(string[] partsOfCommand)
        {
            _partsOfCommand = partsOfCommand;

            return _partsOfCommand[0] switch
            {
                AllCommands.Add => CreateDeleteRollbackCommand(),
                AllCommands.Edit => CreateEditRollbackСommand(),
                AllCommands.Toggle => CreateToggleRollbackCommand(),
                AllCommands.Delete => CreateAddRollbackСommand(),
                _ => null
            };
        }

        private ICommand CreateAddRollbackСommand()
        {
            var index = ValidationNumberInCommands.IntTryParseAndSubtract(_partsOfCommand[1]);
            
            if (index < 0 || index >= _tasks.Count)
                throw new WrongEnteredCommandException($"Task not found with number {index + 1}.");

            return new AddRollbackCommand { Index = index, Task = _tasks[index], Tasks = _tasks };
        }

        private ICommand CreateEditRollbackСommand()
        {
            var index = ValidationNumberInCommands.IntTryParseAndSubtract(_partsOfCommand[1]);

            if (index < 0 || index >= _tasks.Count)
                throw new WrongEnteredCommandException($"Task not found with number {index + 1}.");

            var text = _tasks[index].Text;

            return new EditRollbackCommand { Index = index, Text = text, Tasks = _tasks };
        }

        private ICommand CreateToggleRollbackCommand()
        {
            var index = ValidationNumberInCommands.IntTryParseAndSubtract(_partsOfCommand[1]);

            if (index < 0 || index >= _tasks.Count)
                throw new WrongEnteredCommandException($"Task not found with number {index + 1}.");

            var status = _tasks[index].Status;

            return new ToggleRollbackCommand {Index = index, Status = status, Tasks = _tasks};
        }

        private ICommand CreateDeleteRollbackCommand()
        {
            return new DeleteRollbackCommand {Tasks = _tasks};
        }
    }
}
