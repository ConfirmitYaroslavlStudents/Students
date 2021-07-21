namespace ToDoListProject
{
    public class TestLoaderSaver : IToDoListLoaderSaver
    {
        public ToDoList Load()
        {
            return new ToDoList();
        }

        public void Save(ToDoList toDoList)
        {
        }
    }
}
