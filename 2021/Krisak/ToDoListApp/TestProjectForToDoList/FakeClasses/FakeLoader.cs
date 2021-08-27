using System.Collections.Generic;
using ToDoLibrary;
using ToDoLibrary.SaveAndLoad;

namespace TestProjectForToDoLibrary
{
    public class FakeLoader : ILoadTasks
    {
        public List<Task> Load()
            => new List<Task>();
    }
}