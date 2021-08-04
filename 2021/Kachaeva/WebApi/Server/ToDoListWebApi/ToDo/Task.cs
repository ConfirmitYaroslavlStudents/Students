using Newtonsoft.Json;

namespace ToDo
{
    public class Task
    {
        public string Text { get; set; }
        public bool? IsDone { get; set; }

        [JsonConstructor]
        public Task(string text, bool isDone)
        {
            Text = text;
            IsDone = isDone;
        }

        public Task(string text)
        {
            Text = text;
            IsDone = null;
        }

        public Task(bool isDone)
        {
            Text = null;
            IsDone = isDone;
        }

        public Task()
        {
        }

        public override string ToString()
        {
            if (IsDone==true)
                return $"{Text}  [v]";
            return $"{Text}  [ ]";
        }
    }
}
