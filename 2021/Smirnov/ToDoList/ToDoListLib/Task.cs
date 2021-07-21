using System;

namespace ToDoListLib
{
    public class Task: IEquatable<Task>
    {
        public string Description { set; get; }
        public TaskStatus Status { set; get; }

        public bool Equals(Task other)
        {
            return other != null && Description == other.Description && Status == other.Status;
        }
    }
}
