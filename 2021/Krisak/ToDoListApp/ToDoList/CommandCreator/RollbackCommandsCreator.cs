using System.Collections.Generic;
using ToDoLibrary.Commands;

namespace ToDoLibrary.CommandCreator
{
    public class RollbackCommandsCreator
    {
        private readonly List<Task> _tasks;
        public RollbackCommandsCreator(List<Task> tasks) => _tasks = tasks;

        public ICommand GetCommand(ICommand command)
        {
            return command switch
            {
                AddCommand _ => Add(),
                EditTextCommand editCommand => Edit(editCommand),
                ToggleStatusCommand toggleCommand => Toggle(toggleCommand),
                DeleteCommand deleteCommand => Delete(deleteCommand),
                _ => null
            };
        }

        public ICommand Add()
            => new RollbackAddCommand();

        public ICommand Edit(EditTextCommand editTextCommand)
        {
            var index = _tasks.FindIndex((task) => task.TaskId == editTextCommand.TaskId);
            var text = _tasks[index].Text;
            var taskId = _tasks[index].TaskId;

            return new RollbackEditCommand { TaskId = taskId, Text = text };
        }

        public ICommand Toggle(ToggleStatusCommand toggleStatusCommand)
        {
            var index = _tasks.FindIndex((task) => task.TaskId == toggleStatusCommand.TaskId);
            var status = _tasks[index].Status;
            var taskId = _tasks[index].TaskId;

            return new RollbackToggleStatusCommand() { TaskId = taskId, Status = status};
        }

        public ICommand Delete(DeleteCommand editCommand)
        {
            var index = _tasks.FindIndex((task) => task.TaskId == editCommand.TaskId);
            var task = _tasks[index];

            return new RollbackDeleteCommand() { Index = index,Task = task};
        }
    }
}