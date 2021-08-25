namespace MyTODO
{
    public class ToDoForConsoleConnector:IToDoConnector
    {
        private ToDoList _todo;

        public ToDoForConsoleConnector(ToDoList todo)
        {
            _todo = todo;
        }

        public void Add(string name)
        {
            _todo.Add(name);
        }

        public void ChangeName(int id, string name)
        {
            _todo[id].ChangeName(name);
        }

        public void Complete(int id)
        {
            _todo[id].Complete();
        }

        public void Delete(int id)
        {
            _todo[id].Delete();
        }

        public ToDoList FindAll(bool completed, bool deleted)
        {
            return _todo.FindAll(completed, deleted);
        }

        public ToDoItem GetItem(int id)
        {
            return _todo[id];
        }
    }
}
