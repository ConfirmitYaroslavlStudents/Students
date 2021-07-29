using System;

namespace ToDoList
{
    public class Note
    {
        public string Text { get; set; } 
        public bool isCompletedFlag { get; set; }

        public override string ToString()
        {
            if (isCompletedFlag)
                return Text + " [X]";

            return Text;
        }
    }
}
