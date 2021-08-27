using System.Collections.Generic;

namespace ToDoLibrary.Commands
{
    public class RollbackToggleStatusCommand: ICommand
    {
        public long TaskId;
        public StatusTask Status;

        public List<Task> PerformCommand(List<Task> tasks)
        {
            var task = tasks.Find((t) => t.TaskId == TaskId);
            task.Status = Status;
            return tasks;
        }
    }
}