using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListNikeshina
{
    public class TestLogger : ILogger
    {
        public List<string> Messages = new List<string>();
        public List<string> inputStrings = new List<string>();


        public TestLogger() { }

        public TestLogger(List<string> input)
        {
            inputStrings = input;
        }

        public string ReadLine()
        {
            var current = inputStrings[0];
            inputStrings.RemoveAt(0);
            return current;
        }

        public void WriteLine(string msg)
        {
            Messages.Add(msg);
        }
    }
}
