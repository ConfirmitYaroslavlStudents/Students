using System.Linq;
using ToDoLibrary.Commands;

namespace ToDoLibrary.ChainOfResponsibility.ForToggleStatus
{
    public class LimitOfStatusesToggleResponsibility: AbstractHandlerResponsibility
    {
        private const int LimitOfTaskInProgressStatus = 3;
        private ToggleCommand _command;
        public override object HandlerResponsibility(object @object)
        {
            _command = (ToggleCommand) @object;

            if (_command.Status == StatusTask.IsProgress && NumberTasksInProgressStatus() >= LimitOfTaskInProgressStatus)
                throw new WrongEnteredCommandException(
                    $"Number of tasks in \"In Progress\" status should not exceed {LimitOfTaskInProgressStatus}.");

            return base.HandlerResponsibility(@object);
        }

        private int NumberTasksInProgressStatus()
        {
            return _command.Tasks.Count(task => task.Status == StatusTask.IsProgress);
        }
    }
}