using System;
using System.Collections.Generic;
using ToDoListNikeshina;

namespace ToDoListTests
{
    public class CmdTestLogger:ILogger
    {
        public List<string> Messages = new List<string>();
        public List<string> inputStrings = new List<string>();

        public CmdTestLogger(List<string> input)
        {
            inputStrings = input;
        }

        public string TakeData()
        {
            var current = inputStrings[0];
            inputStrings.RemoveAt(0);
            return current;
        }

        public void Recording(string msg)
        {
            if (msg == "Done! " || msg.Contains("True") || msg.Contains("False") || msg.Contains("Incorrect"))
                    Messages.Add(msg);
        }
    }
}
