using System.Collections.Generic;
using System.Text;

namespace ToDoApiDependencies
{
    public class ToDoTask
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsDone { get; set; }
        public IEnumerable<string> Tags { get; set; }

        public ToDoTask(string text, bool isDone, List<string> tags)
        {
            Text = text;
            IsDone = isDone;
            Tags = tags;
        }

        public ToDoTask() { } 

        public override string ToString()
        {
            var toDoTask = new StringBuilder();
            if (IsDone==true)
                toDoTask.Append($"{Id}. {Text}  [v] ");
            else
                toDoTask.Append($"{Id}. {Text}  [ ] ");
            foreach (var tag in Tags)
                toDoTask.Append(tag + ' ');
            return toDoTask.ToString();
        }
    }
}
