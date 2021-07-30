using System.Collections.Generic;

namespace MyToDoList
{
    public interface IListSaveAndLoad
    {
        public IEnumerable<ToDoItem> LoadTheList();
        public void SaveTheList(IEnumerable<ToDoItem> toDoList);
    }
}
