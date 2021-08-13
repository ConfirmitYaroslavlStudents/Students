using System.Collections.Generic;

namespace ToDoLibrary.Commands
{
    public class ToggleRollbackCommand: ICommand
    {
        public int Index { get; private set; }
        public StatusTask Status { get; private set; }

        public List<Task> PerformCommand(List<Task> tasks)
        {
            tasks[Index].Status = Status;
            return tasks;
        }

        public void SetParameters(ToggleCommand command, List<Task> tasks)
        {
            Index = command.Index;
            Status = tasks[command.Index].Status;
        }
    }
}