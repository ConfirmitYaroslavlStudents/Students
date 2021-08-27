using ToDoLibrary.Commands;

namespace ToDoLibrary.CommandValidators.ValidatorForCommands
{
    public class CountRollbackInRangeValidator: AbstractValidator
    {
        private readonly int _rollbackCount;
        private readonly int _count;

        public CountRollbackInRangeValidator(bool isAbort, int rollbackCount, StartRollbackCommand command) : base(isAbort)
        {
            _rollbackCount = rollbackCount;
            _count = command.Count;
        }

        public override void Validate()
        {
            if (_count < 0 || _count > _rollbackCount)
            {
                ExceptionMessages.Add("Wrong count of rollback steps");
                if (IsAbort)
                    return;
            }

            base.Validate();
        }
    }
}