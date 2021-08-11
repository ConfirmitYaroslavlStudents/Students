using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ToDoWebApi.Models
{
    public class ToDoItem
    {
        public long Id { get; set; }
        [StringLength(1000)]
        public string Description { set; get; }
        [EnumDataType(typeof(ToDoItemStatus))]
        [JsonConverter(typeof(StringEnumConverter))]
        public ToDoItemStatus Status { set; get; }
    }
}
