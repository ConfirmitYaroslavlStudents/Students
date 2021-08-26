using System.Collections.Generic;
using ToDoListLib;

namespace ToDoApi.SaveAndLoad
{
    public interface IListSaveAndLoad
    {
        public IEnumerable<ToDoItem> LoadTheList();
        public void SaveTheList(IEnumerable<ToDoItem> toDoList);
    }
}
