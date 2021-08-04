using System.Collections.Generic;
using ToDoApi.Models;

namespace ToDoApi.SaveAndLoad
{
    public interface IListSaveAndLoad
    {
        public IEnumerable<ToDoItem> LoadTheList();
        public void SaveTheList(IEnumerable<ToDoItem> toDoList);
    }
}
