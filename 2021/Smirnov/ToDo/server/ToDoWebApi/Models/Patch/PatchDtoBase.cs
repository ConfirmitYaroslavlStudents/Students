using System.Collections.Generic;

namespace ToDoWebApi.Models.Patch
{
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
