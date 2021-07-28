namespace ToDoListLib.Models
{
    public class Task
    {
        public long Id { get; set; }
        public string Description { set; get; }
        public TaskStatus Status { set; get; }
    }
}
