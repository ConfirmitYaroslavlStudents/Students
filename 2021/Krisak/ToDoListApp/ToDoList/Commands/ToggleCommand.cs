using System;
using System.Collections.Generic;
using System.Linq;
using ToDoLibrary.ChainOfResponsibility;
using ToDoLibrary.ChainOfResponsibility.ForToggleStatus;
using ToDoLibrary.ChainOfResponsibility.ValidatorForUserInput;

namespace ToDoLibrary.Commands
{
    public class ToggleCommand: ICommand
    {
        public  int Index { get; private set; }
        public  StatusTask Status { get; private set; }

        public List<Task> PerformCommand(List<Task> tasks)
        {
            tasks[Index].Status = Status;
            return tasks;
        }

        public void RunValidate(List<Task> tasks)
        {
            var validator = ChainsOfValidationСompiler.CompileForToggleCommand(tasks);
            ValidatorRunner.Run(validator, this);
        }

        public void SetParameters(string[] partsCommand)
        {
            var validator = new TryParseIntValidator(false);
            var statusValidator = new TryParseStatusTaskValidator(false);

            validator.SetNext(statusValidator);
            ValidatorRunner.Run(validator, partsCommand);

            Index = int.Parse(partsCommand[1])-1;
            Enum.TryParse(typeof(StatusTask), partsCommand[2], true, out var newStatus);
            Status = (StatusTask)newStatus;
        }
    }
}