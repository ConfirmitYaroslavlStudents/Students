using System.Collections.Generic;
using ToDoLibrary.Commands;

namespace ToDoLibrary.CommandValidators.ValidatorForCommands
{
    public class CorrectToggleSequenceValidator : AbstractValidator
    {
        private readonly List<Task> _tasks;
        private readonly ToggleStatusCommand _command;

        public CorrectToggleSequenceValidator(bool isAbort, List<Task> tasks, ToggleStatusCommand command) : base(isAbort)
        {
            _command = command;
            _tasks = tasks;
        }

        public override void Validate()
        {
            var newStatus = _command.Status;
            var previousStatus = _tasks.Find((t) => t.TaskId == _command.TaskId).Status;
            switch (newStatus)
            {
                case StatusTask.IsProgress when previousStatus != StatusTask.ToDo:
                    {
                        ExceptionMessages.Add("Status \"In Progress\" can only be toggled from status \"To Do\".");
                        if (IsAbort)
                            return;
                        break;
                    }
                case StatusTask.Done when previousStatus != StatusTask.IsProgress:
                    {
                        ExceptionMessages.Add("Status \"Done\" can only be toggled from status \"In Progress\".");
                        if (IsAbort)
                            return;
                        break;
                    }
            }

            base.Validate();
        }
    }
}