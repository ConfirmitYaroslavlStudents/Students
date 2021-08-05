using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListNikeshina
{
    public class ConsoleInputDataStorage : IGetInputData
    { 
        public string GetInputData()
        {
            return Console.ReadLine();
        }
    }
}
