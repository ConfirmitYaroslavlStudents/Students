using System.Collections.Generic;
using System.Text;
using ToDoLibrary.Commands;

namespace ToDoLibrary.ChainOfResponsibility
{
    public class TaskLengthLimitValidator : AbstractValidator
    {
        private readonly int _limit;

        public TaskLengthLimitValidator(bool isAbort, int limit) : base(isAbort)
        {
            _limit = limit;
        }

        public override void Validate(ICommand command)
        {
            var text = GetText(command);

            if (text.Length > _limit)
            {
                base.Exceptions.Add($"Task length must not be more than {_limit} characters.");
                if (base.IsAbort)
                    return;
            }

            base.Validate( command);
        }

        private string GetText(ICommand command)
        {
            return command switch
            {
                AddCommand addCommand => addCommand.NewTask.Text,
                EditCommand editCommand => editCommand.Text,
                _ => ""
            };
        }
    }
}