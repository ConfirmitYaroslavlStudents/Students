using System.IO;
using System.Xml.Serialization;
using ToDoListApp;


namespace ToDoListConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var serializer = new Serializer();
            var ToDoListCollection = serializer.Deserialization();

            var toDoListApp = new ToDoListApp.ToDoListApp(ToDoListCollection.Collection);
            while (ToDoListMenu.WorkWithMenu(toDoListApp)) ;

            ToDoListCollection.Collection = toDoListApp.GetAllTask();
            serializer.Serialization(ToDoListCollection);           
        }
    }
}
