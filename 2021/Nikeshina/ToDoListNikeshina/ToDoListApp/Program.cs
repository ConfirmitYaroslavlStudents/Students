using System;
using ToDoListNikeshina;
using System.Text;

namespace ToDoListApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
                DoCommand();
            else
            {
                var app = new CmdApp(new AppLogger(), args);
                app.Read();
                GetCommand(app, args[0]);
                app.Write();
                return;
            }
        }

        private static void DoCommand()
        {
            var app = new ConsoleApp(new AppLogger());
            app.Read();
            WriteInstuction();
            var str = Console.ReadLine();
            while (str != null || str != "")
            {
                if (str == "exit")
                {
                    app.Write();
                    return;
                }

                GetCommand(app, str);
                str = Console.ReadLine();
            }
        }
        private static void GetCommand(IApp app, string command)
        {
            switch (command)
            {
                case "list":
                    app.Print();
                    break;
                case "add":
                    app.Add();
                    break;
                case "delete":
                    app.Delete();
                    break;
                case "change":
                    app.ChangeStatus();
                    break;
                case "edit":
                    app.Edit();
                    break;
                default:
                    return;
            }
        }

        private static void WriteInstuction()
        {
            Console.WriteLine("----------------------------------------------------------");
            Console.Write("list - print ToDoList  ");
            Console.Write("add - add   ");
            Console.Write("delete - delete    ");
            Console.Write("edit - edit   ");
            Console.Write("change - change status   ");
            Console.WriteLine("exit - exit");
            Console.WriteLine("----------------------------------------------------------");
        }
    }
}
