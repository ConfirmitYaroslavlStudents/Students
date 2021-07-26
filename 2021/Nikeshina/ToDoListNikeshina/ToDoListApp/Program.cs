using System;
using ToDoListNikeshina;
using System.Text;

namespace ToDoListApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ILogger logger;
            App application;
            if (args.Length == 0)
                application = new App(logger=new AppLogger());
            else
                application = new App(logger=new CmdLogger(args));

            logger.Recording(Messages.WriteInstuction());

            if (logger as AppLogger != null)
                while (GetCommand(application, logger.TakeData())) ;
            else
            {
                GetCommand(application, logger.TakeData());
                application.Save();
            }

        }
        public static bool GetCommand(App app, string command)
        {
            switch (command)
            {
                case "list":
                    app.Print();
                    return true;
                case "add":
                    app.Add();
                    return true;
                case "delete":
                    app.Delete();
                    return true;
                case "change":
                    app.ChangeStatus();
                    return true;
                case "edit":
                    app.Edit();
                    return true;
                case "exit":
                    app.Save();
                    return false;
                default:
                    return true;
            }
        }

       
    }
}
