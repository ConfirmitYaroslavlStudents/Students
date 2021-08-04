using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListNikeshina
{
    public class AppGetterInput : IGetterInputData
    { 
        public string GetInputData()
        {
            return Console.ReadLine();
        }
    }
}
