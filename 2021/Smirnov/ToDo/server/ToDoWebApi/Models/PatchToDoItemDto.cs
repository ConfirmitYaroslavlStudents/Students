using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToDoWebApi.Models
{
    public class PatchToDoItemDto: PatchDtoBase
    {
        [StringLength(1000)]
        public string Description { set; get; }
        [Range(0, 1)]
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
