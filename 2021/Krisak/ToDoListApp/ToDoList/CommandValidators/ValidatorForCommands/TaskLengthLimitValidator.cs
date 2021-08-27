using ToDoLibrary.Commands;

namespace ToDoLibrary.CommandValidators.ValidatorForCommands
{
    public class TaskLengthLimitValidator : AbstractValidator
    {
        private readonly int _limit;
        private readonly string _text;

        private TaskLengthLimitValidator(bool isAbort, int limit) : base(isAbort)
            => _limit = limit;

        public TaskLengthLimitValidator(bool isAbort, int limit, AddCommand addCommand) : this(isAbort, limit)
            => _text = addCommand.NewTask.Text;

        public TaskLengthLimitValidator(bool isAbort, int limit, EditTextCommand editTextCommand) : this(isAbort, limit)
            => _text = editTextCommand.Text;


        public override void Validate()
        {
            if (_text.Length >_limit)
            {
                ExceptionMessages.Add($"Task length must not be more than {_limit} characters.");
                if (IsAbort)
                    return;
            }

            base.Validate();
        }
    }
}