using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoWebApi.Models
{
    public class TagToDoItem
    {
        
        public long Id { set; get; }
        public long ToDoItemId { set; get; }
        public long TagId { set; get; }
    }
}
