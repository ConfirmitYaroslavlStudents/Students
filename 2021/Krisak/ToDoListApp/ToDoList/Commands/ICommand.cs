using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoLibrary.Commands
{
    public interface ICommand
    {
        public List<Task> PerformCommand(List<Task> tasks);
    }
}
