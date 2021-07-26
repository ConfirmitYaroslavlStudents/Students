using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListNikeshina
{
    public class CmdLogger:ILogger
    {
        public List<string> inputStrings = new List<string>();

        public CmdLogger(string[] input)
        {
            inputStrings.AddRange(input);
        }

        public void AddNewParametrs(List<string> @params)
        {
            inputStrings = @params;
        }

        public string TakeData()
        {
            var current = inputStrings[0];
            inputStrings.RemoveAt(0);
            return current;
        }

        public void Recording(string msg)
        {
            if (msg == "Done! "|| msg.Contains("True")|| msg.Contains("False")||msg.Contains("Incorrect"))
                Console.WriteLine(msg);
        }
        private bool IsParamsCorrect(List<string> @params)
        {
            if ((@params[0] == "list" || @params[0] == "exit") && @params.Count == 1)
                return true;
            else if ((@params[0] == "add" || @params[0] == "delete") && @params.Count == 2)
                return true;
            else if (@params[0] == "change" && @params.Count == 3)
                return true;

            return false;
        }
    }
}
