using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ToDoWebApi.Models.Patch
{
    public class PatchToDoItemDto: PatchDtoBase
    {
        [StringLength(1000)]
        public string Description { set; get; }
        [EnumDataType(typeof(ToDoItemStatus))]
        [JsonConverter(typeof(StringEnumConverter))]
        public ToDoItemStatus Status { set; get; }
    }
}
