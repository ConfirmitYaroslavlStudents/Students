namespace ToDoListConsole
{
    public interface IReader
    {
        public string GetDescription();
        public int GetNumberTask();
        public ToDoListMenuEnum GetSelectedAction();
    }
}
