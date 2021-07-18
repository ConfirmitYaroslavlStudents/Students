using System;

namespace ToDoListProject
{
    [Serializable]
    public class Task
    {
        public string Text { get; private set; }
        public bool IsDone { get; private set; }

        public Task(string text)
        {
            Text = text;
        }

        public void ChangeText(string newText)
        {
            Text = newText;
        }

        public void ChangeStatus()
        {
            IsDone = !IsDone;
        }

        public override string ToString()
        {
            if (IsDone)
                return Text + "  [v]";
            return Text + "  [ ]";
        }
    }
}
