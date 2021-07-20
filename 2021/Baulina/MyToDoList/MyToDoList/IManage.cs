namespace ToDoApp
{
    public interface IManage
    {
        void Add();
        void Edit();
        void MarkAsComplete();
        void Delete();
        void ViewAllTasks();
        string GetMenuItemName();
    }

}
