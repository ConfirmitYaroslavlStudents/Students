using System;
using ToDoListNikeshina;
using System.Text;

namespace ToDoListApp
{
    class Program
    {
        static void Main(string[] args)
        {
            App application;
            if (args.Length == 0)
                application = new ConsoleApp();
            else
                application = new CmdApp(args);

            application.StringHandling();
        }       
    }
}
