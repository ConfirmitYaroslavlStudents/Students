namespace ToDoListProject
{
    public interface IToDoListLoaderSaver
    {
        public ToDoList Load();
        public void Save(ToDoList toDoList);
    }
}
