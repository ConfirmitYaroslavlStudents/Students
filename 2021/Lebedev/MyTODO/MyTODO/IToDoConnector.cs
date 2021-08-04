namespace MyTODO
{
    public interface IToDoConnector
    {
        ToDoList FindAll(bool completed, bool deleted);
        void Complete(int id);
        void Delete(int id);
        void ChangeName(int id, string name);
        void Add(string name);
        ToDoItem GetItem(int id);
    }
}
