namespace ToDoListApp.Reader
{
    public interface IReader
    {
        public string GetTaskDescription();
        public int GetTaskId();
        public ListCommandMenu GetCommand();
        public bool ContinueWork();
    }
}
