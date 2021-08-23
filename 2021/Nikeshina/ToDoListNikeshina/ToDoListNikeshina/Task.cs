using Newtonsoft.Json;

namespace ToDoListNikeshina

{
    public enum StatusOfTask
    {
        Todo = 0,
        InProgress = 1,
        Done = 2
    }
    public class Task
    {
        public string Name { get; private set; }
        public StatusOfTask Status { get; private set; }

        public readonly int _id;

        [JsonConstructor]
        public Task(string name, StatusOfTask status)
        {
            Name = name;
            Status = status;
        }

        public Task(string name, StatusOfTask status, int id)
        {
            Name = name;
            Status = status;
            _id = id;
        }

        public void ChangeStatus()
        {
            if (Status == StatusOfTask.Todo)
                Status = StatusOfTask.InProgress;
            else if (Status == StatusOfTask.InProgress)
                Status = StatusOfTask.Done;
        }

        public void ChangeStatus(StatusOfTask status)
        {
            Status = status;
        }

        public void ChangeName(string newName)
        {
            Name = newName;
        }
        public string StringFormat()
        {
            var str = _id + " " + Name + " " + Status;
            return str;
        }

        public override bool Equals(object obj)
        {
            var other = (Task)obj;

            return this.Name == other.Name &&
                this.Status == other.Status;
        }
    }
}
