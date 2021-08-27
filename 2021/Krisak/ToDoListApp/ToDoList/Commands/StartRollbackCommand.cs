using System.Collections.Generic;
using ToDoLibrary.CommandValidators;
using ToDoLibrary.Storages;

namespace ToDoLibrary.Commands
{
    public class StartRollbackCommand: ICommand
    {
        public int Count;
        public RollbacksStorage RollbacksStorage;

        public List<Task> PerformCommand(List<Task> tasks)
        {
            RunValidate();

            while (Count!=0)
            {
                var command = RollbacksStorage.Pop();
                command.PerformCommand(tasks);

                Count--;
            }
            
            return tasks;
        }

        private void RunValidate()
        {
            var validator = ChainsOfValidationСompiler.CompileForRollbackCommand(RollbacksStorage.Count,this);
            ValidationRunner.Run(validator);
        }
    }
}