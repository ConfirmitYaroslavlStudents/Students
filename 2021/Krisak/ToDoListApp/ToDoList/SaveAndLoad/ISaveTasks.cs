using System.Collections.Generic;

namespace ToDoLibrary.SaveAndLoad
{
    public interface ISaveTasks
    {
        public void Save(List<Task> tasks);
    }
}