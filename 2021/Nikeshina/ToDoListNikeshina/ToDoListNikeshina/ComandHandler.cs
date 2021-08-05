using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListNikeshina
{
    public class ComandHandler
    {
        public void HandlerWork(string [] args)
        {
            var logger = new Logger();
            if (args.Length == 0)
            {
               var app = new ConsoleApp(logger, new ConsoleInputDataStorage());
                logger.Log(Messages.InsructionText());
                while (SwitchForConsoleApp(app)) ;
                app.Save();
            }
            else
            {
               var app = new CmdApp(logger, new CmdInputDataStorage(args));
                SwitchForCmdApp(app);
                app.Save();
            }
            
        }
        private bool SwitchForConsoleApp(ConsoleApp app)
        {
            var comand = app.DataGetter.GetInputData();
            switch (comand)
            {
                case "list":
                    app.Print();
                    return true;
                case "add":
                    app.AddNewTask();
                    return true;
                case "edit":
                    app.EditDescription();
                    return true;
                case "change":
                    app.ChangeStatus();
                    return true;
                case "delete":
                    app.Delete();
                    return true;
                case "rollback":
                    app.Rollback();
                    return true;
                case "exit":
                    return false;
                default:
                    return true;
            }
        }
        private void SwitchForCmdApp(CmdApp app)
        {
            var comand = app.DataGetter.GetInputData();
            switch (comand)
            {
                case "list":
                    app.Print();
                    break;
                case "add":
                    app.AddNewTask();
                    break;
                case "edit":
                    app.EditDescription();
                    break;
                case "change":
                    app.ChangeStatus();
                    break;
                case "delete":
                    app.Delete();
                    break;
                default:
                    return;
            }

        }
    }
}
