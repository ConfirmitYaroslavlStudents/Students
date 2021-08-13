using System.Collections.Generic;
using ToDoLibrary.Commands;

namespace ToDoLibrary.HandlerСommand
{
    public class BaseRollbackCommandCreator
    {
        private readonly List<Task> _tasks;
        public BaseRollbackCommandCreator(List<Task> tasks) => _tasks = tasks;

        public ICommand TryGetCommand(ICommand command)
        {
            return command switch
            {
                AddCommand _ => Delete(),
                EditCommand editCommand => Edit(editCommand),
                ToggleCommand toggleCommand => Toggle(toggleCommand),
                DeleteCommand deleteCommand => Add(deleteCommand),
                _ => null
            };
        }

        private ICommand Add(DeleteCommand command)
        {
            var addCommand = new AddRollbackCommand();
            addCommand.SetParameters(command, _tasks);
            return addCommand;
        }

        private ICommand Edit(EditCommand command)
        {
            var editCommand = new EditRollbackCommand();
            editCommand.SetParameters(command, _tasks);
            return editCommand;
        }

        private ICommand Toggle(ToggleCommand command)
        {
            var toggleCommand = new ToggleRollbackCommand();
            toggleCommand.SetParameters(command, _tasks);
            return toggleCommand;
        }

        private ICommand Delete() => new DeleteRollbackCommand();
    }
}