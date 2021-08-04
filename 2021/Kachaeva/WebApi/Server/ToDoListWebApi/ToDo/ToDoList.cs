using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToDo
{
    public class ToDoList : Dictionary<int, Task>
    {
        private int _id;

        public ToDoList()
        {
            _id = 1;
        }

        public void Add(Task task)
        {
            if(Count!=0)
                _id = Keys.Last() + 1;
            base.Add(_id,task);
        }

        public override string ToString()
        {
            StringBuilder toDoList = new StringBuilder();
            foreach (var task in this)
            {
                toDoList.AppendLine($"{task.Key}. {task.Value.ToString()}");
            }
            return toDoList.ToString();
        }
    }
}