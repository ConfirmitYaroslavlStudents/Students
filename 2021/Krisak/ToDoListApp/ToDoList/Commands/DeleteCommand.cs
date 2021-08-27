using System.Collections.Generic;
using ToDoLibrary.CommandValidators;

namespace ToDoLibrary.Commands
{
    public class DeleteCommand: ICommand
    {
        public long TaskId;

        public List<Task> PerformCommand(List<Task> tasks)
        {
            RunValidate(tasks);

            tasks.RemoveAll((task)=> task.TaskId == TaskId);
            return tasks;
        }

        private void RunValidate(List<Task> tasks)
        {
            var validator = ChainsOfValidationСompiler.CompileForDeleteCommand(tasks,this);
            ValidationRunner.Run(validator);
        }
    }
}