using System;
using System.Collections.Generic;

namespace ToDoLibrary
{
    public interface IShowTasks
    {
        public void ShowTasks(List<Task> tasks);
    }
}