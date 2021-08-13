using System.Collections.Generic;
using ToDoLibrary.ChainOfResponsibility;
using ToDoLibrary.ChainOfResponsibility.ValidatorForUserInput;

namespace ToDoLibrary.Commands
{
    public class DeleteCommand: ICommand
    {
        public int Index { get; private set; }

        public List<Task> PerformCommand(List<Task> tasks)
        {
            tasks.RemoveAt(Index);
            return tasks;
        }

        public void RunValidate(List<Task> tasks)
        {
            var validator = ChainsOfValidationСompiler.CompileForDeleteCommand(tasks);
            ValidatorRunner.Run(validator, this);
        }

        public void SetParameters(string[] partsCommand)
        {
            var validator = new TryParseIntValidator(false);
            ValidatorRunner.Run(validator, partsCommand);

            Index = int.Parse(partsCommand[1]) - 1;
        }
    }
}