
namespace ToDoListNikeshina
{
    public class ComandHandler
    {
        public void HandlerWork(string[] args)
        {
            var logger = new Logger();
            var fm = new FileManager();
            var startedList = fm.Load();
            IApp app;

            if (args.Length == 0)
            {
                app = new ConsoleApp(logger, new ConsoleInputDataStorage(), startedList.Key, startedList.Value);
                logger.Log(Messages.InsructionText());
                while (SwitchForConsoleApp((ConsoleApp)app)) ;
            }
            else
            {
                app = new CmdApp(logger, new CmdInputDataStorage(args), startedList.Key, startedList.Value);
                SwitchForCmdApp((CmdApp)app);
            }

            fm.Save(app.GetListOfTask());
        }
        private bool SwitchForConsoleApp(ConsoleApp app)
        {
            var comand = app.dataGetter.GetInputData();
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
            var comand = app.dataGetter.GetInputData();
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
