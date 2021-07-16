using System;
using ToDoListNikeshina;

namespace ToDoListApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new Application();
            app.Read();
            ComandHandling(app);
        }

        private static void ComandHandling(Application app)
        {
            ClearScrean(app);
            var str = Console.ReadLine();
            while (str != null || str != "")
            {
                switch (str)
                {
                    case "С":
                        app.Print();
                        break;
                    case "Д":
                        app.Add();
                        break;
                    case "У":
                        app.Delete();
                        break;
                    case "Р":
                        app.Edit();
                        break;
                    case "И":
                        app.ChangeStatus();
                        break;
                    case "В":
                        app.Write();
                        return;
                    default:
                        break;
                }
                ClearScrean(app);

                str = Console.ReadLine();
            }
        }

        private static void ClearScrean(Application app)
        {
            Console.Clear();
            WriteInstuction();
            app.Print();
        }

        private static void WriteInstuction()
        {
            Console.WriteLine("----------------------------------------------------------");
            Console.Write("С - список;  ");
            Console.Write("Д - добавить запись;   ");
            Console.Write("У - удалить запись;   ");
            Console.Write("Р - редактировать запись;   ");
            Console.Write("И - изменить статус;   ");
            Console.WriteLine("В - выход");
            Console.WriteLine("----------------------------------------------------------");
        }
    }
}
