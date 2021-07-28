using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ToDo
{
    public class ToDoList : List<Task>
    {
        public override string ToString()
        {
            StringBuilder toDoList = new StringBuilder();
            for (var i = 0; i < Count; i++)
                toDoList.AppendLine(i + 1 + ". " + this[i].ToString());
            return toDoList.ToString();
        }
    }
}