namespace ToDoApp.Models
{
    public class ToDoItem
    {
        public int Id { get; set; }
        public ToDoItemStatus Status { get; set; }
        public string Description { get; set; }
    }
}