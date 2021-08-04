namespace ToDo
{
    public interface ILoaderAndSaver
    {
        public ToDoList Load();
        public void Save(ToDoList toDoList);
    }
}
