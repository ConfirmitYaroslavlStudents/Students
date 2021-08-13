using System.Collections.Generic;
using ToDoLibrary.Commands;

namespace ToDoLibrary.ChainOfResponsibility
{
    public class CountRollbackInRangeValidator: AbstractValidator
    {
        private readonly Stack<ICommand> _rollbacks;

        public CountRollbackInRangeValidator(bool isAbort, Stack<ICommand> rollbacks) : base(isAbort)
        {
            _rollbacks = rollbacks;
        }

        public override void Validate(ICommand command)
        {
            var rollbackCommand = command as RollbackCommand;
            var count = rollbackCommand.Count;

            if (count < 0 || count > _rollbacks.Count)
            {
                base.Exceptions.Add("Wrong count of rollback steps");
                if (base.IsAbort)
                    return;
            }

            base.Validate(command);
        }
    }
}