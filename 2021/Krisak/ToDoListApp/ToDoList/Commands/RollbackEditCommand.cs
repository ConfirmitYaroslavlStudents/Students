using System.Collections.Generic;

namespace ToDoLibrary.Commands
{
    public class RollbackEditCommand : ICommand
    {
        public long TaskId;
        public string Text;
        
        public List<Task> PerformCommand(List<Task> tasks)
        {
            var task = tasks.Find((t) => t.TaskId == TaskId);
            task.Text = Text;
            return tasks;
        }
    }
}