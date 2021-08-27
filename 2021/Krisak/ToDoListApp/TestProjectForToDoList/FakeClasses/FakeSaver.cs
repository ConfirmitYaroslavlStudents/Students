using System.Collections.Generic;
using ToDoLibrary;
using ToDoLibrary.SaveAndLoad;

namespace TestProjectForToDoLibrary
{
    public class FakeSaver: ISaveTasks
    {
        public void Save(List<Task> tasks) { }
    }
}