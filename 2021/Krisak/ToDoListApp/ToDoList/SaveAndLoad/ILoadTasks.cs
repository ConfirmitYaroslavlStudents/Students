using System.Collections.Generic;

namespace ToDoLibrary.SaveAndLoad
{
    public interface ILoadTasks
    {
        public List<Task> Load();
    }
}