namespace ToDo
{
    public class TestLoaderSaver : ILoaderSaver
    {
        public ToDoList ToDoList=new ToDoList();
        public ToDoList Load()
        {
            return ToDoList;
        }

        public void Save(ToDoList toDoList)
        {
            ToDoList = toDoList;
        }
    }
}
