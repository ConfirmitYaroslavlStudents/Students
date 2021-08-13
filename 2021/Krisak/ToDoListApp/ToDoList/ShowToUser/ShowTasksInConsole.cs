using System;
using System.Collections.Generic;

namespace ToDoLibrary
{
    public class ShowTasksInConsole: IShowTasks
    {
        public void ShowTasks(List<Task> tasks)
        {
            for (var i = 0; i < tasks.Count; i++)
                Console.WriteLine($"{i + 1}. {tasks[i]}");
        }
    }
}