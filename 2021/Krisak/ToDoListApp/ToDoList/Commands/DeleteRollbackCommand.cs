using System.Collections.Generic;

namespace ToDoLibrary.Commands
{
    public class DeleteRollbackCommand: ICommand
    {
        public List<Task> Tasks;

        public void PerformCommand()
        {
            Tasks.RemoveAt(Tasks.Count-1);
        }
    }
}