using System.Collections.Generic;

namespace ToDoLibrary.Commands
{
    public class RollbackAddCommand : ICommand
    {
        public List<Task> PerformCommand(List<Task> tasks)
        {
            tasks.RemoveAt(tasks.Count - 1);
            return tasks;
        }
    }
}