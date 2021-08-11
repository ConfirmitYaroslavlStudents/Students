using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListNikeshina
{
    public interface IApp
    {
        public List<Task> GetListOfTask();
    }
}
