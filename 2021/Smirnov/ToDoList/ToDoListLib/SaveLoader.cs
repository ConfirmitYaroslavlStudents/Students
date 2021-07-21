using System.IO;
using System.Xml.Serialization;

namespace ToDoListLib
{
    public class SaveLoader
    {
        private const string FilePath = "tasks.xml";
        private readonly XmlSerializer _xmlSerializer = new XmlSerializer(typeof(ToDoList));
        public ToDoList Load()
        {
            if (!File.Exists(FilePath))
            {
                return new ToDoList();
            }
            using var streamReader = new StreamReader(FilePath);
            return (ToDoList)_xmlSerializer.Deserialize(streamReader);
        }
        public void Save(ToDoList toDoListApp)
        {
            using var streamWriter = new StreamWriter(FilePath);
            _xmlSerializer.Serialize(streamWriter, toDoListApp);
        }
    }
}
