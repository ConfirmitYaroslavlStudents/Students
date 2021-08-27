using System.Collections.Generic;
using ToDoLibrary;
using ToDoLibrary.SaveAndLoad;

namespace TestProjectForToDoLibrary
{
    public class FakeLoader : ILoadTasks
    {
        private readonly List<Task> _tasks;

        public FakeLoader(List<Task> tasks)
            => _tasks = tasks;

        public List<Task> Load()
            => _tasks;
    }
}