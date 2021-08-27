using System.Collections.Generic;

namespace ToDoLibrary.Storages
{
    public class TasksStorage
    {
        private List<Task> _task = new List<Task>();

        public List<Task> Get()
        {
            var newList = new List<Task>();
            foreach (var task in _task)
                newList.Add(new Task { TaskId = task.TaskId, Text = task.Text, Status = task.Status });
            return newList;
        }

        public void Set(List<Task> tasks) 
            => _task = new List<Task>(tasks);
    }
}