namespace ToDo
{
    public class Task
    {
        public string Text { get; set; }
        public bool IsDone { get; set; }

        public Task(string text, bool isDone = false)
        {
            Text = text;
            IsDone = isDone;
        }

        public override string ToString()
        {
            if (IsDone)
                return Text + "  [v]";
            return Text + "  [ ]";
        }
    }
}
