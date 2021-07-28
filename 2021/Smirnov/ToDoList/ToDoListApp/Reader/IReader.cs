namespace ToDoListApp.Reader
{
    public interface IReader
    {
        public string GetDescription();
        public int GetTaskId();
        public ToDoListMenuEnum GetSelectedAction();
        public bool ContinueWork();
    }
}
