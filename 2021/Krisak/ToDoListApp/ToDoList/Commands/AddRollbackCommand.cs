using System.Collections.Generic;

namespace ToDoLibrary.Commands
{
    public class AddRollbackCommand: ICommand
    {
        public  int Index { get; private set; }
        public Task Task { get; private set; }

        public List<Task> PerformCommand(List<Task> tasks)
        {
            tasks.Insert(Index, Task);
            return tasks;
        }

        public void SetParameters(DeleteCommand command, List<Task> tasks)
        {
            Index = command.Index;
            Task = tasks[command.Index];
        }
    }
}