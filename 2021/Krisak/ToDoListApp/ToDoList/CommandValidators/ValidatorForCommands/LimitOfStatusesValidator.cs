using System.Collections.Generic;
using System.Linq;
using ToDoLibrary.Commands;

namespace ToDoLibrary.ChainOfResponsibility.ForToggleStatus
{
    public class LimitOfStatusesValidator: AbstractValidator
    {
        private const int LimitOfTaskInProgressStatus = 3;

        private List<Task> _tasks;

        public LimitOfStatusesValidator(bool isAbort, List<Task>tasks) : base(isAbort)
        {
            _tasks = tasks;
        }

        public override void Validate(ICommand command)
        {
            var toggleCommand = command as ToggleCommand;
            var status = toggleCommand.Status;

            if (status == StatusTask.IsProgress && CountTasksInProgressStatus() >= LimitOfTaskInProgressStatus)
            { 
                base.Exceptions.Add($"Number of tasks in \"In Progress\" status should not exceed {LimitOfTaskInProgressStatus}.");
                if (IsAbort)
                    return;
            }

            base.Validate(command);
        }

        private int CountTasksInProgressStatus()
        {
            return _tasks.Count(task => task.Status == StatusTask.IsProgress);
        }
    }
}