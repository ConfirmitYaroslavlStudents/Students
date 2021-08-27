using System.Collections.Generic;
using ToDoLibrary.CommandValidators;
using ToDoLibrary.Storages;

namespace ToDoLibrary.Commands
{
    public class AddCommand : ICommand
    {
        public Task NewTask;

        public List<Task> PerformCommand(List<Task> tasks)
        {
            RunValidate();

            if (tasks.Count == 0)
                NewTask.TaskId = 1;
            else
                NewTask.TaskId = tasks[^1].TaskId + 1;

            tasks.Add(NewTask);
            return tasks;
        }

        private void RunValidate()
        {
            var validator = ChainsOfValidationСompiler.CompileForAddCommand(this);
            ValidationRunner.Run(validator);
        }
    }
}