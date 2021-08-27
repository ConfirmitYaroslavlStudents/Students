using System.Collections.Generic;
using ToDoLibrary.Commands;

namespace ToDoLibrary.CommandValidators.ValidatorForCommands
{
    public class ExistenceIdValidator : AbstractValidator
    {
        private readonly List<Task> _tasks;
        private readonly long _id;

        private ExistenceIdValidator(bool isAbort, List<Task> tasks) : base(isAbort)
            => _tasks = tasks;

        public ExistenceIdValidator(bool isAbort, List<Task> tasks, EditTextCommand editTextCommand) : this(isAbort, tasks)
            => _id = editTextCommand.TaskId;

        public ExistenceIdValidator(bool isAbort, List<Task> tasks, ToggleStatusCommand toggleStatusCommand) : this(isAbort, tasks)
            => _id = toggleStatusCommand.TaskId;

        public ExistenceIdValidator(bool isAbort, List<Task> tasks, DeleteCommand deleteCommand) : this(isAbort, tasks)
            => _id = deleteCommand.TaskId;

        public override void Validate()
        {
            if (_tasks.Find((t) => t.TaskId == _id) == null)
            {
                ExceptionMessages.Add($"Task not found with Id {_id}.");
                if (IsAbort)
                    return;
            }

            base.Validate();
        }
    }
}