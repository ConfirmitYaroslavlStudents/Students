using System.Collections.Generic;

namespace ToDoLibrary.Commands
{
    public class RollbackDeleteCommand: ICommand
    {
        public int Index;
        public Task Task;

        public List<Task> PerformCommand(List<Task> tasks)
        {
            tasks.Insert(Index, Task);
            return tasks;
        }
    }
}