using Newtonsoft.Json;

namespace ToDoApiDependencies
{
    public class ToDoTask
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsDone { get; set; }

        public ToDoTask(string text, bool isDone)
        {
            Text = text;
            IsDone = isDone;
        }

        public ToDoTask() { } 

        public override string ToString()
        {
            if (IsDone==true)
                return $"{Id}. {Text}  [v]";
            return $"{Id}. {Text}  [ ]";
        }
    }
}
