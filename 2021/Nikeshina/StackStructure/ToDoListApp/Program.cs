using System;
using ToDoListLib;

namespace ToDoListApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new Application();
            WriteInstuction();
            app.Read();
            var str = Console.ReadLine();
            while (str != null || str != "")
            {
                switch (str)
                {
                    case "R":
                        app.Print();
                        break;
                    case "A":
                        app.Add();
                        break;
                    case "D":
                        app.Delete();
                        Console.Clear();
                        WriteInstuction();
                        break;
                    case "C":
                        app.ChangeStatus();
                        break;
                    case "E":
                        app.Write();
                        return;
                    default:
                        break;
                }
                str = Console.ReadLine();
            }
        }
        public static void WriteInstuction()
        {
            Console.WriteLine("----------------------------------------------------------");
            Console.Write("R - Read;  ");
            Console.Write("A - Add;   ");
            Console.Write("D - Delete;   ");
            Console.Write("С - Change the status;   ");
            Console.WriteLine("E - Exit.");
            Console.WriteLine("----------------------------------------------------------");
        }
    }
}
