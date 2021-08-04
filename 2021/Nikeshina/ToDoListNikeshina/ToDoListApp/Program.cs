using System;
using ToDoListNikeshina;
using System.Text;

namespace ToDoListApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var handler = new ComandHandler();
            handler.HandlerWork(args);
        }       
    }
}
