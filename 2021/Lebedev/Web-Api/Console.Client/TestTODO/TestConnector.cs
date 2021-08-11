using MyTODO;

namespace TodoClientTest
{
    class TestConnector : IToDoConnector
    {
        public ToDoList ToDoList;
        public TestConnector(ToDoList list)
        {
            ToDoList = list;
        }
        public void Add(string name)
        {
            ToDoList.Add(name);
        }

        public void ChangeName(int id, string name)
        {
            ToDoList[id].ChangeName(name);
        }

        public void Complete(int id)
        {
            ToDoList[id].SetCompleted();
        }

        public void Delete(int id)
        {
            ToDoList[id].SetDeleted();
        }

        public ToDoList FindAll(bool completed, bool deleted)
        {
            return ToDoList.FindAll(completed, deleted);
        }

        public ToDoItem GetItem(int id)
        {
            return ToDoList[id];
        }
    }
}
