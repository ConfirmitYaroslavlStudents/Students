using System.ComponentModel.DataAnnotations;

namespace ToDoWebApi.Models
{
    public class Tag
    {
        public long Id { set; get; }
        [StringLength(100)]
        public string Name { set; get; }
    }
}
