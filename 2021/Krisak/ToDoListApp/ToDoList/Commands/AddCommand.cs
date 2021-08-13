using System.Collections.Generic;
using System.Text;
using ToDoLibrary.ChainOfResponsibility;

namespace ToDoLibrary.Commands
{
    public class AddCommand : ICommand
    {
        public Task NewTask { get; private set; }

        public List<Task> PerformCommand(List<Task> tasks)
        {
            tasks.Add(NewTask);
            return tasks;
        }

        public void RunValidate()
        {
            var validator = ChainsOfValidationСompiler.CompileForAddCommand();
            ValidatorRunner.Run(validator,this);
        }

        public void SetParameters(string[] partsCommand)
        {
            var text = string.Join(' ', partsCommand, 1, partsCommand.Length - 1);
            NewTask = new Task { Text = text};
        }
    }
}