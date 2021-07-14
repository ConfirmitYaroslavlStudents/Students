using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using ToDoListLib;

namespace ToDoListApp
{
    public class Serializer
    {
        public ToDoListCollection Deserialization()
        {
            var xmlSerializer = new XmlSerializer(typeof(ToDoListCollection));

            if (!File.Exists("tasks.xml"))
            {
                Serialization(new ToDoListCollection());
            }

            var stringReader = new StreamReader("tasks.xml");
            var toDoListCollection = (ToDoListCollection)xmlSerializer.Deserialize(stringReader);
            stringReader.Close();
            return toDoListCollection;
        }
        public void Serialization(ToDoListCollection toDoListApp)
        {
            var xmlSerializer = new XmlSerializer(typeof(ToDoListCollection));
            var stringWriter = new StreamWriter("tasks.xml");
            xmlSerializer.Serialize(stringWriter, toDoListApp);
            stringWriter.Close();
        }
    }
}
