namespace ToDo
{
    public interface ILoaderSaver
    {
        public ToDoList Load();
        public void Save(ToDoList toDoList);
    }
}
