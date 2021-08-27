using System.Collections.Generic;
using ToDoLibrary.CommandValidators;

namespace ToDoLibrary.Commands
{
    public class EditTextCommand : ICommand
    {
        public long TaskId;
        public string Text;
        
        public List<Task> PerformCommand(List<Task> tasks)
        {
            RunValidate(tasks);

            var task = tasks.Find((t) => t.TaskId == TaskId);
            task.Text = Text;
            
            return tasks;
        }

        private void RunValidate(List<Task> tasks)
        {
            var validator = ChainsOfValidationСompiler.CompileForEditCommand(tasks,this);
            ValidationRunner.Run(validator);
        }
    }
}