using System.Collections.Generic;
using MyToDoList;

namespace ToDoApi
{
    public interface IToDoListProvider
    {
        IEnumerable<ToDoItem> GetToDoList();
    }
}
