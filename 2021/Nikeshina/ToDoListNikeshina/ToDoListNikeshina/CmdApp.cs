using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListNikeshina
{
    public class CmdApp :App, IApp
    {
        //public ToDoList List { get; set; }
        //public ILogger Logger { get; set; }
        string [] _params;

        public CmdApp() { }

        public CmdApp(ILogger logger, string[] parametrs):base(logger)
        {
            Logger = logger;
            List = new ToDoList(Logger);
            _params = parametrs;
        }

        public override void Add()
        {
            if(_params.Length==2 && IsDataValidString(_params[1]))
            {
                List.Add(new Task(_params[1], false));
                Logger.WriteLine(Messages.IsDone());
                return;
            }

            Logger.WriteLine(Messages.WrongFormatOfInputData());
        }

        public override void ChangeStatus()
        {
            if(_params.Length==2 && IsDataValidDigit(_params[1]))
            {
                int num = int.Parse(_params[1]);
                List.ChangeStatus(num);
                Logger.WriteLine(Messages.IsDone());
                return;
            }

            Logger.WriteLine(Messages.WrongFormatOfInputData());
        }

        public override void Delete()
        {
            if (_params.Length == 2 && IsDataValidDigit(_params[1]))
            {
                int num = int.Parse(_params[1]);
                List.Delete(num);
                Logger.WriteLine(Messages.IsDone());
                return;
            }

            Logger.WriteLine(Messages.WrongFormatOfInputData());
        }

        public override void Edit()
        {
            if (_params.Length == 3 && IsDataValidDigit(_params[1]) && IsDataValidString(_params[2]))
            {
                int num = int.Parse(_params[1]);
                List.Edit(num, _params[1]);
                Logger.WriteLine(Messages.IsDone());
                return;
            }

            Logger.WriteLine(Messages.WrongFormatOfInputData());
        }
        
    }
}
