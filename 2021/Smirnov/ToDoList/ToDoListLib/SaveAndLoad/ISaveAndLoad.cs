using System.Collections.Generic;
using ToDoListLib.Models;

namespace ToDoListLib.SaveAndLoad
{
    public interface ISaveAndLoad
    {
        public IEnumerable<Task> Load();
        public void Save(IEnumerable<Task> toDoList);
    }
}
