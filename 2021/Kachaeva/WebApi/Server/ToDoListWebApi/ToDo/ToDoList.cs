using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToDoApiDependencies
{
    public class ToDoList : Dictionary<int, ToDoTask>
    {
        private int _id;

        public ToDoList()
        {
            _id = 1;
        }

        public void Add(ToDoTask toDoTask)
        {
            if(Count!=0)
                _id = Keys.Last() + 1;
            toDoTask.Id = _id;
            base.Add(_id,toDoTask);
        }

        public override string ToString()
        {
            StringBuilder toDoList = new StringBuilder();
            foreach (var toDoTask in this)
            {
                toDoList.AppendLine(toDoTask.Value.ToString());
            }
            return toDoList.ToString();
        }
    }
}