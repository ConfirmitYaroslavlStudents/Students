using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ToDoWebApi.Models
{
    public class PatchToDoItemDto: PatchDtoBase
    {
        [StringLength(1000)]
        public string Description { set; get; }
        [EnumDataType(typeof(ToDoItemStatus))]
        [JsonConverter(typeof(StringEnumConverter))]
        public ToDoItemStatus Status { set; get; }
    }
    public abstract class PatchDtoBase
    {
        private List<string> PropertiesInHttpRequest { get; set; }
            = new List<string>();
        public bool IsFieldPresent(string propertyName)
        {
            return PropertiesInHttpRequest.Contains(propertyName.ToLowerInvariant());
        }

        public void SetHasProperty(string propertyName)
        {
            PropertiesInHttpRequest.Add(propertyName.ToLowerInvariant());
        }
    }
}
