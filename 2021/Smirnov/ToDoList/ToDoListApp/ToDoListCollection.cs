using System.Collections.Generic;
using System.Xml.Serialization;
using ToDoListLib;

namespace ToDoListApp
{
    public class ToDoListCollection
    {
        [XmlArray("Collection"), XmlArrayItem("Item")]
        public List<Task> Collection { get; set; }
    }
}
