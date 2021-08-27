using System.Collections.Generic;
using ToDoLibrary.CommandValidators;

namespace ToDoLibrary.Commands
{
    public class ToggleStatusCommand : ICommand
    {
        public long TaskId;
        public StatusTask Status;

        public List<Task> PerformCommand(List<Task> tasks)
        {
            RunValidate(tasks);

            var task = tasks.Find((t) => t.TaskId == TaskId);
            task.Status = Status;

            return tasks;
        }

        private void RunValidate(List<Task> tasks)
        {
            var validator = ChainsOfValidationСompiler.CompileForToggleCommand(tasks, this);
            ValidationRunner.Run(validator);
        }
    }
}