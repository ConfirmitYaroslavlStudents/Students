using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListNikeshina
{
    public class CmdInputDataStorage : IGetInputData
    {
        public List<string> inputStrings = new List<string>();

        public CmdInputDataStorage(string[] input)
        {
            inputStrings.AddRange(input);
        }

        public void AddNewParametrs(List<string> @params)
        {
            inputStrings = @params;
        }

        public string GetInputData()
        {
            var current = inputStrings[0];
            inputStrings.RemoveAt(0);
            return current;
        }

    }
}
