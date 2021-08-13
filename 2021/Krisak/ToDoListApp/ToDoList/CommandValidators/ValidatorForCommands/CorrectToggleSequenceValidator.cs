using System.Collections.Generic;
using ToDoLibrary.Commands;

namespace ToDoLibrary.ChainOfResponsibility.ForToggleStatus
{
    public class CorrectToggleSequenceValidator: AbstractValidator
    {
        private readonly List<Task> _tasks;
        public CorrectToggleSequenceValidator(bool isAbort, List<Task> tasks) : base(isAbort)
        {
            _tasks = tasks;
        }

        public override void Validate (ICommand command)
        {
            var toggleCommand = command as ToggleCommand;
            var status = toggleCommand.Status;
            var previousStatus = _tasks[toggleCommand.Index].Status;

            switch (status)
            {
                case StatusTask.IsProgress when previousStatus != StatusTask.ToDo:
                {
                    base.Exceptions.Add("Status \"In Progress\" can only be toggled from status \"To Do\".");
                    if (base.IsAbort)
                        return;
                    break;
                }
                case StatusTask.Done when previousStatus != StatusTask.IsProgress:
                {
                    base.Exceptions.Add("Status \"Done\" can only be toggled from status \"In Progress\".");
                    if (base.IsAbort)
                        return;
                    break;
                }
            }

            base.Validate(command);
        }
    }
}