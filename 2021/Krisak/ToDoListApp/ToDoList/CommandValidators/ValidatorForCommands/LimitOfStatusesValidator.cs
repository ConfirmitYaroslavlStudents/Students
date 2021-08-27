using System.Collections.Generic;
using System.Linq;
using ToDoLibrary.Commands;

namespace ToDoLibrary.CommandValidators.ValidatorForCommands
{
    public class LimitOfStatusesValidator: AbstractValidator
    {
        private const int LimitOfTaskInProgressStatus = 3;

        private readonly List<Task> _tasks;
        private readonly StatusTask _status;

        public LimitOfStatusesValidator(bool isAbort, List<Task>tasks, ToggleStatusCommand command) : base(isAbort)
        {
            _tasks = tasks;
            _status = command.Status;
        }

        public override void Validate()
        {
            if (_status == StatusTask.IsProgress && CountTasksInProgressStatus() >= LimitOfTaskInProgressStatus)
            { 
                ExceptionMessages.Add($"Number of tasks in \"In Progress\" status should not exceed {LimitOfTaskInProgressStatus}.");
                if (IsAbort)
                    return;
            }

            base.Validate();
        }

        private int CountTasksInProgressStatus()
        {
            return _tasks.Count(task => task.Status == StatusTask.IsProgress);
        }
    }
}