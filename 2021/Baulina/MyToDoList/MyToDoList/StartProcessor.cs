using MyToDoList;

namespace ToDoListConsole
{
    class StartProcessor
    {
        public ToDoList MyToDoList { get; private set; }
        
        public void LoadTheList()
        {
            var toDoItemsArray = DataHandler.LoadFromFile();
            MyToDoList = new ToDoList(toDoItemsArray);
        }
    }
}
