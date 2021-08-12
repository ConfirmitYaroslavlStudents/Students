namespace ToDoListApp.Reader
{
    public interface IReader
    {
        public string GetDescription();
        public int GetTaskId();
        public ListCommandMenu GetCommand();
        public bool ContinueWork();
    }
}
