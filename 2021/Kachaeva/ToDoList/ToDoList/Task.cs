using System;

namespace ToDoList
{
    [Serializable]
    public class Task
    {
        public string text;
        public bool isDone;

        public Task(string text)
        {
            this.text = text;
        }

        public void ChangeText(string newText)
        {
            text = newText;
        }

        public void ChangeStatus()
        {
            isDone = !isDone;
        }
    }
}
