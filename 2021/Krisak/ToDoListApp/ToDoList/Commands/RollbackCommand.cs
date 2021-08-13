using System;
using System.Collections.Generic;
using ToDoLibrary.ChainOfResponsibility;
using ToDoLibrary.ChainOfResponsibility.ValidatorForUserInput;
using ToDoLibrary.Commands;

namespace ToDoLibrary
{
    public class RollbackCommand: ICommand
    {
        public int Count { get; private set; }
        private readonly RollbackStorage _storage;

        public RollbackCommand(RollbackStorage storage) => _storage = storage;

        public List<Task> PerformCommand(List<Task> tasks)
        {
            var rollbacks = _storage.Get();

            for (var i = Count; i > 0; i--)
            {
                var command = rollbacks.Pop();
                command.PerformCommand(tasks);

                Count--;
            }

            _storage.Set(rollbacks);
            return tasks;
        }

        public void RunValidate()
        {
            var validator = ChainsOfValidationСompiler.CompileForRollbackCommand(_storage.Get());
            ValidatorRunner.Run(validator,this);
        }

        public void SetParameters(string[] partsCommand)
        {
            var validator = new TryParseIntValidator(false);
            ValidatorRunner.Run(validator, partsCommand);

            Count = int.Parse(partsCommand[1]);
        }
    }
}