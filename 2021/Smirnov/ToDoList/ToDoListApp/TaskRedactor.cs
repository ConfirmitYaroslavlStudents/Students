using ToDoListLib;

namespace ToDoListApp
{
    public class TaskRedactor
    {
        private Task _task;

        public TaskRedactor(Task task)
        {
            _task = task;
        }

        public string TaskName
        {
            get
            {
                return _task.Name;
            }
        }

        public void ChangeName(string name)
        {
            _task.Name = name;
        }
        public void ChangeDescription(string description)
        {
            _task.Description = description;
        }
        public void CompleteTask()
        {
            _task.Status = TaskStatus.Done;
        }
    }
}
