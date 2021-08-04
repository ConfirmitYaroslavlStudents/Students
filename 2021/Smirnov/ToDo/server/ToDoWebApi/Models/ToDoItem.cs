using System.ComponentModel.DataAnnotations;

namespace ToDoWebApi.Models
{
    public class ToDoItem
    {
        public long Id { get; set; }
        [StringLength(1000)]
        public string Description { set; get; }
        [Range(0, 1)]
        public ToDoItemStatus Status { set; get; }
    }
}
