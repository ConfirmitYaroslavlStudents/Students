using System.Collections.Generic;
using ToDoLibrary.Commands;

namespace ToDoLibrary
{
    public class TaskStorage
    {
        private List<Task> _task = new List<Task>();

        public List<Task> Get()
        {
            return new List<Task>(_task);
        }

        public void Set(List<Task> tasks)
        {
            _task = new List<Task>( tasks);
        }
    }
}