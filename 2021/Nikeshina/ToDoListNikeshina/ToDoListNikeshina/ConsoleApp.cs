using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListNikeshina
{
    public class ConsoleApp:App, IApp
    {
        //public ToDoList List { get; set; }
        //public ILogger Logger { get; set; }

        public ConsoleApp() { }

        public ConsoleApp(ILogger logger) : base(logger) { }

        public override void Add()
        {
           Logger.WriteLine(Messages.RequestDescription());

            var dscr = Logger.ReadLine();

            if(!IsDataValidString(dscr))
                return;

            List.Add(new Task(dscr, false));
        }

        public override void Delete()
        {
            Logger.WriteLine(Messages.RequestNumber());

            var inputStr = Logger.ReadLine();

            if (!IsDataValidDigit(inputStr))
                return;

            int num = int.Parse(inputStr);

            List.Delete(num);
        }

        public override void Edit()
        {
            Logger.WriteLine(Messages.RequestNumber());

            var inputStr = Logger.ReadLine();

            if (!IsDataValidDigit(inputStr))
                return;

            int num = int.Parse(inputStr);

            Logger.WriteLine(Messages.RequestDescription());

            var dscr = Logger.ReadLine();

            if (!IsDataValidString(dscr))
                return;

            List.Edit(num, dscr);
        }

        public override void ChangeStatus()
        {
            Logger.WriteLine(Messages.RequestNumber());
            

            var inputStr = Logger.ReadLine();

            if (!IsDataValidDigit(inputStr))
                return;

            int num = int.Parse(inputStr);

            List.ChangeStatus(num);
        }
    }
}
