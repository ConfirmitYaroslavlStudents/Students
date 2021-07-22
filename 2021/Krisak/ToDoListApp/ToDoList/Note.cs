using System;

namespace ToDoList
{
    public class Note
    {
        public string Text { get; set; } 
        public bool isCompleted { get; set; }

        public override string ToString()
        {
            if (isCompleted)
                return "X "+Text;

            return Text;
        }
    }
}
