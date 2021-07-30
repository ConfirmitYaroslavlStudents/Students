using System.Collections.Generic;
using MyToDoList;

namespace ToDoListTestProject.ApiControllerTests
{
    public class ListSaveAndLoadFake : IListSaveAndLoad
    {
        private readonly ToDoList _toDoList;
        public ToDoList SavedList { get; private set; }

        public ListSaveAndLoadFake(IEnumerable<ToDoItem> toDoItems)
        {
            _toDoList = new ToDoList(toDoItems);
            SavedList = new ToDoList();
        }

        public IEnumerable<ToDoItem> LoadTheList()
        {
            return _toDoList;
        }

        public void SaveTheList(IEnumerable<ToDoItem> toDoList)
        {
            SavedList = _toDoList;
        }
    }
}
