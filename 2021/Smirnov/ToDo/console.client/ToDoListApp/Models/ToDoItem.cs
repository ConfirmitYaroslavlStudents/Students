namespace ToDoListApp.Models
{
    public class ToDoItem
    {
        public int Id { set; get; }
        public string Description { set; get; }
        public string Status { set; get; }
        public override string ToString()
        {
            return $"{Id} {Description} {Status}";
        }
    }
}
