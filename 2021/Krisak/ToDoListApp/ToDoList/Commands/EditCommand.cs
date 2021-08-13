using System.Collections.Generic;
using System.Text;
using ToDoLibrary.ChainOfResponsibility;
using ToDoLibrary.ChainOfResponsibility.ValidatorForUserInput;

namespace ToDoLibrary.Commands
{
    public class EditCommand : ICommand
    {
        public  int Index { get; private set; }
        public  string Text { get; private set; }
        
        public List<Task> PerformCommand(List<Task> tasks)
        {
            tasks[Index].Text = Text;
            return tasks;
        }

        public void RunValidate(List<Task> tasks)
        {
            var validator = ChainsOfValidationСompiler.CompileForEditCommand(tasks);
            ValidatorRunner.Run(validator, this);
        }

        public void SetParameters(string[] partsCommand)
        {
            var validator = new TryParseIntValidator(false);
            ValidatorRunner.Run(validator, partsCommand);

            Index = int.Parse(partsCommand[1]) - 1;
            Text = string.Join(' ', partsCommand, 2, partsCommand.Length - 2);
        }
    }
}